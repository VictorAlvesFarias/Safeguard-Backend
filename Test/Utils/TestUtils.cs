using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Test.Utils
{
    public static class TestUtils
    {
        public static StreamContent CreateStreamFormFile(string contentType)
        {
            var stream = new MemoryStream();
            var imageContent = new StreamContent(stream);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return imageContent;
        }
        public static IFormFile CreateFormFile(string contentType)
        {
            var fileName = "test-image.png";
            var stream = new MemoryStream();
            var formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
            };

            return formFile;
        }
        public static MultipartFormDataContent ToFormData(object obj)
        {
            var formData = new MultipartFormDataContent();

            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
                var value = prop.GetValue(obj);

                if (value == null) continue;

                if (value is byte[] bytes)
                {
                    var byteContent = new ByteArrayContent(bytes);
                    byteContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                    formData.Add(byteContent, prop.Name, prop.Name);
                }

                else if (value is IFormFile file)
                {
                    var streamContent = new StreamContent(file.OpenReadStream());
                    streamContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);
                    formData.Add(streamContent, prop.Name, file.FileName);
                }

                else
                {
                    formData.Add(new StringContent(value.ToString()), prop.Name);
                }
            }

            return formData;
        }
    }
}
