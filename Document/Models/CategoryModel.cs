using Document.Repositories;
using System.ComponentModel.DataAnnotations;

namespace Document.Models
{
    public class CategoryModel : IEntity
    {
        [Key]
            public Guid ID { get; set; }
            public string CategoryName { get; set; }

    }
}
