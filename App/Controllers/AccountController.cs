using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Services;
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


        [HttpPost("create-account-network")]
        public async Task<ActionResult<DefaultResponse>> RegisterProvider([FromBody] AccountRequest accountRequest)
        {
            var result = await _accountService.RegisterProvider(accountRequest);
            return result.DefaultResult(_controller);
        }
        [HttpPut("edit-account-network")]
        public async Task<ActionResult<DefaultResponse>> UpdateProvider([FromBody] AccountRequest accountRequest, int id)
        {
            var result = await _accountService.UpdateProvider(accountRequest, id);
            return result.DefaultResult(_controller);
        }
        [HttpDelete("delete-account-network")]
        public async Task<ActionResult<DefaultResponse>> DeleteProvider(int id)
        {
            var result = await _accountService.DeleteProvider(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-accounts-network")]
        public async Task<ActionResult<BaseResponse<List<Account>>>> GetAllProviders()
        {
            var result = await _accountService.GetAllProviders();
            return result.Result(_controller);
        }
    }
}
