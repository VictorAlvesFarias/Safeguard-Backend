
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Platform:BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public void Create(string _name, string _image)
        {
            Name = _name;
            Image = _image;
        }
        public void Update(string _name, string _image)
        {
            Name = _name ?? Name;
            Image = _image ?? Image;
        }
    }
}
