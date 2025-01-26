using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Email : BaseEntityUserRelation
    {
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public void Update(string _name, string _username, string _password, string _phone, Provider _provider, string _lastName, DateTime? _birthDate)
        {
            Name = _name??Name;
            LastName = _lastName??LastName;
            BirthDate = _birthDate ?? BirthDate;
            Username = _username??Username;
            Password = _password??Password;
            Provider = _provider??Provider;
            Phone = _phone??Phone;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _username, string _password, string _phone, Provider _provider, string _lastName, DateTime _birthDate)
        {
            Name = _name;
            LastName = _lastName;
            BirthDate = _birthDate;
            Username = _username;
            Password = _password;
            Provider = _provider;
            Phone = _phone;
            CreateDate = DateTime.Now;
        }
    }
}
