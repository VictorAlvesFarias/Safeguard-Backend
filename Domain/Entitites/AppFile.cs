﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class AppFile: BaseEntity
    {
        public string Name { get; private set; }
        public string MimeType { get; private set; }
        public string Base64 { get; private set; }
        public void Update(string _name, string _mimeType, string _base64)
        {
            Name = _name??Name;
            MimeType = _mimeType??MimeType;
            Base64 = _base64??Base64;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _mymeType, string _base64)
        {
            Name = _name;
            MimeType = _mymeType;
            Base64 = _base64;
            CreateDate = DateTime.Now;
        }
    }
}