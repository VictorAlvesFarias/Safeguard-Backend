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

        public BaseResponse<Email> RegisterEmail(EmailRequest emailRequest)
        {
            var provider = _providerRepository.GetAsync(emailRequest.ProviderId).Result;
            var email = new Email();

            email.Create(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                provider
            );

            var addResult = _emailRepository.AddAsync(email).Result;
            var response = new BaseResponse<Email>(addResult is not null);

            response.Data = addResult;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public BaseResponse<Email> UpdateEmail(EmailRequest emailRequest, int id)
        {
            var provider = _providerRepository.GetAsync(emailRequest.ProviderId).Result;
            var email = _emailRepository.GetAsync(id).Result;

            email.Update(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                provider
            );

            var result = _emailRepository.UpdateAsync(email);
            var response = new BaseResponse<Email>(result);

            response.Data = email;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;

        }
        public DefaultResponse DeleteEmail(int id)
        {
            var provider = _emailRepository.GetAsync(id).Result;

            var success = _emailRepository.RemoveAsync(provider);

            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<Email>> GetAllEmail()
        {
            var emails =  _emailRepository.GetAll()
                .Include(e=>e.Provider)
                .ThenInclude(e => e.Image)
                .ToList();

            var response = new BaseResponse<List<Email>>()
            {
                Data = emails.ToList(),
                Success = true
            };

            return response;
        }
        public BaseResponse<Email> GetEmailById(int id)
        {

            var provider = _emailRepository.GetAll()
                .Where(e => e.Id == id)
                .Include(e=>e.Provider)
                .ThenInclude(e => e.Image)
                .FirstOrDefault();

            var response = new BaseResponse<Email>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}

