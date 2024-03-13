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
        public async Task<ActionResult<DefaultResponse>> RegisterProvider([FromBody] EmailRequest emailRequest)
        {
            var result = await _emailService.RegisterEmail(emailRequest);
            return result.DefaultResult(_controller);
        }

        [HttpPut("edit-email")]
        public async Task<ActionResult<DefaultResponse>> UpdateProvider([FromBody] EmailRequest emailRequest, int id)
        {
            var result = await _emailService.UpdateEmail(emailRequest, id);
            return result.DefaultResult(_controller);
        }

        [HttpDelete("delete-email")]
        public async Task<ActionResult<DefaultResponse>> DeleteProvider(int id)
        {
            var result = await _emailService.DeleteEmail(id);
            return result.DefaultResult(_controller);
        }

        [HttpGet("get-emails")]
        public async Task<ActionResult<BaseResponse<List<Email>>>> GetAllProviders()
        {
            try
            {
                var result = await _emailService.GetAllEmail();
                return result.Result(_controller);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}
