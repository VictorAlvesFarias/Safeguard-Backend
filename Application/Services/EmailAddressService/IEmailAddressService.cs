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
    public interface IEmailAddressService
    {
        BaseResponse<EmailAddress> RegisterEmailAddress(EmailAddressRequest provider);
        BaseResponse<EmailAddress> UpdateEmailAddress(EmailAddressRequest provider, int id);
        DefaultResponse DeleteEmailAddress(int id);
        BaseResponse<List<EmailAddress>> GetAllEmailsAddresses();
        BaseResponse<EmailAddress> GetEmailAddressById(int id);
    }
}
