using Sirenix.Utilities;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace MSFD.Data
{
    public static class FileReader
    {
        public static string StaticDataBasePath => Application.streamingAssetsPath;
        public static string SaveDataPath
        {
            get
            {
#if UNITY_EDITOR
                return Application.dataPath + "/Data";
#endif
#if !UNITY_EDITOR
                return Application.persistentDataPath;
#endif
            }
        }
        public static string PathCombine(params string[] pathParts)
        {
            return string.Join("/", pathParts);
        }
        static string[] GetStaticFilePathsInDirectory(string directoryPath)
        {
            string path = PathCombine(StaticDataBasePath, directoryPath);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                throw new DirectoryNotFoundException($"Directory with path {path} not found");
            }

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            return directoryInfo.GetFiles("*.csv").Select(x => x.FullName).ToArray();
        }


        /// <summary>
        /// Return parsed by CSVParser dictionary. Ready to deserialize
        /// </summary>
        /// <param name="dataDirectoryPath"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetRawDataTable(string dataDirectoryPath)
        {
            Dictionary<string, string> rawDataTable = new Dictionary<string, string>();
            CSVParser parser = new CSVParser();

            foreach (var x in Resources.LoadAll<TextAsset>(dataDirectoryPath))
            {
                using StringReader reader = new StringReader(x.text);

                var rawData = parser.Parse(reader);
                rawData.ForEach(data => rawDataTable.Add(data.Key, data.Value));
                Resources.UnloadAsset(x);
            }
            return rawDataTable;
            /*            foreach (var x in GetStaticFilePathsInDirectory(dataDirectoryPath))
                        {
                            using (var reader = new StreamReader(x))
                            {
                                var rawData = parser.Parse(reader);
                                rawData.ForEach(data => rawDataTable.Add(data.Key, data.Value));
                            }
                        }*/
        }

        public static void SaveString(string path, string data)
        {
            path = PathCombine(SaveDataPath, path);

            string directoryPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            using var streamWriter = File.CreateText(path);
            streamWriter.Write(data);
        }

        public static string LoadString(string path)
        {
            path = PathCombine(SaveDataPath, path);
            //string directoryPath = Path.GetDirectoryName(path);
            if (!File.Exists(path))
                return null;
            return File.ReadAllText(path);
        }
    }
}