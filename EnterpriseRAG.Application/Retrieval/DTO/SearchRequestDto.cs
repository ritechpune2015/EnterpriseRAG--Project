using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseRAG.Application.Retrieval.DTO
{
    public class SearchRequestDto
    {
        public string Question { get; set; } = "";
        public int TopK { get; set; } = 5;

    }
}
