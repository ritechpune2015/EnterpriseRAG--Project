using EnterpriseRAG.Application.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class CsvReader : IDocumentReader
    {
        public bool CanRead(string extension)
        {
            return extension.Equals(".csv",
                StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ReadAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);

            using var csv =new CsvHelper.CsvReader(reader,
                    CultureInfo.InvariantCulture);

            var rows = csv.GetRecords<dynamic>();

            var text = "";

            foreach (var row in rows)
            {
                text += row.ToString();
                text += Environment.NewLine;
            }

            return await Task.FromResult(text);
        }
    }

}
