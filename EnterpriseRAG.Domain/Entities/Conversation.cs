using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Domain.Entities
{
    public class Conversation
    {
        public Guid ConversationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<ConversationMessage> Messages
            = new List<ConversationMessage>();
    }

}
