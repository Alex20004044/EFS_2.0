using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace MSFD
{
    public static class MessengerEventStore
    {
        public static List<string> GetEvents()
        {
            List<string> eventsWithSource;
            return GetEvents(out eventsWithSource);
        }
        public static List<string> GetEvents(out List<string> eventsWithSource)
        {
            List<string> events = new List<string>();
            eventsWithSource = new List<string>();
            foreach (Type container in GetContainerTypes())
            {
                foreach (var x in GetFieldValues(container))
                {
                    events.Add(x.Value);
                    eventsWithSource.Add(container.Name + "/" + x.Value);
                }
            }
            return events;
        }
        public static string RemoveSourceFromEvent(string eventName)
        {
            return eventName.Substring(eventName.IndexOf("/") + 1);
        }
        public static Dictionary<string, string> GetFieldValues(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.FieldType == typeof(string) && f.GetCustomAttribute<ObsoleteAttribute>() == null)
                      .ToDictionary(f => f.Name,
                                    f => (string)f.GetValue(null));
        }
        static List<Type> GetContainerTypes()
        {
            return EventContainerTypes.GetEventContainers();
        }
    }
}