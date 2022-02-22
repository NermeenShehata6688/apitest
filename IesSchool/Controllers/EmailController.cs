using IesSchool.Core.Dto;
using IesSchool.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IesSchool.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private IEmailSenderService _emailSenderService;
        public EmailController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }
        [HttpPut]
        public ActionResult SendEmail(PasswordResetDto passwordResetDto)
        {
            try
            {
               var all= _emailSenderService.SendEmail(passwordResetDto);
                return Ok(all);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
