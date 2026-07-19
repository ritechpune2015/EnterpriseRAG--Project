using EnterpriseRAG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Conversations.Interfaces
{
    public interface IConversationRepository
    {
        Task CreateConversationAsync(
            Conversation conversation);

        Task<Conversation?> GetConversationAsync(
            Guid conversationId);

        Task<List<Conversation>> GetConversationsAsync();

        Task AddMessageAsync(
            ConversationMessage message);

        Task UpdateConversationAsync(
            Conversation conversation);

        Task DeleteConversationAsync(
            Guid conversationId);

        Task SaveChangesAsync();
    }

}
