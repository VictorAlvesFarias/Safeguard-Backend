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
                keyRequest.ReferenceType,
                keyRequest.Key,
                keyRequest.ReferenceId
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
            var recoveryKey = _recoveryKey.GetAll().Where(e=>e.Id == id).FirstOrDefault();
            var success = _recoveryKey.RemoveAsync(recoveryKey);
            var response = new DefaultResponse(success);

            return response;
        }

        public BaseResponse<List<RecoveryKey>> GetAllRecoveryKeys(string referenceId, string type)
        {
            var result = _recoveryKey.GetAll().Where(e=>e.ReferenceType == type && e.ReferenceId == referenceId).ToList();
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
