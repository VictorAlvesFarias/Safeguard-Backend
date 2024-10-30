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
    public class EmailTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ProviderTests _providerTests;
        private readonly UserTests _userTests;
        public EmailTests(BasicTests factory)
        {
            _providerTests = new ProviderTests(factory);
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
        public async Task<BaseResponse<Email>> Post_Email()
        {
            var provider = await _providerTests.Post_Provider();
            var newEmail = new EmailRequest() {
                Name = "Teste",
                Password = "password",
                Phone = "teste",
                Username = "teste",
                ProviderId = provider.Data.Id
            };
            var createEmailResponse = await _client.PostAsync("/create-email",newEmail.ToJson());
            var emailString = await createEmailResponse.Content.ReadAsStringAsync();
            var email = JsonSerializer.Deserialize<BaseResponse<Email>>(emailString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, createEmailResponse.StatusCode);

            return email;
        }
        [Fact]
        public async Task Delete_Email()
        {
            var email = Post_Email().Result.Data;
            var deleteEmailResponse = await _client.DeleteAsync(@$"/delete-email?id={email.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteEmailResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Email()
        {
            var email = await Post_Email();
            var provider = await _providerTests.Post_Provider();
            var newEmailUpdate = new EmailRequest()
            {
                Name = "Teste 2",
                Password = "password 2",
                Phone = "teste 2",
                Username = "teste 2",
                ProviderId = provider.Data.Id
            };
            var updateEmailResponse = await _client.PutAsync(@$"/edit-email?id={email.Data.Id}", newEmailUpdate.ToJson());
            var updateEmailString = await updateEmailResponse.Content.ReadAsStringAsync();
            var updatedEmail = JsonSerializer.Deserialize<BaseResponse<Email>>(updateEmailString, _jsonOptions);

            Assert.Equal(newEmailUpdate.Name, updatedEmail.Data.Name);
            Assert.Equal(newEmailUpdate.ProviderId, updatedEmail.Data.ProviderId);
            Assert.Equal(newEmailUpdate.Username, updatedEmail.Data.Username);
            Assert.Equal(newEmailUpdate.Password, updatedEmail.Data.Password);
            Assert.Equal(newEmailUpdate.Phone, updatedEmail.Data.Phone);
        }
    }
}
