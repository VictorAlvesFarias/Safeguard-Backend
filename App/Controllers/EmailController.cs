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
        public async Task<ActionResult<DefaultResponse>> RegisterProvider([FromBody] EmailRequest providerRequest)
        {
            var result = await _emailService.RegisterProvider(providerRequest);
            return result.DefaultResult(_controller);
        }
        [HttpPut("edit-email")]
        public async Task<ActionResult<DefaultResponse>> UpdateProvider([FromBody] EmailRequest providerRequest, int id)
        {
            var result = await _emailService.UpdateProvider(providerRequest, id);
            return result.DefaultResult(_controller);
        }
        [HttpDelete("delete-email")]
        public async Task<ActionResult<DefaultResponse>> DeleteProvider(int id)
        {
            var result = await _emailService.DeleteProvider(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-emails")]
        public async Task<ActionResult<BaseResponse<List<Email>>>> GetAllProviders()
        {
            var result = await _emailService.GetAllProviders();
            return result.Result(_controller);
        }
    }
}
