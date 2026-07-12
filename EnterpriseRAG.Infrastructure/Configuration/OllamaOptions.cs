using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class OllamaOptions
    {
        public const string SectionName = "Ollama";
        public string BaseUrl { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;

    }
}
