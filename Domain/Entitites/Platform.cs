
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class Platform : BaseEntityUserRelation
    {
        public string Name { get; set; }
        public AppFile Image { get; set; }
        public int ImageId { get; set; }

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
