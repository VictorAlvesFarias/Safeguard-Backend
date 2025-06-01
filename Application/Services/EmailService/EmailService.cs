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
        public readonly IBaseRepository<Platform> _platformRepository;
        public readonly IBaseRepository<EmailAddress> _emailAddressRepository;
        public EmailService(
            IBaseRepository<Email> emailRepository,
            IBaseRepository<Provider> providerRepository,
            IBaseRepository<Platform> platformRepository,
            IBaseRepository<EmailAddress> emailAddressRepository
        )
        {
            _emailRepository = emailRepository;
            _providerRepository = providerRepository;
            _platformRepository = platformRepository;
            _emailAddressRepository = emailAddressRepository;
        }

        public BaseResponse<Email> RegisterEmail(EmailRequest emailRequest)
        {
            var provider = _providerRepository.Get().FirstOrDefault(e => e.Id == emailRequest.ProviderId);
            var emailAddress = _emailAddressRepository.Get().FirstOrDefault(e => e.Id == emailRequest.EmailAddressId);
            var platform = _platformRepository.Get().FirstOrDefault(e=>e.Id == emailRequest.PlatformId);
            var email = new Email();

            email.Create(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                emailRequest.LastName,
                emailRequest.BirthDate,
                platform,
                emailAddress
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
            var provider = _providerRepository.Get().FirstOrDefault(e => e.Id == emailRequest.ProviderId);
            var emailAddress = _emailAddressRepository.Get().FirstOrDefault(e => e.Id == emailRequest.EmailAddressId);
            var platform = _platformRepository.Get().FirstOrDefault(e => e.Id == emailRequest.PlatformId);
            var email = _emailRepository.Get().FirstOrDefault(e => e.Id ==  id);


            email.Update(
                emailRequest.Name,
                emailRequest.Username,
                emailRequest.Password,
                emailRequest.Phone,
                emailRequest.LastName,
                emailRequest.BirthDate,
                platform,
                emailAddress
            );

            var result = _emailRepository.Update(email);
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
            var provider = _emailRepository.Get().FirstOrDefault(e => e.Id == id);

            var success = _emailRepository.Remove(provider);

            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<Email>> GetEmail()
        {
            var emails =  _emailRepository.Get()
                .Include(e => e.Platform)
                .ThenInclude(e => e.Image)
                .ThenInclude(e => e.StoredFile)
                .Include(e=>e.EmailAddress)
                .ThenInclude(e => e.Provider)
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

                var provider = _emailRepository.Get()
                .Where(e => e.Id == id)
                .Include(e => e.EmailAddress)
                .ThenInclude(e => e.Provider)
                .ThenInclude(e => e.Image)
                .ThenInclude(e => e.StoredFile)
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

