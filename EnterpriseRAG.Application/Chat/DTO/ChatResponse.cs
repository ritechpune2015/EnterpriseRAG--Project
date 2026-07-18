using EnterpriseRAG.Application.Retrieval.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Chat.DTO
{
    public class ChatResponse
    {
        public string Answer { get; set; } = "";
        public List<SearchResultDto> Sources { get; set; } = new();
    }

}
