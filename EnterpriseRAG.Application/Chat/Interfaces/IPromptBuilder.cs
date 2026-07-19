using EnterpriseRAG.Application.Chat.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Interfaces
{
    public interface IPromptBuilder
    {
        string BuildPrompt(
            PromptRequest request);
    }

}
