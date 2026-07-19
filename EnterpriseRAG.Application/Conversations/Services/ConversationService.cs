using EnterpriseRAG.Application.Conversations.DTOS;
using EnterpriseRAG.Application.Conversations.Interfaces;
using EnterpriseRAG.Domain.Entities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Conversations.Services
{
    public class ConversationService
        : IConversationService
    {
        private readonly IConversationRepository _repository;

        public ConversationService(
            IConversationRepository repository)
        {
            _repository = repository;
        }

public async Task AddAssistantMessageAsync(Guid conversationId, string message)
        {
            var conversation =
                await _repository
                    .GetConversationAsync(conversationId);

            if (conversation == null)
                throw new Exception("Conversation not found.");

            var assistantMessage =
                new ConversationMessage
                {
                    MessageId = Guid.NewGuid(),

                    ConversationId = conversationId,

                    Role = "Assistant",

                    Message = message,

                    CreatedOn = DateTime.UtcNow
                };

            await _repository
                .AddMessageAsync(assistantMessage);
          
            if(conversation!=null)
            conversation.UpdatedOn =
                DateTime.UtcNow;

            await _repository
                .UpdateConversationAsync(conversation);

            await _repository.SaveChangesAsync();
        }

        public async Task AddUserMessageAsync(
            Guid conversationId,
            string message)
        {
            var conversation =
                await _repository
                    .GetConversationAsync(conversationId);

            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    ConversationId= Guid.NewGuid(),
                    CreatedOn=DateTime.Now,
                    IsDeleted=false,
                    Title="New Conversation",
                };

               await this._repository.CreateConversationAsync(conversation);
           }
            
            var userMessage =
                new ConversationMessage
                {
                    MessageId = Guid.NewGuid(),
                    ConversationId = conversation.ConversationId,
                    Role = "User",
                    Message = message,
                    CreatedOn = DateTime.UtcNow
                };

            await _repository
                .AddMessageAsync(userMessage);
            
            if (conversation != null)
            {
                conversation.UpdatedOn =
                    DateTime.UtcNow;
                await _repository
                    .UpdateConversationAsync(conversation);
            }
            await _repository.SaveChangesAsync();
        }

        public async Task<Guid> CreateConversationAsync()
        {
            var conversation = new Conversation
            {
                ConversationId = Guid.NewGuid(),
                Title = "New Conversation",
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                IsDeleted = false
            };

            await _repository.CreateConversationAsync(conversation);
            await _repository.SaveChangesAsync();
            return conversation.ConversationId;
        }


        public async Task DeleteConversationAsync(Guid conversationId)
        {
            await _repository
                .DeleteConversationAsync(
                    conversationId);

            await _repository
                .SaveChangesAsync();
        }


        public async Task<List<ConversationMessageDto>> GetConversationHistoryAsync(Guid conversationId)
        {
            var conversation =
                await _repository
                    .GetConversationAsync(conversationId);

            if (conversation == null)
                return new List<ConversationMessageDto>();

            return conversation.Messages
                .OrderBy(x => x.CreatedOn)
                .Select(MapMessage)
                .ToList();
        }

        public async Task<List<ConversationDto>> GetConversationsAsync()
        {
            var conversations =
                await _repository.GetConversationsAsync();

            return conversations
                .Select(MapConversation)
                .ToList();
        }


        private static ConversationDto MapConversation(
    Conversation conversation)
        {
            return new ConversationDto
            {
                ConversationId = conversation.ConversationId,
                Title = conversation.Title,
                CreatedOn = conversation.CreatedOn,
                UpdatedOn = conversation.UpdatedOn
            };
        }

        private static ConversationMessageDto MapMessage(
            ConversationMessage message)
        {
            return new ConversationMessageDto
            {
                MessageId = message.MessageId,
                Role = message.Role,
                Message = message.Message,
                CreatedOn = message.CreatedOn
            };


        }
    }
}