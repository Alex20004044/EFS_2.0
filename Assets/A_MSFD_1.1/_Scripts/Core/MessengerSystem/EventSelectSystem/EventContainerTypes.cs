using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
namespace MSFD
{
    public static class EventContainerTypes
    {
        static List<Type> containers;
        static List<Type> GetProjectEventContainers()
        {
            List<Type> containerTypes = new List<Type>();
            //Write your code hear
            //containerTypes.Add(typeof(GameEvents_CH));
            //...
            return containerTypes;
        }

        public static List<Type> GetEventContainers()
        {
            if (containers != null)
                return containers;
            containers = new List<Type>();
            /*            containers.Add(typeof(GameEvents));
                        containers.Add(typeof(SystemEvents));
                        containers.Add(typeof(UIEvents));
                        List<Type> specificContainers = GetProjectEventContainers();
                        if (specificContainers != null)
                        {
                            containers.AddRange(specificContainers);
                        }*/

            /*            string definedIn = typeof(MessengerEventContainerAttribute).Assembly.GetName().Name;
                        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                            // Note that we have to call GetName().Name.  Just GetName() will not work.  The following
                            // if statement never ran when I tried to compare the results of GetName().
                            if ((!assembly.GlobalAssemblyCache) && ((assembly.GetName().Name == definedIn) || assembly.GetReferencedAssemblies().Any(a => a.Name == definedIn)))
                                foreach (Type type in assembly.GetTypes())
                                    if (type.GetCustomAttributes(typeof(MessengerEventContainerAttribute), true).Length > 0)
                                    {
                                        containers.Add(type);
                                    }*/
            containers.AddRange(GetTypesWithAttribute<MessengerEventContainerAttribute>(typeof(MessengerEventContainerAttribute).Assembly));
            return containers;
        }

        static List<Type> GetTypesWithAttribute<T>(Assembly assembly)
        {
            List<Type> types = new List<Type>();

            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(T), true).Length > 0)
                    types.Add(type);
            }

            return types;
        }

/*        var allAttributesInAppDomain = GetAllAttributesInAppDomain<MessengerEventContainerAttribute>();
        var allAttributesInAppDomainFlattened = allAttributesInAppDomain.SelectMany(c => c);
            foreach (var x in allAttributesInAppDomainFlattened)
                containers.Add(x.GetType());*/

        private static IEnumerable<IEnumerable<T>> GetAllAttributesInAppDomain<T>()
        {
            var definedIn = typeof(T).Assembly.GetName().Name;
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var res = assemblies.AsParallel()
                 .Where(assembly => (!assembly.GlobalAssemblyCache) && ((assembly.GetName().Name == definedIn) ||
                                                                        assembly.GetReferencedAssemblies()
                                                                            .Any(a => a.Name == definedIn))
                     )
                 .SelectMany(c => c.GetTypes())
                 .Select(type => type.GetCustomAttributes(typeof(T), true)
                     .Cast<T>()
                     )
                 .Where(c => c.Any());

            return res;
        }
    }
}