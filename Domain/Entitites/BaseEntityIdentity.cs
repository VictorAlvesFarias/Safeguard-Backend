using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class BaseEntityIdentity:IdentityUser
    {
        public DateTime DataCreate { get;  set; }
        public bool Deleted { get;  set; }
    }
}
