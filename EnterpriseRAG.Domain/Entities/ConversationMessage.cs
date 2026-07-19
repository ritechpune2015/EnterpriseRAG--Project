using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Domain.Entities
{
    public class ConversationMessage
    {
        public Guid MessageId { get; set; }
        public Guid ConversationId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public Conversation Conversation { get; set; } = null!;
    }

}
