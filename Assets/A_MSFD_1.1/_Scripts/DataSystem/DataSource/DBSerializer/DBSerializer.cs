using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using static MSFD.Data.CSVParseConstants;
namespace MSFD.Data
{
    public static class DBSerializer
    {
        public static bool IsDataTypeCorrect(Type type)
        {
            return type == typeof(int)
                || type == typeof(float)
                || type == typeof(bool)
                || type == typeof(string[])
                || type == typeof(string);
        }

        public static string Serialize(IEnumerable<KeyValuePair<string, object>> dataTable)
        {
            //string result = "Path, Value\n";
            StringBuilder result = new StringBuilder();
            foreach (var x in dataTable)
            {
                if (x.Value.GetType().IsArray)
                {
                    result.Append(x.Key);
                    result.Append(separator);
                    result.Append(arrayPrefix);
                    result.Append(string.Join(separator, ((string[])x.Value)));
                }
                else
                {
                    result.Append(x.Key);
                    result.Append(separator);
                    result.Append(x.Value);
                }
                result.Append(nextLine);
            }
            return result.ToString();
        }
        public static IEnumerable<KeyValuePair<string, object>> Deserialize(string rawDataTable, DeserializeAction deserializeAction = null)
        {
            return Deserialize(rawDataTable.Split(nextLine).Where(x=>!x.IsNullOrWhitespace()), deserializeAction);
        }
        public static IEnumerable<KeyValuePair<string, object>> Deserialize(IEnumerable<string> rawDataTable, DeserializeAction deserializeAction = null)
        {
            return Deserialize(rawDataTable.Select((x) =>
                {
                    var rawData = x.Split(separator, 2);
                    return new KeyValuePair<string, string>(rawData[0], rawData[1]);
                }), deserializeAction);
        }
        public static IEnumerable<KeyValuePair<string, object>> Deserialize(IEnumerable<KeyValuePair<string, string>> rawDataTable, DeserializeAction deserializeAction = null)
        {
            List<KeyValuePair<string, object>> data = new List<KeyValuePair<string, object>>();

            foreach (var x in rawDataTable)
            {
                string path = x.Key;
                string rawValue = x.Value;

                if (int.TryParse(rawValue, out int intValue))
                {
                    data.Add(new KeyValuePair<string, object>(path, intValue));
                    deserializeAction?.intAction?.Invoke(path, intValue);
                }
                else if (float.TryParse(rawValue, NumberStyles.Float, CultureInfo.InvariantCulture, out float floatValue))
                {
                    data.Add(new KeyValuePair<string, object>(path, floatValue));
                    deserializeAction?.floatAction?.Invoke(path, floatValue);
                }
                else if(bool.TryParse(rawValue, out bool boolValue))
                {
                    data.Add(new KeyValuePair<string, object>(path, boolValue));
                    deserializeAction?.boolAction?.Invoke(path, boolValue);
                }
                else if (rawValue.StartsWith(arrayPrefix))
                {
                    var stringArrayValue = rawValue.Substring(1).Split(new string[] { arraySeparator }, StringSplitOptions.None);
                    data.Add(new KeyValuePair<string, object>(path, stringArrayValue));
                    deserializeAction?.stringArrayAction?.Invoke(path, stringArrayValue);
                }
                else
                {
                    data.Add(new KeyValuePair<string, object>(path, rawValue));
                    deserializeAction?.stringAction?.Invoke(path, rawValue);
                }
            }
            return data;
        }

        public class DeserializeAction
        {
            public Action<string, int> intAction { get; }
            public Action<string, float> floatAction { get; }
            public Action<string, bool> boolAction { get; }
            public Action<string, string[]> stringArrayAction { get; }
            public Action<string, string> stringAction { get; }

            public DeserializeAction(Action<string, int> intAction = null,
                Action<string, float> floatAction = null,
                Action<string, bool> boolAction = null,
                Action<string, string[]> stringArrayAction = null,
                Action<string, string> stringAction = null)
            {
                this.intAction = intAction;
                this.floatAction = floatAction;
                this.boolAction = boolAction;
                this.stringArrayAction = stringArrayAction;
                this.stringAction = stringAction;
            }
        }


    }
}