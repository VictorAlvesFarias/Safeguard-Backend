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
    public class AccountTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly EmailTests _emailTests;
        private readonly UserTests _userTests;
        private readonly PlatformTests _platformTests;

        public AccountTests(BasicTests factory)
        {
            _userTests = new UserTests(factory);
            _emailTests = new EmailTests(factory);
            _platformTests = new PlatformTests(factory);
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
        public async Task<BaseResponse<Account>> Post_Account()
        {
            var email = _emailTests.Post_Email().Result;
            var platform = _platformTests.Post_Platform().Result;
            var newAccount = new AccountRequest()
            {
                Name = "Teste",
                EmailId = email.Data.Id,
                Password = email.Data.Password,
                PlatformId = platform.Data.Id,
                Phone = "99999999999",
                Username = "teste"
            };
            var createAccountResponse = await _client.PostAsync("/create-account", newAccount.ToJson());
            var accountString = await createAccountResponse.Content.ReadAsStringAsync();
            var account = JsonSerializer.Deserialize<BaseResponse<Account>>(accountString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, createAccountResponse.StatusCode);

            return account;
        }
        [Fact]
        public async Task Delete_Account()
        {
            var account = Post_Account().Result.Data;
            var deleteAccountResponse = await _client.DeleteAsync(@$"/delete-account?id={account.Id}");

            Assert.Equal(HttpStatusCode.OK, deleteAccountResponse.StatusCode);
        }
        [Fact]
        public async Task Update_Account()
        {
            var email = _emailTests.Post_Email().Result;
            var platform = _platformTests.Post_Platform().Result;
            var account = Post_Account().Result.Data;
            var newAccountUpdate = new AccountRequest()
            {
                Name = "Teste Atualizado",
                EmailId = email.Data.Id,
                Password = "newpassword",
                PlatformId = platform.Data.Id,
                Phone = "88888888888",
                Username = "testeAtualizado"
            };
            var updateAccountResponse = await _client.PutAsync(@$"/edit-account?id={account.Id}", newAccountUpdate.ToJson());
            var updateAccountString = await updateAccountResponse.Content.ReadAsStringAsync();
            var updatedAccount = JsonSerializer.Deserialize<BaseResponse<Account>>(updateAccountString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, updateAccountResponse.StatusCode);
            Assert.Equal(newAccountUpdate.Name, updatedAccount.Data.Name);
            Assert.Equal(newAccountUpdate.EmailId, updatedAccount.Data.EmailId);
            Assert.Equal(newAccountUpdate.PlatformId, updatedAccount.Data.PlatformId);
            Assert.Equal(newAccountUpdate.Phone, updatedAccount.Data.Phone);
            Assert.Equal(newAccountUpdate.Username, updatedAccount.Data.Username);
        }
    }
}
