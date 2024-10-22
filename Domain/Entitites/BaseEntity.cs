using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class BaseEntity
    {
        public DateTime CreateDate { get; protected set; }
        public DateTime UpdateDate { get; protected set; }
        public bool Deleted { get; set; }
        public int Id { get; set; }
    }
}
