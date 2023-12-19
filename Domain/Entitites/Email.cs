using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Email:BaseEntity
    {
        public Provider Provider { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public int Phone { get; set; }
        public string Password { get; set; }
        public void Create(string _name, string _username, string _password,int _phone,Provider _provider)
        {
            Name = _name;
            Username = _username;
            Password = _password;
            Provider = _provider;
            Phone = _phone;
        }
        public void Update(string _name, string _username, string _password, int? _phone, Provider _provider)
        {
            Name = _name??Name;
            Username = _username??Username;
            Password = _password??Password;
            Provider = _provider??Provider;
            Phone = _phone??Phone;
        }
    }
}
