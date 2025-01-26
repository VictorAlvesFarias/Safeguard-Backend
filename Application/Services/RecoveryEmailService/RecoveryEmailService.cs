using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Dtos.RecoveryEmail.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;

namespace Safeguard.Services
{
    public class RecoveryEmailService : IRecoveryEmailService
    {
        private readonly IBaseRepository<RecoveryEmail> _recoveryEmail;
        private readonly IBaseRepository<Email> _emailRepository;
        public RecoveryEmailService
        (
            IBaseRepository<RecoveryEmail> recoveryEmail,
            IBaseRepository<Email> emailRepository
        ) 
        {
            _recoveryEmail = recoveryEmail;
            _emailRepository = emailRepository;
        } 
        public BaseResponse<RecoveryEmail> AddRecoveryEmail(RecoveryEmailRequest recoveryEmailRequest)
        {
            var recoveryEmail = new RecoveryEmail();
            var response = new BaseResponse<RecoveryEmail>();

            var recoveryEmails = _recoveryEmail.GetAll()
                .Where(e =>  
                    e.EmailId == recoveryEmailRequest.EmailId && 
                    e.ReferenceType == recoveryEmailRequest.ReferenceType &&
                    e.ReferenceId == recoveryEmailRequest.ReferenceId
                )
                .FirstOrDefault();

            if (recoveryEmails != null)
            {
                response.AddError("O e-mail de recuperação enviado ja foi adicionado para está conta");
                response.Success = false;

                return response;
            }

            recoveryEmail.Create(
                recoveryEmailRequest.ReferenceType,
                recoveryEmailRequest.ReferenceId,
                recoveryEmailRequest.EmailId
            );

            var result = _recoveryEmail.AddAsync(recoveryEmail).Result;

            response.Success = result is not null;
            response.Data = result;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }

        public DefaultResponse DeleteRecoveryEmail(int id)
        {
            var recoveryEmail = _recoveryEmail.GetAll().Where(e=>e.Id == id).FirstOrDefault();
            var success = _recoveryEmail.RemoveAsync(recoveryEmail);
            var response = new DefaultResponse(success);

            return response;
        }

        public BaseResponse<List<RecoveryEmailResponse>> GetAllRecoveryEmails(string referenceId, string type)
        {
            var recoveryEmails = _recoveryEmail.GetAll().Where(e=>e.ReferenceType == type && e.ReferenceId == referenceId).ToList();
            var emailsIds = recoveryEmails.Select(e => e.EmailId).ToList();
            var result = _emailRepository.GetAll()
                .Where(e => emailsIds.Contains(e.Id))
                .Include(e => e.Provider)
                .ToList();
            
            var response = new BaseResponse<List<RecoveryEmailResponse>>(result is not null);

            response.Data = result.Select(e => new RecoveryEmailResponse()
                {
                    EmailId = e.Id,
                    Image = e.Provider.Image,
                    Email = $"{e.Username}@{e.Provider.Signature}",
                    Id = recoveryEmails.First(re => re.EmailId == e.Id).Id
                }).ToList();

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
    }
}
