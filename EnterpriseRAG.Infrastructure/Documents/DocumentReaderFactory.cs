using EnterpriseRAG.Application.Document.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class DocumentReaderFactory : IDocumentReaderFactory
    {
        private readonly IEnumerable<IDocumentReader> _readers;

        public DocumentReaderFactory(
            IEnumerable<IDocumentReader> readers)
        {
            _readers = readers;
        }

        public IDocumentReader GetReader(string extension)
        {
            var reader =
                _readers.FirstOrDefault(
                    x => x.CanRead(extension));

            if (reader == null)
                throw new Exception("Reader not found.");

            return reader;
        }
    }

}
