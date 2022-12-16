using Document.Configurations;
using Document.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection.Emit;

namespace Document.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions)
            : base(options, operationalStoreOptions)
        {

        }
        public DbSet<Document.Models.CategoryModel> CategoryModel { get; set; } = default!;
        public DbSet<Document.Models.ContactsModel> ContactsModel { get; set; } = default!;
        public DbSet<Document.Models.CompanyModel> CompanyModel { get; set; } = default!;
        public DbSet<Document.Models.LocationModel> LocationModel { get; set; } = default!;
        public DbSet<Document.Models.NotifyModel> NotifyModels { get; set; } = default!;
        public DbSet<Document.Models.Files> Files { get; set; } = default!;
        public DbSet<Document.Models.DocumentModel> DocumentModels { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            builder.ApplyConfiguration(new ContactConfiguration());
            builder.ApplyConfiguration(new LocationConfiguration());
            builder.ApplyConfiguration(new CompanyConfiguration());
            builder.ApplyConfiguration(new NotifyConfiguration());
            builder.ApplyConfiguration(new FilesConfiguration());
            builder.ApplyConfiguration(new DocumentConfiguration());
        }

        
    }


}