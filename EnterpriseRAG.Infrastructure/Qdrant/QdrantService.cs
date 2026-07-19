using EnterpriseRAG.Application.Qdrant;
using EnterpriseRAG.Application.Retrieval.DTO;
using EnterpriseRAG.Domain.Entities;
using EnterpriseRAG.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Qdrant.Client;
using Qdrant.Client.Grpc;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Qdrant
{
    public class QdrantService : IQdrantService
    {
        private readonly QdrantClient _client;
        private readonly QdrantOptions _options;
        public QdrantService(IOptions<QdrantOptions> options)
        {
            _options = options.Value;
            _client =new QdrantClient(_options.Host,_options.Port);
        }

        public async Task CreateCollectionAsync()
        {
            bool exists = await _client.CollectionExistsAsync(_options.CollectionName);
            
            if (exists)
                return;

            await _client.CreateCollectionAsync(collectionName: _options.CollectionName, vectorsConfig: new VectorParams
            {
                Size = 384,
                Distance =Distance.Cosine
            });
        }

        public async Task IndexChunkAsync(DocumentChunk chunk)
        {
            await _client.UpsertAsync(collectionName: _options.CollectionName,
                points:
                [
                  new PointStruct
                  {
                    Id =  (ulong)Math.Abs(chunk.Id.GetHashCode()),
                    Vectors = chunk.Embedding,
                    Payload =
                        {
                        ["documentId"] =chunk.DocumentId.ToString(),
                        ["documentname"]=chunk.DocumentName,
                        ["chunkNumber"] =chunk.ChunkNumber,
                        ["content"] =chunk.Content,
                        ["pagenumber"]=chunk.PageNumber,
                        ["startindex"]=chunk.StartIndex,
                        ["endindex"]=chunk.EndIndex,
                        ["length"] = chunk.Length
                }
            }
                ]);

        }

        public async Task IndexChunksAsync(IEnumerable<DocumentChunk> chunks)
        {
            var points = new List<PointStruct>();
            foreach (var chunk in chunks)
            {
                points.Add(
                    new PointStruct
                    {
                        Id =
                        (ulong)Math.Abs(
                        chunk.Id.GetHashCode()),
                        Vectors = chunk.Embedding,
                        Payload =
                        {
                        ["documentId"] =chunk.DocumentId.ToString(),
                        ["documentname"]=chunk.DocumentName,
                        ["chunkNumber"] =chunk.ChunkNumber,
                        ["content"] =chunk.Content,
                        ["pagenumber"]=chunk.PageNumber,
                        ["startindex"]=chunk.StartIndex,
                        ["endindex"]=chunk.EndIndex,
                        ["length"] = chunk.Length
                        }
                    });
            }
            await _client.UpsertAsync(_options.CollectionName, points);
        }

        public async Task<List<SearchResultDTO>> SearchAsync(float[] embedding, int topK)
        {
            var response = await _client.QueryAsync(
                    collectionName: _options.CollectionName,
                    query: embedding,
                    limit: (ulong)topK);

            var results = new List<SearchResultDTO>();

            foreach (var point in response)
            {
                results.Add(new SearchResultDTO
                {
                    DocumentId = point.Payload["documentId"].StringValue,
                    DocumentName = point.Payload["documentname"].StringValue,
                    ChunkNumber = (int)point.Payload["chunkNumber"].IntegerValue,
                    Content = point.Payload["content"].StringValue,
                    Score = point.Score
                });
            }
            return results;
        }

    }
}
