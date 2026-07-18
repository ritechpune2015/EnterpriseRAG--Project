using EnterpriseRAG.Application.Retrieval.DTO;
using EnterpriseRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Qdrant
{
    public interface IQdrantService
    {
        Task CreateCollectionAsync();
        Task IndexChunkAsync(DocumentChunk chunk);
        Task IndexChunksAsync(IEnumerable<DocumentChunk> chunks);
        Task<List<SearchResultDto>> SearchAsync(float[] embedding, int topK);
    }
}
