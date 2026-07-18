using EnterpriseRAG.Application.Retrieval.DTO;
using EnterpriseRAG.Application.Retrieval.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseRAG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IRetrievalService _retrievalService;
        public SearchController(IRetrievalService retrievalService)
        {
            _retrievalService = retrievalService;
        }


        [HttpPost]
        public async Task<IActionResult> Search(SearchRequestDto request)
        {
            var result = await _retrievalService.SearchAsync(request.Question, request.TopK);
            return Ok(result);
        }
    }
}
