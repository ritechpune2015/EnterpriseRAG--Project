using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Retrieval.DTO
{
    public class SearchResultDTO
    {
        public string DocumentId { get; set; } = "";
        public string DocumentName { get; set; } = "";
        public int ChunkNumber { get; set; }
        public string Content { get; set; } = "";
        public float Score { get; set; }
    }
}
