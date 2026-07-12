using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Document.DTO
{
    public class ChunkDto
    {
        public int ChunkNumber { get; set; }
        public string Content { get; set; } = "";
        public int Length { get; set; }

    }
}
