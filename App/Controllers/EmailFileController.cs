using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Services;
using Application.Services.AppFileService;
using Application.Services.EmailService;
using Application.Services.Identity;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace ASP.NET_Core_Template.Controllers
{
    public class EmailFileController : Controller
    {
        public readonly IEmailFileService _appFileService;
        private readonly Controller _controller;

        public EmailFileController
        (
            IEmailFileService appFileService
        )
        {
            _appFileService = appFileService;
            _controller = this;   
        }

        [HttpPost("upload-email-file")]
        public ActionResult<BaseResponse<EmailFile>> UploadFile([FromForm]  IFormFile image, [FromForm] int emailId)
        {
            var result = _appFileService.InsertFile(image, emailId);
            return result.Result(_controller);
        }

        [HttpDelete("delete-email-file")]
        public ActionResult<DefaultResponse> DeleteFile(int id)
        {
            var result = _appFileService.DeleteFile(id);
            return result.DefaultResult(_controller);
        }

        [HttpGet("get-email-files")]
        public ActionResult<BaseResponse<List<EmailFile>>> GetFiles(int emailId)
        {
            var result = _appFileService.GetFiles(emailId);
            return result.Result(_controller);
        }
    }
}
