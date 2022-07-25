using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRHelloSignalR.Server.Services;

namespace SignalRHelloSignalR.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        readonly ChatService _chatService;

        public HomeController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{message}")]
        public async Task<IActionResult> Index(string message)
        {
            await _chatService.SendMessageAsync(message);
            return Ok();
        }
    }
}
