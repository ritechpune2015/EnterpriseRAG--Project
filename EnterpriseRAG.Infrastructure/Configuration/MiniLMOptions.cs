using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class MiniLMOptions
    {
        public const string SectionName = "MiniLM";
        public string ModelPath { get; set; } = string.Empty;
        public string VocabularyPath { get; set; } = string.Empty;
        public int EmbeddingDimension { get; set; } = 384;
    }
}
