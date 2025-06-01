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
        public string EmailId { get; set; }

        public void Create(string emailId, string key)
        {
            Key = key;
            EmailId= emailId;
        }
    }
}
