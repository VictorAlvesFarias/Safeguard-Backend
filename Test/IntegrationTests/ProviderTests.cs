using Application.Dtos.Provider.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test.IntegrationTests
{
    public class ProviderTests : IClassFixture<BasicTests>
    {
        private readonly HttpClient _client;
        private readonly BasicTests _factory;

        public ProviderTests(BasicTests factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
            });
        }

        [Fact]
        public async Task Post_Provider()
        {
            var fileName = "test-image.png";
            var contentType = "image/png";
            var stream = new MemoryStream();
            var formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
            };
            var newProvider = new ProviderRequest()
            {
                Description = "Description",
                Image = formFile,
                Name = "Name",
                Signature = "Signarute"
            };
            var form = new MultipartFormDataContent();
            var imageContent = new StreamContent(stream);

            imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            form.Add(new StringContent("ProviderTest"), "Description");
            form.Add(new StringContent("ProviderTest"), "Name");
            form.Add(new StringContent("ProviderTest"), "Signature");
            form.Add(imageContent, "Image", fileName);

            var createProviderResponse = await _client.PostAsync("/create-provider", form);

            Assert.Equal(HttpStatusCode.OK, createProviderResponse.StatusCode);
        }

    }
}
