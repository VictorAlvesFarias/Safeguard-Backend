using Application.Dtos.Default;
using Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Application.Services.AppFileService
{
    public interface IAppFileService
    {
        BaseResponse<AppFile>  InsertFile(IFormFile req);
    }
}
