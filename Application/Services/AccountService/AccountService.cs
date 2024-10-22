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
    public class AccountService: IAccountService
    {

        private readonly IBaseRepository<Account> _accountRepository;
        private readonly IBaseRepository<Email> _emailRepository;
        private readonly IBaseRepository<Platform> _platformRepository;

        public AccountService
        (
             IBaseRepository<Account> accountRepository,
             IBaseRepository<Email> emailRepository,
             IBaseRepository<Platform> platformRepository
        )
        {
            _accountRepository = accountRepository;
            _emailRepository = emailRepository; 
            _platformRepository = platformRepository; 
        }

         
        public DefaultResponse RegisterAccount(AccountRequest accountRequest)
        {
            var email = _emailRepository.GetAsync(accountRequest.EmailId).Result;
            var plat =  _platformRepository.GetAsync(accountRequest.PlatformId).Result;
            var account = new Account();

            account.Create(
                accountRequest.Name,
                accountRequest.Username,
                accountRequest.Password,
                accountRequest.Phone,
                email,
                plat
            );

            var addResult =  _accountRepository.AddAsync(account).Result;

            var response = new DefaultResponse(addResult.Success);

            if (!addResult.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public DefaultResponse UpdateAccount(AccountRequest accountRequest, int id)
        {
            var account =  _accountRepository.GetAsync(id).Result;
            var email =  _emailRepository.GetAsync(accountRequest.EmailId).Result;
            var plat =  _platformRepository.GetAsync(accountRequest.PlatformId).Result;

            account.Update(
                accountRequest.Name,
                accountRequest.Username,
                accountRequest.Password,
                accountRequest.Phone,
                email,
                plat
            );

            var success = _accountRepository.UpdateAsync(account);

            var response = new DefaultResponse(success);

            return response;

        }
        public DefaultResponse DeleteAccount(int id)
        {
            var account =  _accountRepository.GetAsync(id);
            var success = _accountRepository.RemoveAsync(account.Result);
            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<Account>> GetAllAccounts()
        {

            var accounts = _accountRepository.GetAll()
                .Include(e => e.Platform)
                .ThenInclude(e => e.Image)
                .Include(e => e.Email.Provider)
                .ThenInclude(e => e.Image)
                .ToList();

            var response = new BaseResponse<List<Account>>()
            {
                Data = accounts.ToList(),
                Success = true
            };

            return response;
        }
        public BaseResponse<Account> GetAccountById(int id)
        {

            var provider = _accountRepository.GetAll()
                .Where(e => e.Id == id)
                .Include(e => e.Platform)
                .ThenInclude(e => e.Image)
                .Include(e => e.Email.Provider)
                .ThenInclude(e => e.Image)
                .FirstOrDefault();

            var response = new BaseResponse<Account>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
