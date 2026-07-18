using EnterpriseRAG.Application.Chat.Interfaces;
using EnterpriseRAG.Infrastructure.Configuration;
using EnterpriseRAG.Infrastructure.LLM.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace EnterpriseRAG.Infrastructure.LLM
{
    public class OllamaService : ILLMService
    {
        private readonly HttpClient _client;
        private readonly OllamaOptions _options;
        public OllamaService(HttpClient client, IOptions<OllamaOptions> options)
        {
            _client = client;
            _options = options.Value;
        }

        
        public async Task<string> GenerateAsync(string prompt)
        {
            var request = new
            {
                model = _options.Model,
                prompt = prompt,
                stream = false
            };
            var response = await _client.PostAsJsonAsync("/api/generate", request);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<OllamaResponse>();
            return result?.Response ?? "";
        }

    }
}
