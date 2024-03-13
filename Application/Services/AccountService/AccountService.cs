using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
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

        public AccountService
        (
             IBaseRepository<Account> accountRepository,
             IBaseRepository<Email> emailRepository
        )
        {
            _accountRepository = accountRepository;
            _emailRepository = emailRepository; 
        }


        public async Task<DefaultResponse> RegisterProvider(AccountRequest accountRequest)
        {
            var email = await _emailRepository.GetAsync(accountRequest.EmailId);

            var account = new Account();

            account.Create(
                accountRequest.Name,
                accountRequest.Username,
                accountRequest.Password,
                accountRequest.Phone,
                email,
                accountRequest.Image
            );

            var success = await _accountRepository.AddAsync(account);

            var response = new DefaultResponse(success);

            if (!success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public async Task<DefaultResponse> UpdateProvider(AccountRequest accountRequest, int id)
        {
            var account = await _accountRepository.GetAsync(id);

            var email = await _emailRepository.GetAsync(accountRequest.EmailId);

            account.Update(
                accountRequest.Name,
                accountRequest.Username,
                accountRequest.Password,
                accountRequest.Phone,
                email,
                accountRequest.Image

            );

            var success = _accountRepository.UpdateAsync(account);

            var response = new DefaultResponse(success);

            return response;

        }
        public async Task<DefaultResponse> DeleteProvider(int id)
        {
            var account = await _accountRepository.GetAsync(id);

            var success = _accountRepository.RemoveAsync(account);

            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Account>>> GetAllProviders()
        {

            var accounts = await _accountRepository.GetAllAsync();

            var response = new BaseResponse<List<Account>>()
            {
                Data = accounts.ToList(),
                Success = true
            };

            return response;
        }
    }
}
