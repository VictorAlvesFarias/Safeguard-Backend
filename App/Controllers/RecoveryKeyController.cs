using Domain.Entitites;
using Safeguard.Services;
using Application.Dtos.Default;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos.RecoveryKey.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Safeguard.Controllers
{
    public class RecoveryKeyController : Controller
    {
        private readonly IRecoveryKeyService _recoveryKeyService;
        private readonly Controller _controller;
        public RecoveryKeyController
        (
            IRecoveryKeyService recoveryKeyService 
        ) 
        { 
            _recoveryKeyService = recoveryKeyService;
            _controller = this;
        }

        [Authorize]
        [HttpGet("get-recovery-keys")]
        public ActionResult<BaseResponse<List<RecoveryKey>>> Get(string emailId)
        {
            var response = _recoveryKeyService.GetRecoveryKeys(emailId);    

            return response.Result(_controller); 
        }
        [Authorize]
        [HttpPost("create-recovery-key")]
        public ActionResult<BaseResponse<RecoveryKey>> Create([FromBody] RecoveryKeyRequest entity)
        {
            var response = _recoveryKeyService.AddRecoveryKey(entity);

            return response.Result(_controller);
        }
        [Authorize]
        [HttpDelete("delete-recovery-key")]
        public ActionResult<DefaultResponse> Delete(int id)
        {
            var response = _recoveryKeyService.DeleteRecoveryKey(id);

            return response.DefaultResult(_controller);
        }
    }
}
