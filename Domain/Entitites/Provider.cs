using Microsoft.AspNetCore.Components.Web.Virtualization;
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
        public string Name { get; set; }
        public string Description { get; set; }
        public string Signature { get; set; }
        public AppFile Image { get; set; }
        public int ImageId { get; set; }

        public void Update(string _name, string _desc, string signature, AppFile _image)
        {
            Name = _name??Name;
            Description = _desc??Description;
            Signature = signature??Signature;
            Image = _image??Image;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _desc, string signature, AppFile _image)
        {
            Name = _name;
            Description = _desc;
            Signature = signature;
            Image = _image;
            UpdateDate = DateTime.Now;
            ImageId = _image.Id;
        }
    }
}
