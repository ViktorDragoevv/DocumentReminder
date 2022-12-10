using Document.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<ContactsModel>
    {
        public void Configure(EntityTypeBuilder<ContactsModel> builder)
        {
            //Properties
            builder.Property(x => x.FirstName).HasMaxLength(50);
            builder.Property(x => x.LastName).HasMaxLength(50);

            //Navigations

            builder.HasOne(x => x.LocationModel).WithMany(x => x.Contacts).HasForeignKey(x => x.LocationID);
        }
    }
}
