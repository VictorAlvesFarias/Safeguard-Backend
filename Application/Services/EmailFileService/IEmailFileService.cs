using Application.Dtos.Default;
using Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Application.Services.AppFileService
{
    public interface IEmailFileService
    {
        BaseResponse<EmailFile>  InsertFile(IFormFile req, int emailId);
        DefaultResponse DeleteFile(int id);
        BaseResponse<List<EmailFile>> GetFiles(int emailId);
    }
}
