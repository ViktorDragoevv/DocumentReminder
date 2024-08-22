using AutoFixture;
using Document.Data;
using Document.Models;
using Document.Repositories;
using Document.Services;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Document.Test.Service
{
    public class ContactServiceTests
    {
        private readonly Fixture _fixture;
        private readonly AutoMocker _autoMocker;
        private readonly IContactService _contactService;

        public ContactServiceTests()
        {
            _fixture = new Fixture();
            _autoMocker = new AutoMocker(Moq.MockBehavior.Strict);
            _contactService = _autoMocker.CreateInstance<ContactService>();
        }

        [Fact]
        public async Task ContactService_CreateContact_ShouldCreateContact()
        {
            //Ararange.
            var contactID = _fixture.Create<Guid>();
            var newContact = _fixture.Build<ContactsModel>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Without(_ => _.LocationModel)
                .Without(_ => _.Documents)
                .Without(_ => _.NotifyModel)
                .Create();

            var createUpdateContact = new CreateUpdateContact
            {
                FirstName = newContact.FirstName,
                LastName = newContact.LastName

            };

            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.AddAsync(It.IsAny<ContactsModel>()))
                .ReturnsAsync(newContact);

            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.GetContactWithLocationByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(newContact);

            //Act

            var result = await _contactService.CreateContact(createUpdateContact);

            //Assert
            result.FirstName.Should().BeEquivalentTo(newContact.FirstName);
            result.LastName.Should().BeEquivalentTo(newContact.LastName);

        }
        [Fact]
        public async Task ContactService_GetAllContacts_ShouldReturnAllContacts()
        {
            //Ararange.
            var newContact = _fixture.Build<ContactsModel>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Without(_ => _.LocationModel)
                .Without(_ => _.Documents)
                .Without(_ => _.NotifyModel)
                .CreateMany(6);

            var contactsList = newContact.Select(x => x.ToModel());

            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.GetAllContactsWithLocation())
                .ReturnsAsync(newContact);

            //Act

            var result = await _contactService.GetAllContacts();

            //Assert
            result.Should().BeEquivalentTo(contactsList);

        }
        [Fact]
        public async Task ContactService_DeleteContact_ShouldDeleteContact()
        {
            //Ararange.
            var newContact = _fixture.Build<ContactsModel>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Without(_ => _.LocationModel)
                .Without(_ => _.Documents)
                .Without(_ => _.NotifyModel)
                .Create();


            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.RemoveAsync(It.IsAny<Guid>()))
                .ReturnsAsync(newContact);

            //Act

            var result = await _contactService.DeleteContact(newContact.ID);

            //Assert
            result.ID.Should().Be(newContact.ID);

        }
        [Fact]
        public async Task ContactService_UpdateContactByID_ShouldUpdateContact()
        {
            //Ararange.
            var newContact = _fixture.Build<ContactsModel>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Without(_ => _.LocationModel)
                .Without(_ => _.Documents)
                .Without(_ => _.NotifyModel)
                .Create();
            var newContactView = _fixture.Build<CreateUpdateContact>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Create();


            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(newContact);

            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.UpdateAsync(newContact))
                .ReturnsAsync(newContact);

            newContact.FirstName = newContactView.FirstName;
            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.GetContactWithLocationByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(newContact);

            var contactView = newContact.ToModel();

            //Act
            var result = await _contactService.UpdateContactByID(newContactView, newContact.ID);

            //Assert
            result.FirstName.Should().Be(newContact.FirstName);

        }
        [Fact]
        public async Task ContactService_GetContactByID_ShouldReturnContactByID()
        {
            //Ararange.
            var contactID = _fixture.Create<Guid>();
            var newContact = _fixture.Build<ContactsModel>()
                .With(_ => _.FirstName, _fixture.Create<string>)
                .Without(_ => _.LocationModel)
                .Without(_ => _.Documents)
                .Without(_ => _.NotifyModel)
                .Create();


            _autoMocker.GetMock<IContactRepository>()
                .Setup(_ => _.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(newContact);

            //Act
            var result = await _contactService.GetContactByID(contactID);

            //Assert
            result.Should().Be(newContact);

        }

    }
}
