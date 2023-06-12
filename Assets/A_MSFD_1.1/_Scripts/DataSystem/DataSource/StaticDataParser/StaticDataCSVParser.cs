using System.Collections.Generic;
using CsvHelper;
using System.IO;
using Sirenix.Utilities;
using ParseConstants = MSFD.Data.CSVParseConstants;

namespace MSFD.Data
{
    public class StaticDataCSVParser
    {
        public static Dictionary<string, string> Parse(TextReader fileReader)
        {
            return new StaticDataCSVParser().ParseCSV(fileReader);
        }
        static string Separator => ParseConstants.pathSeparator.ToString();

        Dictionary<string, string> dataTable;
        string tableName;
        public Dictionary<string, string> ParseCSV(TextReader fileReader)
        {
            dataTable = new Dictionary<string, string>();
            using var csv = new CsvReader(fileReader, System.Globalization.CultureInfo.InvariantCulture);

            csv.Read();
            csv.ReadHeader();
            string[] header = GetHeader(csv);

            tableName = RemoveGroupAttribute(header[0]);

            string currentEntityName = "ERROR";
            string groupAttribute = "ERROR";
            bool isInGroup;
            while (csv.Read())
            {
                isInGroup = false;
                //Define current entity
                DefineCurrentEntity(csv.GetField(0), ref currentEntityName);

                for (int i = 1; i < header.Length; i++)
                {
                    //SkipComments
                    if (header[i].Contains(ParseConstants.commentColumn))
                        continue;

                    string dataPath = DataSystemUtilities.PathCombine(tableName, currentEntityName);
                    string value;
                    if (isInGroup)
                    {
                        if (IsEndOfGroup(header[i]))
                        {
                            isInGroup = false;
                            continue;
                        }
                        else if (IsGroupColumn(header[i]))
                        {
                            isInGroup = false;
                            i--;
                            continue;
                        }
                        else
                        {
                            if (csv.GetField(i).IsNullOrWhitespace())
                                continue;
                            //Add value childs
                            AddAttributeName(dataPath + groupAttribute, header[i]);

                            dataPath += groupAttribute + Separator + header[i];
                            value = csv.GetField(i);
                        }
                    }
                    else
                    {
                        if (IsGroupColumn(header[i]))
                        {
                            isInGroup = true;
                            groupAttribute = TryHideGroupName(header[i]) + csv.GetField(i);

                            //Add attributeName
                            AddAttributeName(dataPath.TrimEnd(ParseConstants.pathSeparator), groupAttribute);

                            continue;
                        }
                        else
                        {
                            //Add attributeName
                            AddAttributeName(dataPath.TrimEnd(ParseConstants.pathSeparator), header[i]);

                            dataPath += header[i];
                            value = csv.GetField(i);
                        }
                    }
                    //Skip null value
                    if (value.IsNullOrWhitespace())
                        continue;
                    //Add reference label to value
                    if (IsReferenceColumn(header[i]))
                    {
                        value = AddReferenceLabel(value);
                        //Remove reference char from header
                        dataPath = dataPath.Replace(ParseConstants.refChar, "").TrimEnd();
                    }

                    if (dataTable.ContainsKey(dataPath))
                    {
                        dataTable[dataPath] += ", " + value;
                    }
                    else
                    {
                        dataTable.Add(dataPath, value);
                    }
                }

            }
            return dataTable;
        }

        static string[] GetHeader(CsvReader csv)
        {
            return csv.HeaderRecord;
        }


        void DefineCurrentEntity(string leftCellInRow, ref string currentEntityName)
        {
            if (leftCellInRow.IsNullOrWhitespace())
                return;
            currentEntityName = leftCellInRow;
            //Create entities list Tanks: Red, Green...
            AddAttributeName(tableName, currentEntityName);
        }





        static bool IsEndOfGroup(string str)
        {
            return str == ParseConstants.groupEnd;
        }
        static string TryHideGroupName(string str)
        {
            if (!str.Contains(ParseConstants.includeGroupNamePrefix))
                return "";
            return RemoveGroupAttribute(str.Replace(ParseConstants.includeGroupNamePrefix, "")) + Separator;
        }

        static bool IsGroupColumn(string header)
        {
            return header.EndsWith(ParseConstants.groupAttribute);
        }
        static string RemoveGroupAttribute(string header)
        {
            return header.Replace(ParseConstants.groupAttribute, "").Trim();
        }
        void AddAttributeName(string entityPath, string attributeName)
        {
            if (dataTable.ContainsKey(entityPath))
            {
                if (!dataTable[entityPath].Contains(attributeName))
                    dataTable[entityPath] += ParseConstants.arraySeparator + attributeName;
            }
            else
                dataTable.Add(entityPath, ParseConstants.arrayPrefix + attributeName);
        }
        static bool IsReferenceColumn(string str)
        {
            return str.Contains(ParseConstants.refChar);
        }
        static string AddReferenceLabel(string str)
        {
            return ParseConstants.refChar + str;
        }
    }
}