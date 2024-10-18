using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Dtos.Provider.Base;
using Application.Services;
using Application.Services.Identity;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ASP.NET_Core_Template.Controllers
{
    public class ProviderController : Controller
    {

        public readonly IProviderService _providerService;
        private readonly Controller _controller;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
            _controller = this;
        }

        [HttpPost("create-provider")]
        public async Task<ActionResult<DefaultResponse>> RegisterProvider([FromBody] ProviderRequest providerRequest)
        {
            var result = await _providerService.RegisterProvider(providerRequest);
            return result.DefaultResult(_controller);
        }
        [HttpPut("edit-provider")]
        public async Task<ActionResult<DefaultResponse>> UpdateProvider([FromBody] ProviderRequest providerRequest, int id) 
        {
            var result = await _providerService.UpdateProvider(providerRequest,id);
            return result.DefaultResult(_controller);
        }
        [HttpDelete("delete-provider")]
        public async Task<ActionResult<DefaultResponse>> DeleteProvider(int id) 
        {
            var result = await _providerService.DeleteProvider(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-providers")]
        public async Task<ActionResult<BaseResponse<List<Provider>>>> GetAllProviders()
        {
            var result = await _providerService.GetAllProviders();
            return result.Result(_controller);
        }
        [HttpGet("get-provider-by-id")]
        public async Task<ActionResult<BaseResponse<Provider>>> GetProviderById(int id)
        {
            var result = await _providerService.GetProviderById(id);
            return result.Result(_controller);
        }
    }
}
