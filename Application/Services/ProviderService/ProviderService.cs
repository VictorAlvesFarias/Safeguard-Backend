using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Infrastructure.Repositories.BaseRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProviderService:IProviderService
    {
        public readonly IBaseRepository<Provider> _providerRepository;
        public ProviderService
        (
            IBaseRepository<Provider> providerRepository
        )
        { 
            _providerRepository = providerRepository;
        }

        public async Task<DefaultResponse> RegisterProvider(ProviderRequest providerRequest)
        {
            var provider = new Provider();

            provider.Create(
                providerRequest.Name,
                providerRequest.Description,
                providerRequest.Signature,
                providerRequest.Image
            );

            var success = await _providerRepository.AddAsync(provider);

            var response = new DefaultResponse(success);
            
            if(!success)
            {
                response.AddError("Não foi possivel completar a operação");
            }


            return response;
        }
        public async Task<DefaultResponse> UpdateProvider(ProviderRequest providerRequest, int id)
        {
            var provider = await _providerRepository.GetAsync(id);

            provider.Update(
                providerRequest.Name,
                providerRequest.Description,
                providerRequest.Signature,
                providerRequest.Image
            );

            var success =  _providerRepository.UpdateAsync(provider);

            var response = new DefaultResponse(success);

            return response;

        }
        public async Task<DefaultResponse> DeleteProvider(int id)
        {
            var provider = await _providerRepository.GetAsync(id);

            var success =  _providerRepository.RemoveAsync(provider);

            var response = new DefaultResponse(success);

            return response;
        }
        public async Task<BaseResponse<List<Provider>>> GetAllProviders()
        {

            var providers = _providerRepository.GetAll().ToList();

            var response = new BaseResponse<List<Provider>>()
            {
                Data = providers,
                Success = true 
            };

            return response;
        }
        public async Task<BaseResponse<Provider>> GetProviderById(int id)
        {

            var provider = await _providerRepository.GetAsync(id);

            var response = new BaseResponse<Provider>()
            {
                Data = provider,
                Success = true
            };

            return response;
        }
    }
}
