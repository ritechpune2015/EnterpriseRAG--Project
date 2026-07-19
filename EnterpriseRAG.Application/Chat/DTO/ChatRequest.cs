using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.DTO
{
    public class ChatRequest
    {
        public Guid ConversationId { get; set; }
        public string Question { get; set; } = "";
        public int TopK { get; set; } = 5;
    }

}
