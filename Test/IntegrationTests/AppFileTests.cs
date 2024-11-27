using Application.Dtos.Default;
using Domain.Entitites;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Test.Utils;

namespace Test.IntegrationTests
{
    public class AppFileTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly UserTests _userTests;

        public AppFileTests(BasicTests factory)
        {
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
        public async Task<BaseResponse<AppFile>> Post_File_Put()
        {
            var newFile = TestUtils.CreateFormFile("Assets/PUT.png");
            var uploadResponse = await _client.PostAsync("/upload-file", TestUtils.ToFormData(new { image = newFile }));
            var uploadResponseString = await uploadResponse.Content.ReadAsStringAsync();
            var uploadedFile = JsonSerializer.Deserialize<BaseResponse<AppFile>>(uploadResponseString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, uploadResponse.StatusCode);

            return uploadedFile;
        }
        [Fact]
        public async Task<BaseResponse<AppFile>> Post_File_Post()
        {
            var newFile = TestUtils.CreateFormFile("Assets/POST.png");
            var uploadResponse = await _client.PostAsync("/upload-file", TestUtils.ToFormData(new { image = newFile }));
            var uploadResponseString = await uploadResponse.Content.ReadAsStringAsync();
            var uploadedFile = JsonSerializer.Deserialize<BaseResponse<AppFile>>(uploadResponseString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, uploadResponse.StatusCode);

            return uploadedFile;
        }

        [Fact]
        public async Task Get_Files()
        {
            var uploadResponse = await Post_File_Post();
            var getFilesResponse = await _client.GetAsync("/get-files");
            var getFilesString = await getFilesResponse.Content.ReadAsStringAsync();
            var files = JsonSerializer.Deserialize<BaseResponse<List<AppFile>>>(getFilesString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, getFilesResponse.StatusCode);
            Assert.Contains(files.Data, file => file.Id == uploadResponse.Data.Id);
        }

        [Fact]
        public async Task Delete_File()
        {
            var uploadedFile = await Post_File_Post();
            var deleteResponse = await _client.DeleteAsync(@$"/delete-file?id={uploadedFile.Data.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

            var getFilesResponse = await _client.GetAsync("/get-files");
            var getFilesString = await getFilesResponse.Content.ReadAsStringAsync();
            var files = JsonSerializer.Deserialize<BaseResponse<List<AppFile>>>(getFilesString, _jsonOptions);

            Assert.DoesNotContain(files.Data, file => file.Id == uploadedFile.Data.Id);
        }
    }
}
