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
        Task<DefaultResponse> RegisterProvider(AccountRequest provider);
        Task<DefaultResponse> UpdateProvider(AccountRequest provider, int id);
        Task<DefaultResponse> DeleteProvider(int id);
        Task<BaseResponse<List<Account>>> GetAllProviders();
    }
}
