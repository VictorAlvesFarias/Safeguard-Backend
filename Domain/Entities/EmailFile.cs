using Domain.Entitites;
using System.Buffers.Text;
using System.Xml.Linq;

namespace Domain.Entitites
{
    public class EmailFile : BaseEntityUserRelation
    {
        public StoredFile StoredFile { get; set; }
        public int StoredFileId  { get; set; }
        public Email Email { get; set; }
        public int EmailId { get; set; }

        public void Create(StoredFile storedFile,Email email) { 
            StoredFile = storedFile;
            Email = email;
        }
    }
}
