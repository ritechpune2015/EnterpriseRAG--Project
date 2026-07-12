using EnterpriseRAG.Application.Document.Interfaces;
using EnterpriseRAG.Domain.Entities;
using EnterpriseRAG.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Documents
{
    public class ChunkingService:IChunkingService
    {
        private readonly ChunkOptions _options;
        public ChunkingService(IOptions<ChunkOptions> options)
        {
            _options = options.Value;
        }


 public List<DocumentChunk> ChunkDocument(Guid documentId, string text)
 {
            var chunks = new List<DocumentChunk>();
            int chunkNumber = 1;
            int start = 0;

            while (start < text.Length)
            {
                int length =
                    Math.Min(
                        _options.ChunkSize,
                        text.Length - start);

                string chunk =
                    text.Substring(start, length);

                chunks.Add(new DocumentChunk
                {
                    Id = Guid.NewGuid(),

                    DocumentId = documentId,

                    ChunkNumber = chunkNumber++,

                    Content = chunk,

                    StartIndex = start,

                    EndIndex = start + length,
                    Length = length
                });

                start +=
                _options.ChunkSize -
                _options.ChunkOverlap;
            }
            return chunks;
        }

    }
}
