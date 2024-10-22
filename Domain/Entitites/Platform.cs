
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Platform:BaseEntity
    {
        public string Name { get; private set; }
        public AppFile Image { get; private set; }
        public int ImageId { get; private set; }

        public void Update(string _name, AppFile _image)
        {
            Name = _name ?? Name;
            Image = _image ?? Image;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, AppFile _image)
        {
            Name = _name;
            Image = _image;
            CreateDate = DateTime.Now;
        }
    }
}
