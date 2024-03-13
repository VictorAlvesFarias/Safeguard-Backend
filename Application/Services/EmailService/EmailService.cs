using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.EmailService
{
    public class EmailService:IEmailService
    {

        public readonly IBaseRepository<Email> _emailRepository;
        public readonly IBaseRepository<Provider> _providerRepository;
        public EmailService(
            IBaseRepository<Email> emailRepository,
            IBaseRepository<Provider> providerRepository
)
        {
            _emailRepository = emailRepository;
            _providerRepository = providerRepository;
        }

        public async Task<DefaultResponse> RegisterEmail(EmailRequest emailRequest)
        {
            var provider = await _providerRepository.GetAsync(emailRequest.ProviderId);

            var email = new Email();

            email.Create(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                provider
            );

            var success = await _emailRepository.AddAsync(email);

            var response = new DefaultResponse(success);

            if (!success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public async Task<DefaultResponse> UpdateEmail(EmailRequest emailRequest, int id)
        {
            var provider = await _providerRepository.GetAsync(emailRequest.ProviderId);

            var email = await _emailRepository.GetAsync(id);

            email.Update(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                provider
            );

            var success = _emailRepository.UpdateAsync(email);

            var response = new DefaultResponse(success);

            return response;

        }
        public async Task<DefaultResponse> DeleteEmail(int id)
        {
            var provider = await _emailRepository.GetAsync(id);

            var success = _emailRepository.RemoveAsync(provider);

            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Email>>> GetAllEmail()
        {
            var emails = await _emailRepository.GetAllAsync(x=>x.Provider);

            var response = new BaseResponse<List<Email>>()
            {
                Data = emails.ToList(),
                Success = true
            };

            return response;
        }
    }
}

