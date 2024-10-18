using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
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

        public PlatformService
        (
             IBaseRepository<Platform> platformRepository
        )
        {
            _platformRepository = platformRepository;
        }


        public async Task<DefaultResponse> Register(PlatformRequest platRequest)
        {

            var plat = new Platform();

            plat.Create(
                platRequest.Name,
                platRequest.Image
            );

            var success = await _platformRepository.AddAsync(plat);
            var response = new DefaultResponse(success);

            if (!success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public async Task<DefaultResponse> Update(PlatformRequest platRequest, int id)
        {
            var plat = await _platformRepository.GetAsync(id);

            plat.Update(
                platRequest.Name,
                platRequest.Image

            );

            var success = _platformRepository.UpdateAsync(plat);
            var response = new DefaultResponse(success);

            return response;

        }
        public async Task<DefaultResponse> Delete(int id)
        {
            var plat = await _platformRepository.GetAsync(id);
            var success = _platformRepository.RemoveAsync(plat);
            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Platform>>> GetAll()
        {

            var plats = _platformRepository.GetAll().ToList();
            var response = new BaseResponse<List<Platform>>()
            {
                Data = plats,
                Success = true
            };

            return response;
        }
        public async Task<BaseResponse<Platform>> GetPlatformById(int id)
        {

            var provider = await _platformRepository.GetAsync(id);

            var response = new BaseResponse<Platform>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
