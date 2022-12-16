using Document.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Configurations
{
    public class NotifyConfiguration : IEntityTypeConfiguration<NotifyModel>
    {
        public void Configure(EntityTypeBuilder<NotifyModel> builder)
        {
            //Properties
            builder.Property(x => x.Send).HasDefaultValue(false);

            //Navigations

            builder.HasOne(x => x.DocumentModel).WithMany(x => x.NotifyModels).HasForeignKey(x => x.DocumentID);
            builder.HasOne(x => x.ContactModel).WithMany(x => x.NotifyModel).HasForeignKey(x => x.ContactID);
        }
    }
}
