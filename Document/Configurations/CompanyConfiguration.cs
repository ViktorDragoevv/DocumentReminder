using Document.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Document.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<CompanyModel>
    {
        public void Configure(EntityTypeBuilder<CompanyModel> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50);
            builder.Property(x => x.TradeName).HasMaxLength(50);
            builder.Property(x => x.Phone).HasMaxLength(50);
            builder.Property(x => x.Type).HasMaxLength(50);

            //Navigations

            builder.HasOne(x => x.LocationModel).WithMany(x => x.Company).HasForeignKey(x => x.LocationID);
        }
    }
}
