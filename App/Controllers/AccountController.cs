﻿using Application.Dtos.Default;
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
        public async Task<ActionResult<DefaultResponse>> RegisterAccount([FromBody] AccountRequest accountRequest)
        {
            var result = await _accountService.RegisterAccount(accountRequest);
            return result.DefaultResult(_controller);
        }
        [HttpPut("edit-account")]
        public async Task<ActionResult<DefaultResponse>> UpdateAccount([FromBody] AccountRequest accountRequest, int id)
        {
            var result = await _accountService.UpdateAccount(accountRequest, id);
            return result.DefaultResult(_controller);
        }
        [HttpDelete("delete-account")]
        public async Task<ActionResult<DefaultResponse>> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-accounts")]
        public async Task<ActionResult<BaseResponse<List<Account>>>> GetAllAccounts()
        {
            var result = await _accountService.GetAllAccounts();
            return result.Result(_controller);
        }
        [HttpGet("get-account-by-id")]
        public async Task<ActionResult<BaseResponse<Account>>> GetEmailById(int id)
        {
            var result = await _accountService.GetAccountById(id);
            return result.Result(_controller);
        }
    }
}
