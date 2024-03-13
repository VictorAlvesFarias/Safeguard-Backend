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
         Task<DefaultResponse> RegisterEmail(EmailRequest emailRequest);
         Task<DefaultResponse> UpdateEmail(EmailRequest emailRequest, int id);
        Task<DefaultResponse> DeleteEmail(int id);
         Task<BaseResponse<List<Email>>> GetAllEmail();
    }
}
