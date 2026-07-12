using EnterpriseRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Document.Interfaces
{
    public interface IChunkingService
    {
        List<DocumentChunk> ChunkDocument(Guid documentId,string text);
    }
}
