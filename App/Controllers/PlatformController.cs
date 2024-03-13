using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Application.Dtos.Provider.Base;
using Application.Services;
using Application.Services.Identity;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ASP.NET_Core_Template.Controllers
{
    public class PlatformController : Controller
    {

        public readonly IPlatformService _platformService;
        private readonly Controller _controller;

        public PlatformController
        (
            IPlatformService platformService
        )
        {
            _platformService = platformService;
            _controller = this;   
        }

        [HttpPost("create-platform")]
        public async Task<ActionResult<DefaultResponse>> Register([FromBody] PlatformRequest accountRequest)
        {
            var result = await _platformService.Register(accountRequest);
            return result.DefaultResult(_controller);
        }
        [HttpPut("edit-platform")]
        public async Task<ActionResult<DefaultResponse>> Update([FromBody] PlatformRequest accountRequest, int id)
        {
            var result = await _platformService.Update(accountRequest, id);
            return result.DefaultResult(_controller);
        }
        [HttpDelete("delete-platform")]
        public async Task<ActionResult<DefaultResponse>> Delete(int id)
        {
            var result = await _platformService.Delete(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-platforms")]
        public async Task<ActionResult<BaseResponse<List<Platform>>>> GetAll()
        {
            var result = await _platformService.GetAll();
            return result.Result(_controller);
        }
    }
}
