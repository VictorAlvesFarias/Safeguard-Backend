using Application.Dtos.Default;
using Application.Dtos.Provider.Base;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text.Json;
using Test.Utils;

namespace Test.IntegrationTests
{
    public class PlatformTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        public PlatformTests(BasicTests factory)
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
        public async Task<BaseResponse<Provider>> Post_Platform()
        {
            var newProvider = new ProviderRequest() {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("Assets/POST.png")
            };
            var createProviderResponse = await _client.PostAsync("/create-provider",TestUtils.ToFormData(newProvider));
            var providerString = await createProviderResponse.Content.ReadAsStringAsync();
            var provider = JsonSerializer.Deserialize<BaseResponse<Provider>>(providerString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);

            return provider;
        }
        [Fact]
        public async Task Delete_Platform()
        {
            var provider = Post_Platform().Result.Data;
            var deleteProviderResponse = await _client.DeleteAsync(@$"/delete-provider?id={provider.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteProviderResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Platform()
        {
            var provider = Post_Platform().Result.Data;
            var newProviderUpdate = new ProviderRequest()
            {
                Description = "Teste",
                Name = "Teste",
                Signature = "teat",
                Image = TestUtils.CreateFormFile("Assets/PUT.png")
            };
            var expectedProvider = new Provider();
            var appFile = new AppFile();

            using (var memoryStream = new MemoryStream())
            {
                newProviderUpdate.Image.OpenReadStream().CopyTo(memoryStream);
                var base64Image = Convert.ToBase64String(memoryStream.ToArray());

                appFile.Create(
                    newProviderUpdate.Image.FileName,
                    newProviderUpdate.Image.ContentType,
                    base64Image
                );
            }

            expectedProvider.Create(
                newProviderUpdate.Name,
                newProviderUpdate.Description,
                newProviderUpdate.Signature,
                appFile
            );

            var updateProviderResponse = await _client.PutAsync(@$"/edit-provider?id={provider.Id}", TestUtils.ToFormData(newProviderUpdate));
            var updateProviderString = await updateProviderResponse.Content.ReadAsStringAsync();
            var updateProvider = JsonSerializer.Deserialize<BaseResponse<Provider>>(updateProviderString, _jsonOptions);

            expectedProvider.Image.Id = updateProvider.Data.Image.Id;
            expectedProvider.Id = updateProvider.Data.Id;
            expectedProvider.ImageId = updateProvider.Data.ImageId;

            Assert.Equal(expectedProvider.Name, updateProvider.Data.Name);
            Assert.Equal(expectedProvider.Description, updateProvider.Data.Description);
            Assert.Equal(expectedProvider.Signature, updateProvider.Data.Signature);
            Assert.Equal(expectedProvider.ImageId, updateProvider.Data.ImageId);
        }
    }
}
