using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Conversations.Interfaces;
using EnterpriseRAG.Application.Document.Interfaces;
using EnterpriseRAG.Application.Embeddings.Interfaces;
using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Application.Retrieval.Interfaces;
using EnterpriseRAG.Infrastructure.Context;
using EnterpriseRAG.Infrastructure.Conversations.Repositories;
using EnterpriseRAG.Infrastructure.Documents;
using EnterpriseRAG.Infrastructure.Embedding;
using EnterpriseRAG.Infrastructure.LLM;
using EnterpriseRAG.Infrastructure.Qdrant;
using EnterpriseRAG.Infrastructure.Retrieval.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace EnterpriseRAG.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,IConfiguration config)
        {
            services.AddSingleton<IEmbeddingService,
                EmbeddingService>();

            services.AddSingleton<IQdrantService,
                QdrantService>();

            

            services.AddScoped<IChunkingService, ChunkingService>();
            services.AddScoped<IDocumentService,DocumentService>();
            services.AddScoped<IDocumentReader, PdfReader>();

            services.AddScoped<IDocumentReader, WordReader>();

            services.AddScoped<IDocumentReader, EnterpriseRAG.Infrastructure.Documents.TextReader>();

            services.AddScoped<IDocumentReader, CsvReader>();

            services.AddScoped<IDocumentReaderFactory,
                DocumentReaderFactory>();

            services.AddSingleton<IQdrantService, QdrantService>();

            services.AddScoped<IRetrievalService, RetrievalService>();
            services.AddHttpClient<ILLMService, OllamaService>(client =>
            {
                client.BaseAddress = new Uri(config["Ollama:BaseUrl"]);
                client.Timeout = TimeSpan.FromMinutes(10);
            });

            services.AddDbContextPool<ApplicationDbContext>(opt=>opt.UseSqlServer(config.GetConnectionString("scon")));

            services.AddScoped<IConversationRepository, ConversationRepository>();


            return services;
        }
    }

}
