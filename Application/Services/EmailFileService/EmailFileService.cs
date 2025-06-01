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
    public class EmailFileService : IEmailFileService
    {
        private readonly IBaseRepository<EmailFile> _appFileRepository;
        private readonly IBaseRepository<StoredFile> _storedFileRepository;
        private readonly IBaseRepository<Email> _emailRepository;
        public EmailFileService(
             IBaseRepository<EmailFile> appFileRepository,
             IBaseRepository<StoredFile> storedFileRepository,
             IBaseRepository<Email> emailRepository
        ) {
            _appFileRepository = appFileRepository;
            _storedFileRepository = storedFileRepository;
            _emailRepository = emailRepository;
        }
        public BaseResponse<EmailFile> InsertFile(IFormFile req, int emailId)
        {
            var file = new EmailFile();
            var storedFile = new StoredFile();
            var email = _emailRepository.Get().FirstOrDefault(e=>e.Id == emailId);
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
                storedFileAddedFile,
                email
            );

            var appFileAddResult = _appFileRepository.AddAsync(file).Result;
            var response = new BaseResponse<EmailFile>(appFileAddResult is not null && storedFileAddedFile is not null);

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
        public BaseResponse<List<EmailFile>> GetFiles(int emailId)
        {
            var result = _appFileRepository.Get()
                .Where(e=>e.EmailId == emailId)
                .OrderByDescending(e=>e.Id)
                .Include(e=>e.StoredFile)
                .ToList();
            var response = new BaseResponse<List<EmailFile>>(result is not null);

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
