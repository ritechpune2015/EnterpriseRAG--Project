using EnterpriseRAG.Application.Conversations.Interfaces;
using EnterpriseRAG.Domain.Entities;
using EnterpriseRAG.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace EnterpriseRAG.Infrastructure.Conversations.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;
        public ConversationRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateConversationAsync(Conversation conversation)
        {
            await _context.Conversations.AddAsync(conversation);
        }

        public async Task<Conversation?> GetConversationAsync(
        Guid conversationId)
        {
            return await _context.Conversations
                .Include(x => x.Messages)
                .FirstOrDefaultAsync(x =>
                    x.ConversationId == conversationId &&
                    !x.IsDeleted);
        }
        public async Task<List<Conversation>> GetConversationsAsync()
        {
            return await _context.Conversations
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.UpdatedOn)
                .ToListAsync();
        }
        public async Task AddMessageAsync(ConversationMessage message)
        {
            await _context.ConversationMessages
                .AddAsync(message);
        }
        public Task UpdateConversationAsync(Conversation conversation)
        {
            _context.Conversations.Update(conversation);
            return Task.CompletedTask;
        }
        public async Task DeleteConversationAsync(Guid conversationId)
        {
            var conversation =
                await _context.Conversations
                    .FirstOrDefaultAsync(x =>
                        x.ConversationId == conversationId);

            if (conversation == null)
                return;

            conversation.IsDeleted = true;
            conversation.UpdatedOn = DateTime.UtcNow;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}