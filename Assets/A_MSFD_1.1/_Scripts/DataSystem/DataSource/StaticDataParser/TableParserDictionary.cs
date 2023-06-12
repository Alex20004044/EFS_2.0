using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public class TableParserDictionary : TableParserBase
    {
        public TableParserDictionary(Dictionary<string, string> dataTable, string[] header, string tableName) : base(dataTable, header, tableName)
        {
        }

        public override void Parse(string[] row)
        {
            dataTable.Add(DataSystemUtilities.PathCombineIgnoreEmptyParts(tableName, row[0]), row[1]);
        }
    }
}