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
        public Provider Provider { get; private set; }
        public int ProviderId { get; private set; }
        public string Name { get; private set; }
        public string Username { get; private set; }
        public string Phone { get; private set; }
        public string Password { get; private set; }
        public void Update(string _name, string _username, string _password, string _phone, Provider _provider)
        {
            Name = _name??Name;
            Username = _username??Username;
            Password = _password??Password;
            Provider = _provider??Provider;
            Phone = _phone??Phone;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _username, string _password, string _phone, Provider _provider)
        {
            Name = _name;
            Username = _username;
            Password = _password;
            Provider = _provider;
            Phone = _phone;
            CreateDate = DateTime.Now;
        }
    }
}
