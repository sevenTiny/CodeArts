using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public class DynamicProxy
    {
        public static TInterface Create<TInterface, TImp, TInterceptor>() where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase
        {
            return Invoke<TInterface, TImp, TInterceptor>();
        }

        public static TInterface Create<TInterface, TImp, TInterceptor, TActionAttribute>() where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase where TActionAttribute : DynamicProxyActionBaseAttribute
        {
            return Invoke<TInterface, TImp, TInterceptor>(typeof(TActionAttribute));
        }

        private static TInterface Invoke<TInterface, TImp, TInterceptor>(Type actionAttributeType = null) where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase
        {
            var impType = typeof(TImp);

            string nameOfAssembly = impType.Name + "ProxyAssembly";
            string nameOfModule = impType.Name + "ProxyModule";
            string nameOfType = impType.Name + "Proxy";

            var assemblyName = new AssemblyName(nameOfAssembly);

            //var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            //var moduleBuilder = assembly.DefineDynamicModule(nameOfModule);

            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            var moduleBuilder = assembly.DefineDynamicModule(nameOfModule, nameOfAssembly + ".dll");

            var typeBuilder = moduleBuilder.DefineType(nameOfType, TypeAttributes.Public, null, new[] { typeof(TInterface) });

            InjectInterceptor<TImp, TInterceptor>(typeBuilder, actionAttributeType);

            var t = typeBuilder.CreateType();

            assembly.Save(nameOfAssembly + ".dll");

            return Activator.CreateInstance(t) as TInterface;
        }

        private static void InjectInterceptor<TImp, TInterceptor>(TypeBuilder typeBuilder, Type actionAttributeType = null)
        {
            var interceptorType = typeof(TInterceptor);
            // ---- define fields ----

            var fieldInterceptor = typeBuilder.DefineField("_interceptor", interceptorType, FieldAttributes.Private);

            // ---- define costructors ----

            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, null);
            var ilOfCtor = constructorBuilder.GetILGenerator();

            ilOfCtor.Emit(OpCodes.Ldarg_0);
            ilOfCtor.Emit(OpCodes.Newobj, interceptorType.GetConstructor(new Type[0]));
            ilOfCtor.Emit(OpCodes.Stfld, fieldInterceptor);
            ilOfCtor.Emit(OpCodes.Ret);

            // ---- define methods ----

            var methodsOfType = typeof(TImp).GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var method in methodsOfType)
            {
                var methodParameterTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();

                var methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final, CallingConventions.Standard, method.ReturnType, methodParameterTypes);

                var ilMethod = methodBuilder.GetILGenerator();

                if (actionAttributeType != null && method.GetCustomAttribute(actionAttributeType) != null)
                {
                    ilMethod.Emit(OpCodes.Newobj, actionAttributeType.GetConstructor(new Type[0]));
                    ilMethod.Emit(OpCodes.Call, actionAttributeType.GetMethod("Before"));
                }

                ilMethod.Emit(OpCodes.Ldarg_0);
                ilMethod.Emit(OpCodes.Ldfld, fieldInterceptor);

                // create instance of T
                ilMethod.Emit(OpCodes.Newobj, typeof(TImp).GetConstructor(new Type[0]));
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
                        ilMethod.Emit(OpCodes.Box, methodParameterTypes[j]);

                        ilMethod.Emit(OpCodes.Stelem_Ref);
                    }
                    ilMethod.Emit(OpCodes.Ldloc, parameters);
                }

                // call Invoke() method of Interceptor
                ilMethod.Emit(OpCodes.Callvirt, interceptorType.GetMethod("Invoke"));

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
}