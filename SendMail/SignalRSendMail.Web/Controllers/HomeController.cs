using Microsoft.AspNetCore.Mvc;
using SignalRSendMail.Web.Models;
using SignalRSendMail.Web.Services.RabbitMQServices;
using System.Diagnostics;

namespace SignalRSendMail.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly RabbitMQPublisher _rabbitMQPublisher;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RabbitMQPublisher rabbitMQPublisher)
        {
            _logger = logger;
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}