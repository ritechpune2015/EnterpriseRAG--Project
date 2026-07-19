using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Conversations.DTOS
{
    public class ConversationDto
    {
        public Guid ConversationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
