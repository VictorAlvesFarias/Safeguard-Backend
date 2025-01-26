using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Application.Services.AppFileService;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProviderService : IProviderService
    {
        public readonly IBaseRepository<Provider> _providerRepository;
        private readonly IAppFileService _appFileService;

        public ProviderService
        (
            IBaseRepository<Provider> providerRepository,
            IAppFileService appFileService
        )
        { 
            _providerRepository = providerRepository;
            _appFileService = appFileService;
        }

        public BaseResponse<Provider> RegisterProvider(ProviderRequest providerRequest)
        {
            var newProvider = new Provider();

            newProvider.Create(
                providerRequest.Name,
                providerRequest.Description,
                providerRequest.Signature,
                providerRequest.ImageId
            );

            var provider = _providerRepository.AddAsync(newProvider).Result;
            var response = new BaseResponse<Provider>(newProvider is not null);

            response.Data = provider;

            if (!response.Success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public BaseResponse<Provider> UpdateProvider(ProviderRequest providerRequest, int id)
        {
            var provider = _providerRepository.GetAll()
                .Include(e=>e.Image)
                .Where(e=>id == e.Id)
                .FirstOrDefault();

            provider.Update(
                providerRequest.Name,
                providerRequest.Description,
                providerRequest.Signature,
                providerRequest.ImageId
            );

            var success =  _providerRepository.UpdateAsync(provider);
            var response = new BaseResponse<Provider>(success);

            response.Data = provider;

            return response;

        }
        public DefaultResponse DeleteProvider(int id)
        {
            var provider = _providerRepository.GetAsync(id).Result;
            var success = _providerRepository.RemoveAsync(provider);
            var response = new DefaultResponse(success);

            return response;
        }
        public BaseResponse<List<Provider>> GetAllProviders()
        {

            var providers = _providerRepository.GetAll()
                .OrderByDescending(e => e.Id)
                .Include(e => e.Image)
                .ToList();

            var response = new BaseResponse<List<Provider>>()
            {
                Data = providers,
                Success = true 
            };

            return response;
        }
        public BaseResponse<Provider> GetProviderById(int id)
        {

            var provider = _providerRepository.GetAll()
                .Where(e => e.Id == id)
                .Include(e=>e.Image)
                .FirstOrDefault();

            var response = new BaseResponse<Provider>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
