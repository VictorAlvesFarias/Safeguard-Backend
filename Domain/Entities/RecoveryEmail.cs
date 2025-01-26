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
        public string ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public int EmailId { get; set; }

        public void Create(string type, string reference, int emailId)
        {
            ReferenceType = type;
            ReferenceId = reference;
            EmailId = emailId;
        }
    }
}
