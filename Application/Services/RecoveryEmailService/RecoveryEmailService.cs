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

            var recoveryEmails = _recoveryEmail.Get()
                .Where(e =>  
                    e.EmailId == recoveryEmailRequest.EmailId && 
                    e.ParentEmailId == recoveryEmailRequest.ParentEmailId
                )
                .FirstOrDefault();

            if (recoveryEmails != null)
            {
                response.AddError("O e-mail de recuperação enviado ja foi adicionado para está conta");
                response.Success = false;

                return response;
            }

            recoveryEmail.Create(
                recoveryEmailRequest.ParentEmailId,
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
            var recoveryEmail = _recoveryEmail.Get().Where(e=>e.Id == id).FirstOrDefault();
            var success = _recoveryEmail.Remove(recoveryEmail);
            var response = new DefaultResponse(success);

            return response;
        }

        public BaseResponse<List<RecoveryEmailResponse>> GetRecoveryEmails(int emailId)
        {
            var recoveryEmails = _recoveryEmail.Get().Where(e=>e.EmailId == emailId).ToList();
            var emailsIds = recoveryEmails.Select(e => e.ParentEmailId).ToList();
            var result = _emailRepository.Get()
                .Where(e => emailsIds.Contains(e.Id))
                .Include(e => e.EmailAddress)
                .ThenInclude(e=>e.Provider)
                .ThenInclude(e=>e.Image)
                .ToList();
            
            var response = new BaseResponse<List<RecoveryEmailResponse>>(result is not null);

            response.Data = result.Select(e => new RecoveryEmailResponse()
                {
                    ParentEmailId = e.Id,
                    Image = e.EmailAddress.Provider.Image,
                    Email = $"{e.Username}@{e.EmailAddress.Provider.Signature}",
                    Id = recoveryEmails.First(re => re.ParentEmailId == e.Id).Id
                }).ToList();

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
    }
}
