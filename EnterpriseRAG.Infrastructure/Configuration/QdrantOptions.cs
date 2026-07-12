using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class QdrantOptions
    {
        public const string SectionName = "Qdrant";
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string CollectionName { get; set; } = string.Empty;

    }
}
