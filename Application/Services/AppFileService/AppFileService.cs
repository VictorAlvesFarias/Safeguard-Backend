using Application.Dtos.Default;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AppFileService
{
    public class AppFileService : IAppFileService
    {
        private readonly IBaseRepository<AppFile> _appFileRepository;
        public AppFileService(
             IBaseRepository<AppFile> appFileRepository
        ) {
            _appFileRepository = appFileRepository;
        }
        public BaseResponse<AppFile> InsertFile(IFormFile req)
        {
            var file = new AppFile();
            var stream = req.OpenReadStream();
            
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo( memoryStream );

                file.Create(
                    req.FileName,
                    req.ContentType,
                     Convert.ToBase64String(memoryStream.ToArray()) 
                );
            }
                
            var result = _appFileRepository.AddAsync(file).Result;
            var response = new BaseResponse<AppFile>(result is not null);

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }
            else
            {
                response.Data = result;
            }

            return response;
        }
        public DefaultResponse DeleteFile(int id)
        {
            var result = _appFileRepository.RemoveAsync(id);
            var response = new DefaultResponse(result);

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;
        }
        public BaseResponse<List<AppFile>> GetFiles()
        {

            var result = _appFileRepository.GetAll().OrderByDescending(e=>e.Id).ToList();
            var response = new BaseResponse<List<AppFile>>(result is not null);

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }
            else {
                response.Data = result;
            }

            return response;
        }
    }
}
