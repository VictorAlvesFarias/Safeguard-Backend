
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Account:BaseEntity
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public Email Email { get; set; }
        public string Image { get; set; }
        public void Create(string _name, string _username, string _password, string _phone, Email _email, string _image)
        {
            Name = _name;
            Username = _username;
            Password = _password;
            Email = _email;
            Phone = _phone;
        }
        public void Update(string _name, string _username, string _password, string? _phone, Email _email, string _image)
        {
            Name = _name ?? Name;
            Username = _username ?? Username;
            Password = _password ?? Password;
            Email = _email?? Email;
            Phone = _phone ?? Phone;
            Image = _image ?? Image;
        }
    }
}
