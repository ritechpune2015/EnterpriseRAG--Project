using EnterpriseRAG.Application.Chat.DTO;
using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Retrieval.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.Services
{
    public class ChatService:IChatService
    {
        private readonly  IRetrievalService _retrieval;
        private readonly ILLMService _llm;
        private readonly PromptBuilder _builder;
        public ChatService(IRetrievalService retrieval, ILLMService llm, PromptBuilder builder)
        {
            _retrieval = retrieval;
            _llm = llm;
            _builder = builder;
        }

        public async Task<ChatResponse> AskAsync(ChatRequest request)
        {
           
            var chunks = await _retrieval.SearchAsync(request.Question, request.TopK);

            var prompt = _builder.BuildPrompt(request.Question, chunks);

            var answer = await _llm.GenerateAsync(prompt);

            return new ChatResponse { Answer = answer, Sources = chunks };
        }


    }
}
