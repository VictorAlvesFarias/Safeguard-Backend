using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Test.Utils;

namespace Test.IntegrationTests
{
    public class ProviderTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        public ProviderTests(BasicTests factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    
        [Fact]
        public async Task Post_Provider()
        {
            var newProvider = new ProviderRequest() {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("image/png")
            };
            var createProviderResponse = await _client.PostAsync("/create-provider",TestUtils.ToFormData(newProvider));

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);
        }
        [Fact]
        public async Task Delete_Provider()
        {
            var newProvider = new ProviderRequest()
            {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("image/png")
            };
            var createProviderResponse = await _client.PostAsync("/create-provider", TestUtils.ToFormData(newProvider));

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);

            var providerString = await createProviderResponse.Content.ReadAsStringAsync();
            var provider = JsonSerializer.Deserialize<BaseResponse<Provider>>(providerString,_jsonOptions);
            var deleteProviderResponse = await _client.DeleteAsync(@$"/delete-provider?id={provider.Data.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteProviderResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Provider()
        {
            var newProvider = new ProviderRequest()
            {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("image/png")
            };
            var newProviderUpdate = new ProviderRequest()
            {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("image/jpg")
            };
            var expectedProvider = new Provider();
            var appFile = new AppFile();
            var createProviderResponse = await _client.PostAsync("/create-provider", TestUtils.ToFormData(newProvider));
            var providerString = await createProviderResponse.Content.ReadAsStringAsync();
            var provider = JsonSerializer.Deserialize<BaseResponse<Provider>>(providerString, _jsonOptions);

            appFile.Create(
                "test-image.png",
                "image/jpg",
                ""
            );
            expectedProvider.Create(
                newProviderUpdate.Name,
                newProviderUpdate.Description,
                newProviderUpdate.Signature,
                appFile
            );

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);


            var updateProviderResponse = await _client.PutAsync(@$"/edit-provider?id={provider.Data.Id}", TestUtils.ToFormData(newProviderUpdate));
            var updateProviderString = await updateProviderResponse.Content.ReadAsStringAsync();
            var updateProvider = JsonSerializer.Deserialize<BaseResponse<Provider>>(updateProviderString, _jsonOptions);

            expectedProvider.Image.Id = updateProvider.Data.Image.Id;
            expectedProvider.Image.CreateDate = updateProvider.Data.Image.CreateDate;
            expectedProvider.Image.UpdateDate = updateProvider.Data.Image.UpdateDate;
            expectedProvider.Id = updateProvider.Data.Id;
            expectedProvider.ImageId = updateProvider.Data.ImageId;
            expectedProvider.CreateDate = updateProvider.Data.CreateDate;
            expectedProvider.UpdateDate = updateProvider.Data.UpdateDate;

            Assert.Equal(HttpStatusCode.OK, updateProviderResponse.StatusCode);
            Assert.Equal(expectedProvider, updateProvider.Data);
        }
    }
}
