
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

        public void Update(string _name, int _imageId = 0)
        {
            Name = _name ?? Name;
            ImageId = _imageId == 0 ? ImageId: _imageId;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _name, int _imageId = 0)
        {
            Name = _name;
            ImageId = _imageId == 0 ? ImageId : _imageId;
            CreateDate = DateTime.Now;
        }
    }
}
