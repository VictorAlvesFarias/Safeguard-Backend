﻿using Application.Dtos.Default;
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


        public async Task<DefaultResponse> RegisterAccount(AccountRequest accountRequest)
        {
            var email = await _emailRepository.GetAsync(accountRequest.EmailId);
            var plat = await _platformRepository.GetAsync(accountRequest.PlatformId);
            var account = new Account();

            account.Create(
                accountRequest.Name,
                accountRequest.Username,
                accountRequest.Password,
                accountRequest.Phone,
                email,
                plat
            );

            var success = await _accountRepository.AddAsync(account);

            var response = new DefaultResponse(success);

            if (!success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public async Task<DefaultResponse> UpdateAccount(AccountRequest accountRequest, int id)
        {
            var account = await _accountRepository.GetAsync(id);
            var email = await _emailRepository.GetAsync(accountRequest.EmailId);
            var plat = await _platformRepository.GetAsync(accountRequest.PlatformId);

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
        public async Task<DefaultResponse> DeleteAccount(int id)
        {
            var account = await _accountRepository.GetAsync(id);

            var success = _accountRepository.RemoveAsync(account);

            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Account>>> GetAllAccounts()
        {

            var accounts = _accountRepository.GetAll()
                .Include(e => e.Platform)
                .Include(e => e.Email.Provider)
                .ToList();

            var response = new BaseResponse<List<Account>>()
            {
                Data = accounts.ToList(),
                Success = true
            };

            return response;
        }
        public async Task<BaseResponse<Account>> GetAccountById(int id)
        {

            var provider = _accountRepository.GetAll()
                .Where(e => e.Id == id)
                .Include(e => e.Platform)
                .Include(e => e.Email.Provider)
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
