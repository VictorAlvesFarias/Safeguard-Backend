
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entitites
{
    public class EmailAddress: BaseEntityUserRelation
    {
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public string Username { get; set; }
        public void Update(string _username, Provider _provider)
        {
            Username = _username ?? Username;
            Provider = _provider ?? Provider;
            UpdateDate = DateTime.Now;
        }
        public void Create(string _username, Provider _provider)
        {
            Username = _username;
            Provider = _provider;
            CreateDate = DateTime.Now;
        }
    }
}
