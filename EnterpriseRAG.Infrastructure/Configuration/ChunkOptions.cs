using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class ChunkOptions
    {
        public const string SectionName = "Chunking";
        public int ChunkSize { get; set; } = 500;
        public int ChunkOverlap { get; set; } = 100;
    }
}
