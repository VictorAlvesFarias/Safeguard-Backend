using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IPlatformService
    {
        Task<DefaultResponse> Register(PlatformRequest plat);
        Task<DefaultResponse> Update(PlatformRequest plat, int id);
        Task<DefaultResponse> Delete(int id);
        Task<BaseResponse<List<Platform>>> GetAll();
    }
}
