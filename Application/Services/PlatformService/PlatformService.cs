using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Application.Dtos.Provider.Base;
using Application.Services.AppFileService;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlatformService: IPlatformService
    {

        private readonly IBaseRepository<Platform> _platformRepository;
        private readonly IAppFileService _appFilseService;

        public PlatformService
        (
            IAppFileService appFilseService,
            IBaseRepository<Platform> platformRepository
        )
        {
            _platformRepository = platformRepository;
            _appFilseService = appFilseService;
        }


        public BaseResponse<Platform> Register(PlatformRequest platRequest)
        {

            var plat = new Platform();
            var file = _appFilseService.InsertFile(platRequest.Image);

            plat.Create(
                platRequest.Name,
                file.Data
            );

            var addResult = _platformRepository.AddAsync(plat).Result;
            var response = new BaseResponse<Platform>(addResult is not null);

            response.Data = addResult;

            if (response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public BaseResponse<Platform> Update(PlatformRequest platRequest, int id)
        {
            var plat = _platformRepository.GetAsync(id).Result;
            var file = _appFilseService.InsertFile(platRequest.Image);

            plat.Update(
                platRequest.Name,
                file.Data

            );

            var success = _platformRepository.UpdateAsync(plat);
            var response = new BaseResponse<Platform>(success);

            response.Data = plat;

            if (response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }

            return response;

        }
        public DefaultResponse Delete(int id)
        {
            var plat = _platformRepository.GetAsync(id).Result;
            var success = _platformRepository.RemoveAsync(plat);
            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<Platform>> GetAll()
        {

            var plats = _platformRepository.GetAll()
                .Include(e=>e.Image)
                .ToList();
            var response = new BaseResponse<List<Platform>>()
            {
                Data = plats,
                Success = true
            };

            return response;
        }
        public BaseResponse<Platform> GetPlatformById(int id)
        {

            var provider = _platformRepository.GetAll()
                .Where(e=>e.Id==id)
                .Include(e=>e.Image)
                .FirstOrDefault();

            var response = new BaseResponse<Platform>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
