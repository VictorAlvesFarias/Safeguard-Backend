using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Dtos.RecoveryKey.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;

namespace Safeguard.Services
{
    public class RecoveryKeyService : IRecoveryKeyService
    {
        private readonly IBaseRepository<RecoveryKey> _recoveryKey;
        public RecoveryKeyService
        (
            IBaseRepository<RecoveryKey> recoveryKey
        ) 
        {
            _recoveryKey = recoveryKey;
        } 
        public BaseResponse<RecoveryKey> AddRecoveryKey(RecoveryKeyRequest keyRequest)
        {
            var recoveryKey = new RecoveryKey();

            recoveryKey.Create(
                keyRequest.EmailId,
                keyRequest.Key
            );

            var result = _recoveryKey.AddAsync(recoveryKey).Result;
            var response = new BaseResponse<RecoveryKey>(result is not null);

            response.Data = result;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }

        public DefaultResponse DeleteRecoveryKey(int id)
        {
            var recoveryKey = _recoveryKey.Get().Where(e=>e.Id == id).FirstOrDefault();
            var success = _recoveryKey.Remove(recoveryKey);
            var response = new DefaultResponse(success);

            return response;
        }

        public BaseResponse<List<RecoveryKey>> GetRecoveryKeys(string emailId)
        {
            var result = _recoveryKey.Get().Where(e=>e.EmailId == emailId).ToList();
            var response = new BaseResponse<List<RecoveryKey>>(result is not null);

            response.Data = result;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;
        }
    }
}
