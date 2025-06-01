using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Services;
using Application.Services.EmailService;
using Application.Services.Identity;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ASP.NET_Core_Template.Controllers
{
    public class EmailAddressController : Controller
    {

        public readonly IEmailAddressService _accountService;
        private readonly Controller _controller;

        public EmailAddressController
        (
            IEmailAddressService accountService
        )
        {
            _accountService = accountService;
            _controller = this;   
        }

        [HttpPost("create-email-address")]
        public ActionResult<BaseResponse<EmailAddress>> RegisterEmailAddress([FromBody] EmailAddressRequest accountRequest)
        {
            var result = _accountService.RegisterEmailAddress(accountRequest);
            return result.Result(_controller);
        }

        [HttpPut("edit-email-address")]
        public ActionResult<BaseResponse<EmailAddress>> UpdateEmailAddress([FromBody] EmailAddressRequest accountRequest, int id)
        {
            var result = _accountService.UpdateEmailAddress(accountRequest, id);
            return result.Result(_controller);
        }
        
        [HttpDelete("delete-email-address")]
        public ActionResult<DefaultResponse> DeleteEmailAddress(int id)
        {
            var result = _accountService.DeleteEmailAddress(id);
            return result.DefaultResult(_controller);
        }
        
        [HttpGet("get-email-addresses")]
        public ActionResult<BaseResponse<List<EmailAddress>>> GetAllEmailsAddresses()
        {
            var result = _accountService.GetAllEmailsAddresses();
            return result.Result(_controller);
        }
        
        [HttpGet("get-email-address-by-id")]
        public ActionResult<BaseResponse<EmailAddress>> GetEmailById(int id)
        {
            var result = _accountService.GetEmailAddressById(id);
            return result.Result(_controller);
        }
    }
}
