using EnterpriseRAG.Application.Retrieval.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.DTO
{
    public class ChatResponse
    {
        public Guid ConversationId { get; set; }
        public string Answer { get; set; } = "";
        public List<SearchResultDTO> Sources { get; set; } = new();
    }

}
