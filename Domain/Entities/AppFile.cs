using Domain.Entitites;
using System.Buffers.Text;
using System.Xml.Linq;

namespace Domain.Entitites
{
    public class AppFile : BaseEntityUserRelation
    {
        public StoredFile StoredFile { get; set; }
        public int StoredFileId  { get; set; }
        public void Create(StoredFile storedFile) { 
            StoredFile = storedFile;
        }
        public void Update(StoredFile storedFile) {
            StoredFile = storedFile ?? StoredFile;

        }
    }
}
