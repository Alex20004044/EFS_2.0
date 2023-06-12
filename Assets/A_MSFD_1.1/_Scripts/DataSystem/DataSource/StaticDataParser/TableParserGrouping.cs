using Sirenix.Utilities;
using System.Collections.Generic;
using ParseConstants = MSFD.Data.CSVParseConstants;
using static MSFD.Data.DataSystemUtilities;

namespace MSFD.Data
{
    public class TableParserGrouping : TableParserBase
    {
        string parsingEntity = "PARSING_ERROR";
        string groupAttribute = "PARSING_ERROR";
        bool isInGroup;

        public TableParserGrouping(Dictionary<string, string> dataTable, string[] header, string tableName) : base(dataTable, header, tableName)
        {
        }

        public override void Parse(string[] row)
        {
            DefineParsingEntity(row[0]);

            for (int i = 1; i < header.Length; i++)
            {
                string currentHeader = header[i];
                string currentCell = row[i];

                if (IsCommentColumn(currentHeader))
                    continue;

                string dataPath = PathCombine(tableName, parsingEntity);

                if (isInGroup)
                {
                    if (IsEndOfGroup(currentHeader))
                    {
                        isInGroup = false;
                        continue;
                    }
                    else if (IsGroupColumn(currentHeader))
                    {
                        isInGroup = false;
                        i--;
                        continue;
                    }
                    else if (IsEmptyCell(currentCell))
                        continue;
                    else
                    {
                        dataPath = PathCombine(dataPath, groupAttribute);
                        //Add all headers of current group (access via group path)
                        AddAttributeName(dataPath, currentHeader);
                        dataPath = PathCombine(dataPath, currentHeader);
                    }
                }
                else
                {
                    if (IsGroupColumn(currentHeader))
                    {
                        isInGroup = true;
                        groupAttribute = PathCombineIgnoreEmptyParts(FormatGroupName(currentHeader), currentCell);
                        AddAttributeName(dataPath, groupAttribute);
                        continue;
                    }
                    else if (IsEmptyCell(currentCell))
                        continue;
                    else
                    {
                        AddAttributeName(dataPath, currentHeader);
                        dataPath = PathCombine(dataPath, currentHeader);
                    }
                }

                AddCellData(dataPath, currentCell);
            }
        }


        void DefineParsingEntity(string row0)
        {
            if (row0.IsNullOrWhitespace())
                return;
            parsingEntity = row0;
            //Create entities list Tanks: Red, Green...
            AddAttributeName(tableName, parsingEntity);
        }

        void AddAttributeName(string entityPath, string attributeName)
        {
            if (!TryCreateArrayField(entityPath, attributeName))
                AddFieldToArrayFieldIfNotExitst(entityPath, attributeName);
        }
        bool TryCreateArrayField(string entityPath, string attributeName)
        {
            return dataTable.TryAdd(entityPath, ParseConstants.arrayPrefix + attributeName);
        }
        void AddFieldToArrayFieldIfNotExitst(string entityPath, string attributeName)
        {
            if (!dataTable[entityPath].Contains(attributeName))
                dataTable[entityPath] += ParseConstants.arraySeparator + attributeName;
        }


        bool IsCommentColumn(string columnHeader)
        {
            return columnHeader.StartsWith(ParseConstants.commentColumn);
        }

        static bool IsEndOfGroup(string columnHeader)
        {
            return columnHeader == ParseConstants.groupEnd;
        }
        static bool IsGroupColumn(string columnHeade)
        {
            return columnHeade.EndsWith(ParseConstants.groupAttribute);
        }
        static bool IsEmptyCell(string cell)
        {
            return cell.IsNullOrWhitespace();
        }
        static string FormatGroupName(string headerColumn)
        {
            if (!headerColumn.Contains(ParseConstants.includeGroupNamePrefix))
                return "";
            return headerColumn.Replace(ParseConstants.groupAttribute, "").Trim();
        }

        void AddCellData(string dataPath, string currentCell)
        {
            if (dataTable.ContainsKey(dataPath))
            {
                string data = dataTable[dataPath];
                if (!data.StartsWith(ParseConstants.arrayPrefix))
                    data = ParseConstants.arrayPrefix + data;
                dataTable[dataPath] = data + ParseConstants.arraySeparator + currentCell;
            }
            else
                dataTable.Add(dataPath, currentCell);
        }
    }
}