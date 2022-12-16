using Document.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            //Properties
            builder.Property(x => x.Comments).HasMaxLength(200);
            //Navigations

            builder.HasOne(x => x.CategoryModel).WithMany(x => x.Documents).HasForeignKey(x => x.CategoryID);
            builder.HasOne(x => x.ContactsModel).WithMany(x => x.Documents).HasForeignKey(x => x.ContactID);
            builder.HasOne(x => x.LocationModel).WithMany(x => x.Documents).HasForeignKey(x => x.LocationID);
            builder.HasOne(x => x.CompanyModel).WithMany(x => x.Documents).HasForeignKey(x => x.CompanyID);
        }
    }
}
