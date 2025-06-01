using Application.Dtos.Default;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IBaseRepository<StoredFile> _storedFileRepository;
        public AppFileService(
             IBaseRepository<AppFile> appFileRepository,
             IBaseRepository<StoredFile> storedFileRepository
        ) {
            _appFileRepository = appFileRepository;
            _storedFileRepository = storedFileRepository;
        }
        public BaseResponse<AppFile> InsertFile(IFormFile req)
        {
            var file = new AppFile();
            var storedFile = new StoredFile();
            var stream = req.OpenReadStream();
            
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo( memoryStream );

                storedFile.Create(
                    req.FileName,
                    req.ContentType,
                     Convert.ToBase64String(memoryStream.ToArray()) 
                );
            }
                
            var storedFileAddedFile = _storedFileRepository.AddAsync(storedFile).Result;

            file.Create(
                storedFileAddedFile
            );

            var appFileAddResult = _appFileRepository.AddAsync(file).Result;
            var response = new BaseResponse<AppFile>(appFileAddResult is not null && storedFileAddedFile is not null);

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }
            else
            {
                response.Data = file;
            }

            return response;
        }
        public DefaultResponse DeleteFile(int id)
        {
            var appFile = _appFileRepository.Get().FirstOrDefault(e => e.Id == id);
            var storedFile = _storedFileRepository.Get().FirstOrDefault(e => e.Id == appFile.StoredFileId);
            var removeAppFileResult = _appFileRepository.Remove(appFile);
            var removeStoredFileResult = _storedFileRepository.Remove(storedFile);
            var response = new DefaultResponse(removeAppFileResult && removeStoredFileResult);

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;
        }
        public BaseResponse<List<AppFile>> GetFiles()
        {
            var result = _appFileRepository.Get()
                .OrderByDescending(e=>e.Id)
                .Include(e=>e.StoredFile)
                .ToList();
            var response = new BaseResponse<List<AppFile>>(result is not null);

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
    }
}
