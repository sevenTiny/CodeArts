using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public class DynamicProxy
    {
        public static IInterface Inject<IInterface, T>() where T : class, new() where IInterface : class
        {
            string nameOfAssembly = typeof(T).Name + "ProxyAssembly";
            string nameOfModule = typeof(T).Name + "ProxyModule";
            string nameOfType = typeof(T).Name + "Proxy";

            var assemblyName = new AssemblyName(nameOfAssembly);
            //var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            //var moduleBuilder = assembly.DefineDynamicModule(nameOfModule);
            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assembly.DefineDynamicModule(nameOfModule, nameOfAssembly + ".dll");

            var typeBuilder = moduleBuilder.DefineType(nameOfType, TypeAttributes.Public, null, new[] { typeof(IInterface) });

            InjectInterceptor<T>(typeBuilder);

            var t = typeBuilder.CreateType();

            assembly.Save(nameOfAssembly + ".dll");

            return Activator.CreateInstance(t) as IInterface;
        }

        private static void InjectInterceptor<T>(TypeBuilder typeBuilder)
        {
            // ---- define fields ----

            var fieldInterceptor = typeBuilder.DefineField("_interceptor", typeof(Interceptor), FieldAttributes.Private);

            // ---- define costructors ----

            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
            var ilOfCtor = constructorBuilder.GetILGenerator();

            ilOfCtor.Emit(OpCodes.Ldarg_0);
            ilOfCtor.Emit(OpCodes.Newobj, typeof(Interceptor).GetConstructor(new Type[0]));
            ilOfCtor.Emit(OpCodes.Stfld, fieldInterceptor);
            ilOfCtor.Emit(OpCodes.Ret);

            // ---- define methods ----

            var methodsOfType = typeof(T).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            for (var i = 0; i < methodsOfType.Length; i++)
            {
                var method = methodsOfType[i];
                var methodParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

                var methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final, CallingConventions.Standard, method.ReturnType, methodParameterTypes);

                var ilMethod = methodBuilder.GetILGenerator();
                ilMethod.Emit(OpCodes.Ldarg_0);
                ilMethod.Emit(OpCodes.Ldfld, fieldInterceptor);

                // create instance of T
                ilMethod.Emit(OpCodes.Newobj, typeof(T).GetConstructor(new Type[0]));
                ilMethod.Emit(OpCodes.Ldstr, method.Name);

                // build the method parameters
                if (methodParameterTypes == null)
                {
                    ilMethod.Emit(OpCodes.Ldnull);
                }
                else
                {
                    var parameters = ilMethod.DeclareLocal(typeof(object[]));
                    ilMethod.Emit(OpCodes.Ldc_I4, methodParameterTypes.Length);
                    ilMethod.Emit(OpCodes.Newarr, typeof(object));
                    ilMethod.Emit(OpCodes.Stloc, parameters);

                    for (var j = 0; j < methodParameterTypes.Length; j++)
                    {
                        ilMethod.Emit(OpCodes.Ldloc, parameters);
                        ilMethod.Emit(OpCodes.Ldc_I4, j);
                        ilMethod.Emit(OpCodes.Ldarg, j + 1);
                        //box
                        ilMethod.Emit(OpCodes.Box, method.ReturnType);

                        ilMethod.Emit(OpCodes.Stelem_Ref);
                    }
                    ilMethod.Emit(OpCodes.Ldloc, parameters);
                }

                // call Invoke() method of Interceptor
                ilMethod.Emit(OpCodes.Callvirt, typeof(Interceptor).GetMethod("Invoke"));

                // pop the stack if return void
                if (method.ReturnType == typeof(void))
                {
                    ilMethod.Emit(OpCodes.Pop);
                }
                else
                {
                    //unbox
                    ilMethod.Emit(OpCodes.Unbox_Any, method.ReturnType);
                    ilMethod.Emit(OpCodes.Stloc_0);
                    ilMethod.Emit(OpCodes.Ldloc_0);
                }

                // complete
                ilMethod.Emit(OpCodes.Ret);
            }
        }
    }

    public class Interceptor
    {
        public object Invoke(object @object, string @method, object[] parameters)
        {
            Trace.WriteLine(string.Format("interceptor does something before invoke [{0}]...", @method));

            var retobj = @object.GetType().GetMethod(@method).Invoke(@object, parameters);

            Trace.WriteLine(string.Format("interceptor does something after invoke [{0}]...", @method));

            return retobj;
        }
    }

    public class BusinessProxy : IBusiness
    {
        private Interceptor _interceptor = new Interceptor();

        public int GetAge(int num)
        {
            return (int)this._interceptor.Invoke(new Business(), "GetAge", new object[]
            {
            num
            });
        }

        public void Test()
        {
            Interceptor arg_1C_0 = this._interceptor;
            object arg_1C_1 = new Business();
            string arg_1C_2 = "Test";
            object[] parameters = new object[0];
            arg_1C_0.Invoke(arg_1C_1, arg_1C_2, parameters);
        }
    }
}
