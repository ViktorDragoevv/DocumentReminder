using Document.Repositories;
using NuGet.Packaging.Signing;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class Files : IEntity
    {
        [Key]
        public Guid ID { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public Guid? DocumentID { get; set; }
        public virtual DocumentModel? DocumentModel { get; set; }
    }
}
