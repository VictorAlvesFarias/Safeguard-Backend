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
    public interface IProviderService
    {
         Task<DefaultResponse> RegisterProvider(ProviderRequest provider);
         Task<DefaultResponse> UpdateProvider(ProviderRequest provider, int id);
         Task<DefaultResponse> DeleteProvider(int id);
         Task<BaseResponse<List<Provider>>> GetAllProviders();
         Task<BaseResponse<Provider>> GetProviderById(int id);
    }
}
