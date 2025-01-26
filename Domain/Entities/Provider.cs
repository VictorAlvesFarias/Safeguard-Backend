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
    public class Provider : BaseEntityUserRelation
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Signature { get; set; }
        public AppFile Image { get; set; }
        public int ImageId { get; set; }
        public void Update(string _name, string _desc, string signature, int _imageId = 0)
        {
            Name = _name??Name;
            Description = _desc??Description;
            Signature = signature??Signature;
            ImageId = _imageId == 0 ? ImageId : _imageId;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, string _desc, string signature, int _image = 0)
        {
            Name = _name;
            Description = _desc;
            Signature = signature;
            ImageId = _image;
            UpdateDate = DateTime.Now;
        }
    }
}
