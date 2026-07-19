using EnterpriseRAG.Application.Conversations.DTOS;
using EnterpriseRAG.Application.Retrieval.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.DTO
{
    public class PromptRequest
    {
        public string Question { get; set; } = string.Empty;
        public List<ConversationMessageDto> History { get; set; }
            = new();
        public List<SearchResultDTO> RetrievedChunks { get; set; }
            = new();
    }
}
