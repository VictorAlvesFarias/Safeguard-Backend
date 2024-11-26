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
        public ActionResult<BaseResponse<Platform>> Register([FromForm] PlatformRequest accountRequest)
        {
            var result = _platformService.Register(accountRequest);
            return result.Result(_controller);
        }
        [HttpPut("edit-platform")]
        public ActionResult<BaseResponse<Platform>> Update([FromForm] PlatformRequest accountRequest, int id)
        {
            var result = _platformService.Update(accountRequest, id);
            return result.Result(_controller);
        }
        [HttpDelete("delete-platform")]
        public ActionResult<DefaultResponse> Delete(int id)
        {
            var result = _platformService.Delete(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-platforms")]
        public ActionResult<BaseResponse<List<Platform>>> GetAll()
        {
            var result = _platformService.GetAll();
            return result.Result(_controller);
        }

        [HttpGet("get-platform-by-id")]
        public ActionResult<BaseResponse<Platform>> GetProviderById(int id)
        {
            var result = _platformService.GetPlatformById(id);
            return result.Result(_controller);
        }
    }
}
