using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeArts.FrameworkKnowledge.EmitDynamicProxy
{
    public class ServiceProvider
    {
        public object GetService(string assemblyName, string typeName)
        {
            Type type = Assembly.Load(assemblyName).GetType(typeName);
            return Activator.CreateInstance(type);
        }
    }
}
