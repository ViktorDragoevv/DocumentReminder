using Document.NewFolder1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Document.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private ISendMailServices _sendMailServices;
        public SendMailController(ISendMailServices sendMailServices)
        {
            _sendMailServices = sendMailServices;
        }
        [HttpPost("SendEmail")]
        public async Task<ResponseVM> SendEmail() => await _sendMailServices.SendEmail();
    }
}
