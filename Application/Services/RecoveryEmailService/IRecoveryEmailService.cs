using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Dtos.RecoveryEmail.Base;
using Domain.Entitites;
using System;

namespace Safeguard.Services
{
    public interface IRecoveryEmailService
    {
        BaseResponse<RecoveryEmail> AddRecoveryEmail(RecoveryEmailRequest provider);
        DefaultResponse DeleteRecoveryEmail(int id);
        BaseResponse<List<RecoveryEmailResponse>> GetAllRecoveryEmails(string referenceId, string type);
    }
}
