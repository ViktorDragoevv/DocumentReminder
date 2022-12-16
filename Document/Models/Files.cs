using Document.Repositories;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class Files : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public Guid? DocumentID { get; set; }
        public virtual DocumentModel? DocumentModel { get; set; }
    }
}
