using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLua;
using System.Linq;
using System.Text;

namespace Demo.NLua
{
    /**
     * reference
     * https://github.com/NLua/NLua
     */
    [TestClass]
    public class NLuaTest
    {
        [TestMethod]
        public void UTF8String()
        {
            using (Lua lua = new Lua())
            {
                lua.State.Encoding = Encoding.UTF8;
                lua.DoString("res = '§¶§Ñ§Û§Ý'");
                string res = (string)lua["res"];

                Assert.AreEqual("§¶§Ñ§Û§Ý", res);
            }
        }

        [TestMethod]
        public void Evaluating_simple_expressions()
        {
            using (Lua state = new Lua())
            {
                // Lua can return multiple values, for this reason DoString return a array of objects
                var res = (long)state.DoString("return 10 + 3*(5 + 2)")[0];

                Assert.AreEqual(31, res);
            }
        }

        [TestMethod]
        public void Passing_raw_values_to_the_state()
        {
            using (Lua state = new Lua())
            {
                state["x"] = 12.0; // Create a global value 'x' 
                var res = (double)state.DoString("return 10 + x*(5 + 2)")[0];

                Assert.AreEqual(94, res);
            }
        }

        [TestMethod]
        public void Retrieving_global_values()
        {
            using (Lua state = new Lua())
            {
                state.DoString("y = 10 + 5 + 2");
                double y = (double)state["y"]; // Retrieve the value of y

                Assert.AreEqual(17, y);
            }
        }

        [TestMethod]
        public void Retrieving_Lua_functions()
        {
            using (Lua state = new Lua())
            {
                state.DoString(@"
	                function ScriptFunc (val1, val2)
		                if val1 > val2 then
			                return val1 + 1
		                else
			                return val2 - 1
		                end
	                end
	                ");
                var scriptFunc = state["ScriptFunc"] as LuaFunction;
                var res = (long)scriptFunc.Call(3, 5).First();
                // LuaFunction.Call will also return a array of objects, since a Lua function
                // can return multiple values

                Assert.AreEqual(4, res);
            }
        }

        [TestMethod]
        public void Using_the_DotNet_objects_GetProperty()
        {
            using (Lua state = new Lua())
            {
                //Passing .NET objects to the state:

                SomeClass obj = new SomeClass("Param");
                state["obj"] = obj; // Create a global value 'obj' of .NET type SomeClass 
                                    // This could be any .NET object, from BCL or from your assemblies

                //Calling properties: You can get(or set) any property using . notation from Lua.
                state.DoString(@"
	                res5 = obj.StrProperty
	                ");

                var res = state["res5"] as string;

                Assert.AreEqual("Param", res);
            }
        }

        [TestMethod]
        public void Using_the_DotNet_objects_CallMethods()
        {
            using (Lua state = new Lua())
            {
                //Passing .NET objects to the state:

                SomeClass obj = new SomeClass("Param");
                state["obj"] = obj; // Create a global value 'obj' of .NET type SomeClass 
                                    // This could be any .NET object, from BCL or from your assemblies

                //Calling properties: You can get(or set) any property using . notation from Lua.
                state.DoString(@"
	                res1 = obj:Func1()
	                res2 = obj:AnotherFunc(10, 'hello')
	                ");
                //Calling static methods: You can call static methods using only the class name and the.notation from Lua.

                var res1 = state["res1"] as string;
                var res2 = state["res2"] as string;

                Assert.AreEqual("1", res1);
                Assert.AreEqual("10hello", res2);
            }
        }

        [TestMethod]
        public void Using_the_DotNet_objects_LoadClass()
        {
            using (Lua state = new Lua())
            {
                /*
                 Using .NET assemblies inside Lua:
                To access any .NET assembly to create objects, events etc inside Lua you need to ask NLua to use CLR as a Lua package. To do this just use the method LoadCLRPackage and use the import function inside your Lua script to load the Assembly.
                 */

                state.LoadCLRPackage();
                state.DoString(@" 
                     import ('Demo.NLua', 'Demo.NLua')
                     ");
                // import will load any .NET assembly and they will be available inside the Lua context.

                //Creating.NET objects: To create object you only need to use the class name with the().

                state.DoString(@"
	                obj = SomeClass() -- you can suppress default values.
	                res1 = obj:Func1()
	                res2 = SomeClass.StaticMethod(4)
	                ");
                //Calling instance methods: To call instance methods you need to use the : notation, you can call methods from objects passed to Lua or to objects created inside the Lua context.

                var res1 = state["res1"] as string;
                var res2 = state["res2"] as string;
                Assert.AreEqual("1", res1);
                Assert.AreEqual("4", res2);
            }
        }

        [TestMethod]
        public void Using_the_DotNet_objects_LoadClass2()
        {
            using (Lua state = new Lua())
            {
                /*
                 Using .NET assemblies inside Lua:
                To access any .NET assembly to create objects, events etc inside Lua you need to ask NLua to use CLR as a Lua package. To do this just use the method LoadCLRPackage and use the import function inside your Lua script to load the Assembly.
                 */

                state.LoadCLRPackage();
                state.DoString(@" 
                     import ('Newtonsoft.Json')
                     ");
                // import will load any .NET assembly and they will be available inside the Lua context.

                state.DoString(@"
	                res = JsonConvert.SerializeObject(123)
	                ");
                //Calling instance methods: To call instance methods you need to use the : notation, you can call methods from objects passed to Lua or to objects created inside the Lua context.

                var res1 = state["res"] as string;
                Assert.AreEqual("123", res1);
            }
        }
    }

    public class SomeClass
    {
        public SomeClass()
        {
        }

        public SomeClass(string v)
        {
            this.StrProperty = v;
        }

        public int MyProperty { get; set; }
        public string StrProperty { get; set; }

        public string Func1()
        {
            return "1";
        }

        public string AnotherFunc(int a, string b)
        {
            return a + b;
        }

        public static string StaticMethod(int a)
        {
            return a.ToString();
        }
    }
}
