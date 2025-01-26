using Application.Dtos.Default;
using Application.Dtos.Platform.Base;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Test.Utils;

namespace Test.IntegrationTests
{
    public class PlatformTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly UserTests _userTests;
        private readonly AppFileTests _appFileTests;

        public PlatformTests(BasicTests factory)
        {
            _appFileTests = new AppFileTests(factory);
            _userTests = new UserTests(factory);
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userTests.User_Login().Result.Data.Token);
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }
    
        [Fact]
        public async Task<BaseResponse<Platform>> Post_Platform()
        {
            var image = await _appFileTests.Post_File_Post();
            var newPlatform = new PlatformRequest() {
                Name = "Teste",
                ImageId = image.Data.Id
            };
            var createPlatformResponse = await _client.PostAsync("/create-platform", TestUtils.ToFormData(newPlatform));
            var platformString = await createPlatformResponse.Content.ReadAsStringAsync();
            var platform = JsonSerializer.Deserialize<BaseResponse<Platform>>(platformString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, createPlatformResponse.StatusCode);

            return platform;
        }
        [Fact]
        public async Task Delete_Platform()
        {
            var platform = Post_Platform().Result.Data;
            var deletePlatformResponse = await _client.DeleteAsync(@$"/delete-platform?id={platform.Id}");

            Assert.Equal(HttpStatusCode.OK, deletePlatformResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Platform()
        {
            var platform = Post_Platform().Result.Data;
            var image = await _appFileTests.Post_File_Put();
            var newPlatformUpdate = new PlatformRequest()
            {
                Name = "Teste",
                ImageId = image.Data.Id
            };

            var updatePlatformResponse = await _client.PutAsync(@$"/edit-platform?id={platform.Id}", newPlatformUpdate.ToFormData());
            var updatePlatformString = await updatePlatformResponse.Content.ReadAsStringAsync();
            var updatePlatform = JsonSerializer.Deserialize<BaseResponse<Platform>>(updatePlatformString, _jsonOptions);

            Assert.Equal(newPlatformUpdate.Name, updatePlatform.Data.Name);
            Assert.NotEqual(platform.ImageId, updatePlatform.Data.ImageId);
        }
    }
}
