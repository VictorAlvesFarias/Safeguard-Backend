using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using App.Program;
using Application.Dtos.Default;
using Application.Dtos.User.Create;
using Application.Dtos.User.Login;
using Domain.Entitites;
using System.Net;
using Test.Utils;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Test.IntegrationTests
{
    public class UserTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;
        private readonly JsonSerializerOptions _jsonOptions;
        public UserTests(BasicTests factory)
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
        public async Task<BaseResponse<LoginUserResponse>> User_Login()
        {
            var newLogin = new LoginUserRequest()
            {
                AccessKey = "userTest@gmail.com",
                Password = "Teste123*"
            };
            var loginResponse = await _client.PostAsync("/sign-in", newLogin.ToJson());
            var loginString = await loginResponse.Content.ReadAsStringAsync();
            var login = JsonSerializer.Deserialize<BaseResponse<LoginUserResponse>>(loginString, _jsonOptions);

            Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

            return login;
        }
    }
}
