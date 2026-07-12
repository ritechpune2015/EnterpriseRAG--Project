using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Document.Interfaces;
using EnterpriseRAG.Application.Embeddings.Interfaces;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Infrastructure.Documents;
using EnterpriseRAG.Infrastructure.Embedding;
using EnterpriseRAG.Infrastructure.LLM;
using EnterpriseRAG.Infrastructure.Qdrant;
using Microsoft.Extensions.DependencyInjection;
namespace EnterpriseRAG.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services)
        {
            services.AddSingleton<IEmbeddingService,
                EmbeddingService>();

            services.AddSingleton<IQdrantService,
                QdrantService>();

            services.AddScoped<IChatService,
                OllamaService>();

            services.AddScoped<IChunkingService, ChunkingService>();
            services.AddScoped<IDocumentService,DocumentService>();
            services.AddScoped<IDocumentReader, PdfReader>();

            services.AddScoped<IDocumentReader, WordReader>();

            services.AddScoped<IDocumentReader, EnterpriseRAG.Infrastructure.Documents.TextReader>();

            services.AddScoped<IDocumentReader, CsvReader>();

            services.AddScoped<IDocumentReaderFactory,
                DocumentReaderFactory>();

            return services;
        }
    }

}
