﻿using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Provider : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Signature { get; private set; }
        public string Image { get; private set; }
        public void Create(string _name, string _desc, string _signature, string _image)
        {
            Name = _name;
            Description = _desc;
            Image = _image;
            Signature = _signature;
        }
        public void Update(string _name, string _desc, string signature, string _image)
        {
            Name = _name??Name;
            Description = _desc??Description;
            Signature = signature??Signature;
            Image = _image;
        }
    }
}
