using Domain.Entitites;

namespace Application.Dtos.Provider.Base
{

    public class EmailAddressRequest
    {
        public string Username { get; set; }
        public int ProviderId { get; set; }
    }
}
