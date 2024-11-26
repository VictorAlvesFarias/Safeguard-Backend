using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Services;
using Application.Services.Identity;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ASP.NET_Core_Template.Controllers
{
    public class EmailController:Controller
    {
        public readonly IEmailService _emailService;
        private readonly Controller _controller;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
            _controller = this;
        }

        [HttpPost("create-email")]
        public ActionResult<BaseResponse<Email>> RegisterProvider([FromBody] EmailRequest emailRequest)
        {
            var result = _emailService.RegisterEmail(emailRequest);
            return result.Result(_controller);
        }

        [HttpPut("edit-email")]
        public ActionResult<BaseResponse<Email>> UpdateProvider([FromBody] EmailRequest emailRequest, int id)
        {
            var result = _emailService.UpdateEmail(emailRequest, id);
            return result.Result(_controller);
        }

        [HttpDelete("delete-email")]
        public ActionResult<DefaultResponse> DeleteProvider(int id)
        {
            var result = _emailService.DeleteEmail(id);
            return result.DefaultResult(_controller);
        }

        [HttpGet("get-emails")]
        public ActionResult<BaseResponse<List<Email>>> GetAllProviders()
        {
            try
            {
                var result = _emailService.GetAllEmail();
                return result.Result(_controller);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
        [HttpGet("get-email-by-id")]
        public ActionResult<BaseResponse<Email>> GetEmailById(int id)
        {
            var result = _emailService.GetEmailById(id);
            return result.Result(_controller);
        }
    }
}
