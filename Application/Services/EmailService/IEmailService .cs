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
         BaseResponse<Email> RegisterEmail(EmailRequest emailRequest);
         BaseResponse<Email> UpdateEmail(EmailRequest emailRequest, int id);
         DefaultResponse DeleteEmail(int id);
         BaseResponse<List<Email>> GetEmail();
         BaseResponse<Email> GetEmailById(int id);
    }
}
