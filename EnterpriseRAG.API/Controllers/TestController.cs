using EnterpriseRAG.Application.Embeddings.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseRAG.API.Controllers
{
    public class EmbeddingRequest
    {
        public string Text { get; set; } = "";
     }


    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IEmbeddingService _embeddingService;

        public TestController(
            IEmbeddingService embeddingService)
        {
            _embeddingService = embeddingService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Dependency Injection Working");
        }

        [HttpPost]
        public async Task<IActionResult> Generate(EmbeddingRequest request)
        {
            var embedding =
        await _embeddingService
        .GenerateEmbeddingAsync(
        request.Text);

            return Ok(new
            {
                Length = embedding.Length,
                Vector = embedding
            });
        }

    }
}
