using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IAccountService
    {
        BaseResponse<Account> RegisterAccount(AccountRequest provider);
        BaseResponse<Account> UpdateAccount(AccountRequest provider, int id);
        DefaultResponse DeleteAccount(int id);
        BaseResponse<List<Account>> GetAllAccounts();
        BaseResponse<Account> GetAccountById(int id);
    }
}
