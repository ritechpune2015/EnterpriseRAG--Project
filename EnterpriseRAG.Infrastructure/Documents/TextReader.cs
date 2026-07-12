using EnterpriseRAG.Application.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class TextReader : IDocumentReader
    {
        public bool CanRead(string extension)
        {
            return extension.Equals(".txt",
                StringComparison.OrdinalIgnoreCase);
        }

        public async Task<string> ReadAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }
    }

}
