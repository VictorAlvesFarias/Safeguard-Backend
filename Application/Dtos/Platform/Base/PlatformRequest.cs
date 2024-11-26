using Domain.Entitites;
using Microsoft.AspNetCore.Http;

namespace Application.Dtos.Platform.Base
{

    public class PlatformRequest
    {
        public string Name { get; set; }
        public int ImageId { get; set; }
    }
}
