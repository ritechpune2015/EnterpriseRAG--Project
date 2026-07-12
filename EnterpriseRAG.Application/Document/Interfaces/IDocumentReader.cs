using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Document.Interfaces
{
    public interface IDocumentReader
    {
        bool CanRead(string extension);
        Task<string> ReadAsync(string filePath);
    }

}
