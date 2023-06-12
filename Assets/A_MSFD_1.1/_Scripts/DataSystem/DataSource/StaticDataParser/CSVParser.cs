using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MSFD.Data
{
    public class CSVParser
    {
        Dictionary<string, string> dataTable;
        string[] header;
        string[] currentRow;
        string tableName;
        string attribute;

        ITableParser tableParser;
        public Dictionary<string, string> Parse(TextReader fileReader)
        {
            dataTable = new Dictionary<string, string>();

            var csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                AllowComments = true,
                Comment = CSVParseConstants.commentRowPrefix
            };

            using var csv = new CsvReader(fileReader, csvConfig);

            ReadAttributes();
            ReadHeader();
            DefineParseModeAndTableName();

            while (csv.Read())
            {
                currentRow = csv.Parser.Record;
                tableParser.Parse(currentRow);
            }
            return dataTable;

            void ReadAttributes()
            {
                fileReader.Peek();
                if ((char)fileReader.Peek() == CSVParseConstants.commentRowPrefix)
                    attribute = fileReader.ReadLine();
            }
            void ReadHeader()
            {
                csv.Read();
                csv.ReadHeader();
                header = csv.HeaderRecord;
            }
        }
        public string GetFileAttribute()
        {
            return attribute;
        }
        void DefineParseModeAndTableName()
        {
            if (IsGroupColumn(header[0]))
            {
                tableName = RemoveGroupAttribute(header[0]);
                tableParser = new TableParserGrouping(dataTable, header, tableName);
            }
            else
            {
                tableName = header[0];
                tableParser = new TableParserDictionary(dataTable, header, tableName);
                if (header.Length != 2)
                    throw new FormatException("Header in KeyValue ParseMode must has 2 columns");
            }
        }
        static bool IsGroupColumn(string header)
        {
            return header.EndsWith(CSVParseConstants.groupAttribute);
        }
        static string RemoveGroupAttribute(string header)
        {
            return header.Replace(CSVParseConstants.groupAttribute, "").Trim();
        }
    }
}