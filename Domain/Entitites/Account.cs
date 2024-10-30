﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Account: BaseEntityUserRelation
    {
        public string Password { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Phone { get; set; }
        public Email Email { get; set; }
        public int EmailId{ get; set; }
        public Platform Platform { get; set; }
        public int PlatformId { get; set; }
        public void Update(string _name, string _username, string _password, string? _phone, Email _email, Platform _platform)
        {
            Name = _name ?? Name;
            Username = _username ?? Username;
            Password = _password ?? Password;
            Email = _email?? Email;
            Phone = _phone ?? Phone;
            Platform = _platform??Platform;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _username, string _password, string? _phone, Email _email, Platform _platform)
        {
            Name = _name ;
            Username = _username;
            Password = _password;
            Email = _email;
            Phone = _phone;
            Platform = _platform;
            CreateDate = DateTime.Now;
        }
    }
}
