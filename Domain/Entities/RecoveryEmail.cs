using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class RecoveryEmail: BaseEntityUserRelation
    {
        public int ParentEmailId { get; set; }
        public int EmailId { get; set; }

        public void Create(int parentEmail, int emailId)
        {
            ParentEmailId = parentEmail;
            EmailId = emailId;
        }
    }
}
