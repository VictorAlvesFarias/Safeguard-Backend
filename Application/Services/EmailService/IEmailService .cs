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
    public interface IEmailService
    {
         Task<DefaultResponse> RegisterProvider(EmailRequest emailRequest);
         Task<DefaultResponse> UpdateProvider(EmailRequest emailRequest, int id);
        Task<DefaultResponse> DeleteProvider(int id);
         Task<BaseResponse<List<Email>>> GetAllProviders();
    }
}
