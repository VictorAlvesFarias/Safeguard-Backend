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
         DefaultResponse RegisterEmail(EmailRequest emailRequest);
         DefaultResponse UpdateEmail(EmailRequest emailRequest, int id);
         DefaultResponse DeleteEmail(int id);
         BaseResponse<List<Email>> GetAllEmail();
         BaseResponse<Email> GetEmailById(int id);
    }
}
