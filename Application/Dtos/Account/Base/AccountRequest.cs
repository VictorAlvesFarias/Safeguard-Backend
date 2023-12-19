using Domain.Entitites;

namespace Application.Dtos.Provider.Base
{

    public class AccountRequest
    {
        public int EmailId { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Phone { get; set; }
    }
}
