using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
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
        public EmailService(IBaseRepository<Email> emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<DefaultResponse> RegisterProvider(EmailRequest emailRequest)
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
        public async Task<DefaultResponse> UpdateProvider(EmailRequest emailRequest, int id)
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
        public async Task<DefaultResponse> DeleteProvider(int id)
        {
            var provider = await _emailRepository.GetAsync(id);

            var success = _emailRepository.RemoveAsync(provider);

            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Email>>> GetAllProviders()
        {

            var emails = await _emailRepository.GetAllAsync();

            var response = new BaseResponse<List<Email>>()
            {
                Data = emails.ToList(),
                Success = true
            };

            return response;
        }
    }
}

