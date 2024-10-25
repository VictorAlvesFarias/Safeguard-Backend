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
        public ActionResult<BaseResponse<Provider>> RegisterProvider([FromForm] ProviderRequest providerRequest)
        {
            var result = _providerService.RegisterProvider(providerRequest);
            return result.Result(_controller);
        }
        [HttpPut("edit-provider")]
        public ActionResult<BaseResponse<Provider>> UpdateProvider([FromForm] ProviderRequest providerRequest, int id) 
        {
            var result = _providerService.UpdateProvider(providerRequest,id);
            return result.Result(_controller);
        }
        [HttpDelete("delete-provider")]
        public ActionResult<DefaultResponse> DeleteProvider(int id) 
        {
            var result = _providerService.DeleteProvider(id);
            return result.DefaultResult(_controller);
        }
        [HttpGet("get-providers")]
        public ActionResult<BaseResponse<List<Provider>>> GetAllProviders()
        {
            var result = _providerService.GetAllProviders();
            return result.Result(_controller);
        }
        [HttpGet("get-provider-by-id")]
        public ActionResult<BaseResponse<Provider>> GetProviderById(int id)
        {
            var result = _providerService.GetProviderById(id);
            return result.Result(_controller);
        }
    }
}
