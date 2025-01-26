using Domain.Entitites;
using Safeguard.Services;
using Application.Dtos.Default;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.RecoveryEmail.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Safeguard.Controllers
{
    public class RecoveryEmailController : Controller
    {
        private readonly IRecoveryEmailService _recoveryEmailService;
        private readonly Controller _controller;
        public RecoveryEmailController
        (
            IRecoveryEmailService recoveryEmailService 
        ) 
        { 
            _recoveryEmailService = recoveryEmailService;
            _controller = this;
        }

        [Authorize]
        [HttpGet("get-recovery-emails")]
        public ActionResult<BaseResponse<List<RecoveryEmailResponse>>> GetAll(string referenceId, string type)
        {
            var response = _recoveryEmailService.GetAllRecoveryEmails(referenceId,  type);    

            return response.Result(_controller); 
        }
        [Authorize]
        [HttpPost("create-recovery-email")]
        public ActionResult<BaseResponse<RecoveryEmail>> Create([FromBody] RecoveryEmailRequest entity)
        {
            var response = _recoveryEmailService.AddRecoveryEmail(entity);

            return response.Result(_controller);
        }
        [Authorize]
        [HttpDelete("delete-recovery-email")]
        public ActionResult<DefaultResponse> Delete(int id)
        {
            var response = _recoveryEmailService.DeleteRecoveryEmail(id);

            return response.DefaultResult(_controller);
        }
    }
}
