using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Document.Interfaces
{
    public interface IDocumentReaderFactory
    {
       IDocumentReader GetReader(string extension);
    }

}
