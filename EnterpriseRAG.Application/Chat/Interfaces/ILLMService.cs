using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Interfaces
{
    public interface ILLMService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
