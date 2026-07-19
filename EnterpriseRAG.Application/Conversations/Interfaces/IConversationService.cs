using EnterpriseRAG.Application.Conversations.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Conversations.Interfaces
{
    public interface IConversationService
    {
        Task<Guid> CreateConversationAsync();
        Task<List<ConversationDto>> GetConversationsAsync();
        Task<List<ConversationMessageDto>>
            GetConversationHistoryAsync(Guid conversationId);
        Task AddUserMessageAsync(
            Guid conversationId,
            string message);
        Task AddAssistantMessageAsync(
            Guid conversationId,
            string message);
        Task DeleteConversationAsync(Guid conversationId);
    }
}
