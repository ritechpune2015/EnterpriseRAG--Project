using EnterpriseRAG.Application.Embeddings.Interfaces;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Application.Retrieval.DTO;
using EnterpriseRAG.Application.Retrieval.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Retrieval.Services
{
    public class RetrievalService : IRetrievalService
    {
        private readonly IEmbeddingService _embeddingService;
        private readonly IQdrantService _qdrantService;
        public RetrievalService(IEmbeddingService embeddingService, IQdrantService qdrantService)
        {
            _embeddingService = embeddingService;
            _qdrantService = qdrantService;
        }
        public async Task<List<SearchResultDTO>> SearchAsync(string question, int topK = 5)
        {
            var embedding = await _embeddingService.GenerateEmbeddingAsync(question);
            return await _qdrantService.SearchAsync(embedding, topK);
        }
    }

}
