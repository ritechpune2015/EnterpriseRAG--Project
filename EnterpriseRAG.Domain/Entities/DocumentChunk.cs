using System;
using System.Collections.Generic;
using System.Text;
namespace EnterpriseRAG.Domain.Entities
{
    public class DocumentChunk
    {
        public Guid Id { get; set; }
        public string DocumentName { get; set; } = "";
        public Guid DocumentId { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public int Length { get; set; }

        public int ChunkNumber { get; set; }
        public int PageNumber { get; set; }
        public string Content { get; set; } = "";
        public float[]? Embedding { get; set; }
    }
}
