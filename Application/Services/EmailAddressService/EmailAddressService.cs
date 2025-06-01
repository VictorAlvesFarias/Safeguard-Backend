using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailAddressService: IEmailAddressService
    {

        private readonly IBaseRepository<Provider> _providerRepository;
        private readonly IBaseRepository<EmailAddress> _emailAddressRepository;

        public EmailAddressService
        (
             IBaseRepository<Provider> providerRepository,
             IBaseRepository<EmailAddress> emailAddressRepository
        )
        {
            _providerRepository = providerRepository;
            _emailAddressRepository = emailAddressRepository;
        }
         
        public BaseResponse<EmailAddress> RegisterEmailAddress(EmailAddressRequest accountRequest)
        {
            var account = new EmailAddress();
            var provider = _providerRepository.Get().FirstOrDefault(e=>e.Id==accountRequest.ProviderId);

            account.Create(
                accountRequest.Username,
                provider
            );

            var addResult = _emailAddressRepository.AddAsync(account).Result;
            var response = new BaseResponse<EmailAddress>(addResult is not null);

            response.Data = addResult;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;
        }
        public BaseResponse<EmailAddress> UpdateEmailAddress(EmailAddressRequest accountRequest, int id)
        {
            var account = _emailAddressRepository.Get().FirstOrDefault(e=>e.Id == id);
            var provider = _providerRepository.Get().FirstOrDefault(e => e.Id == accountRequest.ProviderId);

            account.Update(
                account.Username,
                provider
            );

            var result = _emailAddressRepository.Update(account);
            var response = new BaseResponse<EmailAddress>(result);

            response.Data = account;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;

        }
        public DefaultResponse DeleteEmailAddress(int id)
        {
            var account = _emailAddressRepository.Get().FirstOrDefault(e=> e.Id == id);
            var success = _emailAddressRepository.Remove(account);
            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<EmailAddress>> GetAllEmailsAddresses()
        {
            var accounts = _emailAddressRepository.Get()
                .Include(e => e.Provider)
                .ThenInclude(e => e.Image)
                .ThenInclude(e => e.StoredFile)
                .ToList();

            var response = new BaseResponse<List<EmailAddress>>()
            {
                Data = accounts.ToList(),
                Success = true
            };

            return response;
        }
        public BaseResponse<EmailAddress> GetEmailAddressById(int id)
        {

            var provider = _emailAddressRepository.Get()
                .Where(e => e.Id == id)
                .Include(e => e.Provider)
                .ThenInclude(e => e.Image)
                .ThenInclude(e => e.StoredFile)
                .FirstOrDefault();

            var response = new BaseResponse<EmailAddress>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
