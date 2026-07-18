using EnterpriseRAG.Application.Chat.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Interfaces
{
    public interface IChatService
    {
        Task<ChatResponse> AskAsync(ChatRequest request);
    }
}
