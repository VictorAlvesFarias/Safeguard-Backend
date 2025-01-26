using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Dtos.RecoveryKey.Base;
using Domain.Entitites;
using System;

namespace Safeguard.Services
{
    public interface IRecoveryKeyService
    {
        BaseResponse<RecoveryKey> AddRecoveryKey(RecoveryKeyRequest provider);
        DefaultResponse DeleteRecoveryKey(int id);
        BaseResponse<List<RecoveryKey>> GetAllRecoveryKeys(string referenceId, string type);
    }
}
