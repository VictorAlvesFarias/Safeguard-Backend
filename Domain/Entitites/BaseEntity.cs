using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class BaseEntity
    {
        public DateTime DataCreate { get; private set; }
        public bool Deleted { get; private set; }
        public int Id { get; set; }
    }
}
