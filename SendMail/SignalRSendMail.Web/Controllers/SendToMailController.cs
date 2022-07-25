using Microsoft.AspNetCore.Mvc;
using SignalRSendMail.Web.Models;
using SignalRSendMail.Web.Services.RabbitMQServices;

namespace SignalRSendMail.Web.Controllers
{
    public class SendToMailController : Controller
    {

        private readonly RabbitMQPublisher _rabbitMQPublisher;

        public SendToMailController(RabbitMQPublisher rabbitMQPublisher)
        {
            _rabbitMQPublisher = rabbitMQPublisher;
        }

        [HttpGet]
        public IActionResult SendMail()
        {

            return View();
        }

        [HttpPost]
        public IActionResult SendMail(MailandMessageDto mailandMessageDto)
        {
            if (ModelState.IsValid)
            {
                _rabbitMQPublisher.Publish(mailandMessageDto);
            }
            return Json(mailandMessageDto.Mail);
        }
    }
}
