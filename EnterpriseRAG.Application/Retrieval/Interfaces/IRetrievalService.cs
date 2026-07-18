using EnterpriseRAG.Application.Retrieval.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Retrieval.Interfaces
{
    public interface IRetrievalService
    {
        Task<List<SearchResultDto>> SearchAsync(string question, int topK = 5);
    }

}
