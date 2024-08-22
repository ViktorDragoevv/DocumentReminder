using AutoFixture;
using Document.Data;
using Document.Models;
using Document.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using NuGet.Protocol.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;



namespace Document.Test.Repository
{
    public class ContactRepositoryTests : IDisposable
    {
        private readonly Fixture _fixture;
        private readonly ApplicationDbContext _dbContext;

        public ContactRepositoryTests()
        {
            _fixture = new Fixture();
        }

        /*public ApplicationDbContext AddMemory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("ApplicationDbContext");
            var dbContext = new ApplicationDbContext(optionsBuilder.Options, this.currentUserService);
            dbContext.Database.EnsureDeleted();
            return dbContext;

            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseInMemoryDataBase
        }*/

        [Fact]
        public async Task ContactRepository_CreateContact_ShouldCreateContact()
        {
           /* // Arrange
            var testObject = new ContactsModel();

            var context = new Mock<ApplicationDbContext>();
            var dbSetMock = new Mock<DbSet<ContactsModel>>();
            context.Setup(x => x.Set<ContactsModel>()).Returns(dbSetMock.Object);
            dbSetMock.Setup(x => x.Add(It.IsAny<ContactsModel>())).Returns(testObject);

            // Act
            var repository = new Repository<ContactsModel>(context.Object);
            repository.Add(testObject);

            //Assert
            context.Verify(x => x.Set<ContactsModel>());
            dbSetMock.Verify(x => x.Add(It.Is<ContactsModel>(y => y == testObject)));*/
        }


            public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
