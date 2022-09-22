using EmailService;
using EmailService.Configurations;
using EmailService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayCore_Final.TestX
{
    public class EmailTest
    {
        [Fact]
        public void CreateEmail_ReturnsMailMessage_WhenMessageGiven()
        {
            var emailMock = new EmailConfiguration { From = "omer-akkaya@outlook.com" };

            Message messageMock = new Message("omer-akkaya@outlook.com", "subject", "body");

            //arrange

            var eMailSender = new EmailSender(emailMock);

            //act
            var mail = eMailSender.CreateEmail(messageMock);

            //assert
            Assert.NotNull(mail);

        }
    }
}
