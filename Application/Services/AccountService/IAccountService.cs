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
        Task<DefaultResponse> RegisterAccount(AccountRequest provider);
        Task<DefaultResponse> UpdateAccount(AccountRequest provider, int id);
        Task<DefaultResponse> DeleteAccount(int id);
        Task<BaseResponse<List<Account>>> GetAllAccounts();
        Task<BaseResponse<Account>> GetAccountById(int id);
    }
}
