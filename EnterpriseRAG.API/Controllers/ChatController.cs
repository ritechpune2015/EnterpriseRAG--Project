using EnterpriseRAG.Application.Chat.DTO;
using EnterpriseRAG.Application.Chat.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseRAG.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _service;
        public ChatController(IChatService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Chat(ChatRequest request)
        {
            return Ok(await _service.AskAsync(request));
        }

    }
}
