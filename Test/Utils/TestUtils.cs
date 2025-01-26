using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Test.Utils
{
    public static class TestUtils
    {
        public static StreamContent CreateStreamFormFile(string imagePath)
        {
            var provider = new FileExtensionContentTypeProvider();

            // Obtém o tipo de conteúdo com base na extensão
            if (!provider.TryGetContentType(imagePath, out var contentType))
            {
                contentType = "application/octet-stream"; // Tipo padrão caso não encontre
            }

            var stream = File.OpenRead(imagePath);
            var imageContent = new StreamContent(stream);
            imageContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            return imageContent;
        }
        public static IFormFile CreateFormFile(string imagePath)
        {
            var provider = new FileExtensionContentTypeProvider();

            // Obtém o tipo de conteúdo com base na extensão
            if (!provider.TryGetContentType(imagePath, out var contentType))
            {
                contentType = "application/octet-stream"; // Tipo padrão caso não encontre
            }

            var fileName = Path.GetFileName(imagePath);
            var stream = File.OpenRead(imagePath);
            var formFile = new FormFile(stream, 0, stream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType,
            };

            return formFile;
        }
        public static MultipartFormDataContent ToFormData(this object obj)
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
        public static StringContent ToJson(this object obj)
        {
            var jsonContent = new StringContent(
               JsonSerializer.Serialize(obj),
               Encoding.UTF8,
               "application/json"
           );

           return jsonContent;
        }
    }
}
