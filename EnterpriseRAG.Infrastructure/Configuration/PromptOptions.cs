using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Infrastructure.Configuration
{
    public class PromptOptions
    {
        public int MaxHistoryMessages { get; set; } = 10;

        public int MaxRetrievedChunks { get; set; } = 5;

        public bool IncludeSources { get; set; } = true;
    }
}
