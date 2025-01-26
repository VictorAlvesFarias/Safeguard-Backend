using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class RecoveryKey: BaseEntityUserRelation
    {
        public string Key { get; set; }
        public string ReferenceId { get; set; }
        public string ReferenceType { get; set; }

        public void Create(string type, string key,string reference)
        {
            Key = key;
            ReferenceType = type;
            ReferenceId = reference;
        }
    }
}
