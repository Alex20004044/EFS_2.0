using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MSFD.Data
{
    public abstract class TableParserBase : ITableParser
    {
        protected Dictionary<string, string> dataTable;
        protected string[] header;
        protected string tableName;

        protected TableParserBase(Dictionary<string, string> dataTable, string[] header, string tableName)
        {
            this.dataTable = dataTable;
            this.header = header;
            this.tableName = tableName;
        }

        public abstract void Parse(string[] row);
    }
}