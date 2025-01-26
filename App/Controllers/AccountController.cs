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
    public class AccountController : Controller
    {

        public readonly IAccountService _accountService;
        private readonly Controller _controller;

        public AccountController
        (
            IAccountService accountService
        )
        {
            _accountService = accountService;
            _controller = this;   
        }

        [HttpPost("create-account")]
        public ActionResult<BaseResponse<Account>> RegisterAccount([FromBody] AccountRequest accountRequest)
        {
            var result = _accountService.RegisterAccount(accountRequest);
            return result.Result(_controller);
        }
        [HttpPut("edit-account")]
        public ActionResult<BaseResponse<Account>> UpdateAccount([FromBody] AccountRequest accountRequest, int id)
        {
            var result = _accountService.UpdateAccount(accountRequest, id);
            return result.Result(_controller);
        }
        [HttpDelete("delete-account")]
        public ActionResult<DefaultResponse> DeleteAccount(int id)
        {
            var result = _accountService.DeleteAccount(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-accounts")]
        public ActionResult<BaseResponse<List<Account>>> GetAllAccounts()
        {
            var result = _accountService.GetAllAccounts();
            return result.Result(_controller);
        }
        [HttpGet("get-account-by-id")]
        public ActionResult<BaseResponse<Account>> GetEmailById(int id)
        {
            var result = _accountService.GetAccountById(id);
            return result.Result(_controller);
        }
    }
}
