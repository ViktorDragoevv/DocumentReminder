using Document.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Configurations
{
    public class FilesConfiguration : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            //Properties
            //builder.Property(x => x.Content).HasMaxLength(int.MaxValue);

            //Navigations

            builder.HasOne(x => x.DocumentModel).WithMany(x => x.Files).HasForeignKey(x => x.DocumentID);
        }
    }
}
