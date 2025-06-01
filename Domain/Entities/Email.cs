namespace Domain.Entitites
{
    public class Email : BaseEntityUserRelation
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public EmailAddress EmailAddress { get; set; }
        public int? EmailAddressId { get; set; }
        public Platform Platform { get; set; }
        public int? PlatformId { get; set; }
        public void Update(string _name, string _username, string _password, string _phone, string _lastName, DateTime? _birthDate, Platform _platform, EmailAddress _provider)
        {
            Name = _name??Name;
            LastName = _lastName??LastName;
            BirthDate = _birthDate ?? BirthDate;
            Username = _username??Username;
            Password = _password??Password;
            EmailAddress = _provider ?? EmailAddress;
            Platform = _platform ?? Platform;
            Phone = _phone??Phone;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _username, string _password, string _phone, string _lastName, DateTime _birthDate, Platform _platform, EmailAddress _provider)
        {
            Name = _name;
            LastName = _lastName;
            BirthDate = _birthDate;
            Username = _username;
            Password = _password;
            Platform = _platform;
            EmailAddress = _provider;
            Phone = _phone;
            CreateDate = DateTime.Now;
        }
    }
}
