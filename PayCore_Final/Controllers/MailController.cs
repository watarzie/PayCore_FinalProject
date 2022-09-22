using EmailService.Domain;
using EmailService.Services;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayCore_Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        private readonly IBackgroundJobClient backgroundJobClient;

        public MailController(IEmailSender emailSender, IBackgroundJobClient backgroundJobClient)
        {
            this.emailSender = emailSender;
            this.backgroundJobClient = backgroundJobClient;
        }

        [HttpGet]


        public IActionResult SendMail()
        {
            var message = new Message("omer-akkaya@outlook.com", "paycore", "paycoredeneme");
            emailSender.SendEmailAsync(message);
            // _backgroundJobClient.Schedule<IEmailSender> (x => x.SendEmailAsync(message), new DateTimeOffset(DateTime.Now));
            backgroundJobClient.Enqueue<IEmailSender>(x => x.SendEmailAsync(message));
            return Ok();
        }
    }
}
