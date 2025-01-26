using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Test.Utils;

namespace Test.IntegrationTests
{
    public class ProviderTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly UserTests _userTests;
        private readonly AppFileTests _appFileTests;
        public ProviderTests(BasicTests factory)
        {
            _userTests = new UserTests(factory);
            _appFileTests = new AppFileTests(factory);
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false, 

            });
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userTests.User_Login().Result.Data.Token);
            _jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    
        [Fact]
        public async Task<BaseResponse<Provider>> Post_Provider()
        {
            var image = await _appFileTests.Post_File_Post();
            var newProvider = new ProviderRequest() {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                ImageId = image.Data.Id   
            };
            var createProviderResponse = await _client.PostAsync("/create-provider",newProvider.ToFormData());
            var providerString = await createProviderResponse.Content.ReadAsStringAsync();
            var provider = JsonSerializer.Deserialize<BaseResponse<Provider>>(providerString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);

            return provider;
        }
        [Fact]
        public async Task Delete_Provider()
        {
            var provider = Post_Provider().Result.Data;
            var deleteProviderResponse = await _client.DeleteAsync(@$"/delete-provider?id={provider.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteProviderResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Provider()
        {
            var provider = Post_Provider().Result.Data;
            var image = await _appFileTests.Post_File_Put();
            var newProviderUpdate = new ProviderRequest()
            {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                ImageId = image.Data.Id
            };
            var updateProviderResponse = await _client.PutAsync(@$"/edit-provider?id={provider.Id}", newProviderUpdate.ToFormData());
            var updateProviderString = await updateProviderResponse.Content.ReadAsStringAsync();
            var updateProvider = JsonSerializer.Deserialize<BaseResponse<Provider>>(updateProviderString, _jsonOptions);

            Assert.Equal(newProviderUpdate.Name, updateProvider.Data.Name);
            Assert.Equal(newProviderUpdate.Description, updateProvider.Data.Description);
            Assert.Equal(newProviderUpdate.Signature, updateProvider.Data.Signature);
            Assert.NotEqual(provider.ImageId, updateProvider.Data.ImageId);
        }
    }
}
