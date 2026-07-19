using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Conversations.DTOS
{
    public class ConversationMessageDto
    {
        public Guid MessageId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
    }

}
