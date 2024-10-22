using Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Platform.Base
{

    public class PlatformRequest
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
    }
}
