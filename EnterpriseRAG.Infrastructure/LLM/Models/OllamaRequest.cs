using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.LLM.Models
{
    public class OllamaRequest
    {
        public string Model { get; set; } = "";
        public string Prompt { get; set; } = "";
        public bool Stream { get; set; }
    }

}
