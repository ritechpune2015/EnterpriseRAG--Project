using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Embeddings.Interfaces
{
    public interface IEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string text);
    }
}
