using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperBotManagerBase.Utils
{
    public static class AssemblyUtils
    {
        public static IEnumerable<Type> GetClassesWithAttribute<TAttribute>(this Assembly assembly) 
            where TAttribute : Attribute
        {
            return assembly.GetTypes().Where(t => t.IsDefined(typeof(TAttribute)));
        }

        public static List<Assembly> LoadAssembliesContainingName(string name)
        {
            var loadedAssemblies = new List<Assembly>();
            foreach(string dll in Directory.GetFiles(System.AppDomain.CurrentDomain.BaseDirectory, "*.dll", SearchOption.AllDirectories))
            {
                if(!dll.Contains(name))
                    continue;
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                    loadedAssemblies.Add(loadedAssembly);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return loadedAssemblies;
        }
    }

}
