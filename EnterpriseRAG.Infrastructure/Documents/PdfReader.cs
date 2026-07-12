using EnterpriseRAG.Application.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UglyToad.PdfPig;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class PdfReader : IDocumentReader
    {
        public bool CanRead(string extension)
        {
            return extension.Equals(".pdf",
          StringComparison.OrdinalIgnoreCase);
        }

        public Task<string> ReadAsync(string filePath)
        {
            var text = "";
            using var document =PdfDocument.Open(filePath);

            foreach (var page in document.GetPages())
            {
                text += page.Text;
                text += Environment.NewLine;
            }

           return Task.FromResult(text);
        }
    }
}

