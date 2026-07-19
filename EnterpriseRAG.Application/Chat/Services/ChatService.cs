using EnterpriseRAG.Application.Chat.DTO;
using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Application.Conversations.Interfaces;
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
        private readonly IPromptBuilder _builder;
        private readonly IConversationService _conversationService;
        public ChatService(IRetrievalService retrieval, ILLMService llm, IPromptBuilder builder, IConversationService conversationService)
        {
            _retrieval = retrieval;
            _llm = llm;
            _builder = builder;
            _conversationService = conversationService;
        }

        public async Task<ChatResponse> AskAsync(ChatRequest request)
        {

            if (string.IsNullOrWhiteSpace(request.Question))
            {
                throw new ArgumentException(
                    "Question is required.");
            }

         
            await _conversationService.AddUserMessageAsync(request.ConversationId, request.Question);


            var chunks = await _retrieval.SearchAsync(request.Question, request.TopK);

            var history =await _conversationService.GetConversationHistoryAsync(request.ConversationId);


            var promptRequest = new PromptRequest
                {
                    Question = request.Question,
                    History = history,
                    RetrievedChunks = chunks
                };

            var prompt = _builder.BuildPrompt(promptRequest);
            var answer = await _llm.GenerateAsync(prompt);
            await _conversationService .AddAssistantMessageAsync(
                    request.ConversationId,
                    answer);

            return new ChatResponse { ConversationId=request.ConversationId, Answer = answer, Sources = chunks };
        }


    }
}
