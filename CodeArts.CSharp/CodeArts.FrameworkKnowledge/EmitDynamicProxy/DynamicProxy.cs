using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public class DynamicProxy
    {
        public static TInterface CreateProxyOfRealize<TInterface, TImp, TInterceptor>() where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase
        {
            return Invoke<TInterface, TImp, TInterceptor>();
        }

        public static TInterface CreateProxyOfRealize<TInterface, TImp, TInterceptor, TActionAttribute>() where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase where TActionAttribute : DynamicProxyActionBaseAttribute
        {
            return Invoke<TInterface, TImp, TInterceptor>(typeof(TActionAttribute));
        }

        public static TProxyClass CreateProxyOfInherit<TProxyClass, TInterceptor>() where TProxyClass : class, new() where TInterceptor : DynamicProxyInterceptorBase
        {
            return Invoke<TProxyClass, TProxyClass, TInterceptor>(null,true);
        }
        public static TProxyClass CreateProxyOfInherit<TProxyClass, TInterceptor, TActionAttribute>()  where TProxyClass : class,new() where TInterceptor : DynamicProxyInterceptorBase where TActionAttribute : DynamicProxyActionBaseAttribute
        {
            return Invoke<TProxyClass, TProxyClass, TInterceptor>(typeof(TActionAttribute),true);
        }

        private static TInterface Invoke<TInterface, TImp, TInterceptor>(Type actionAttributeType = null, bool inheritMode = false) where TImp : class, new() where TInterface : class where TInterceptor : DynamicProxyInterceptorBase
        {
            var impType = typeof(TImp);

            string nameOfAssembly = impType.Name + "ProxyAssembly";
            string nameOfModule = impType.Name + "ProxyModule";
            string nameOfType = impType.Name + "Proxy";

            var assemblyName = new AssemblyName(nameOfAssembly);

            var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assembly.DefineDynamicModule(nameOfModule);

            //var assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndSave);
            //var moduleBuilder = assembly.DefineDynamicModule(nameOfModule, nameOfAssembly + ".dll");

            TypeBuilder typeBuilder;
            if (inheritMode)
                typeBuilder = moduleBuilder.DefineType(nameOfType, TypeAttributes.Public, impType);
            else
                typeBuilder = moduleBuilder.DefineType(nameOfType, TypeAttributes.Public, null, new[] { typeof(TInterface) });

            InjectInterceptor<TImp, TInterceptor>(typeBuilder, actionAttributeType,inheritMode);

            var t = typeBuilder.CreateType();

            //assembly.Save(nameOfAssembly + ".dll");

            return Activator.CreateInstance(t) as TInterface;
        }

        private static void InjectInterceptor<TImp, TInterceptor>(TypeBuilder typeBuilder, Type actionAttributeType = null, bool inheritMode = false)
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

                MethodAttributes methodAttributes;

                if (inheritMode)
                    methodAttributes = MethodAttributes.Public | MethodAttributes.Virtual;
                else
                    methodAttributes = MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual | MethodAttributes.Final;

                var methodBuilder = typeBuilder.DefineMethod(method.Name, methodAttributes, CallingConventions.Standard, method.ReturnType, methodParameterTypes);

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
                    if (method.ReturnType.IsValueType)
                        ilMethod.Emit(OpCodes.Unbox_Any, method.ReturnType);
                    else
                        ilMethod.Emit(OpCodes.Castclass, method.ReturnType);
                    ilMethod.Emit(OpCodes.Ldloc_0);
                    ilMethod.Emit(OpCodes.Stloc_0);
                }

                // complete
                ilMethod.Emit(OpCodes.Ret);
            }
        }
    }
}