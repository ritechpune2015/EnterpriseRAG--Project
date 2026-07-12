using DocumentFormat.OpenXml.Packaging;
using EnterpriseRAG.Application.Document.Interfaces;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class WordReader : IDocumentReader
    {
        public bool CanRead(string extension)
        {
            return extension.Equals(".docx",
                StringComparison.OrdinalIgnoreCase);
        }

        public Task<string> ReadAsync(string filePath)
        {
            using var document =
                WordprocessingDocument.Open(filePath, false);

            var body =
                document.MainDocumentPart!
                        .Document
                        .Body;

            return Task.FromResult(body?.InnerText ?? "");
        }

    }
}