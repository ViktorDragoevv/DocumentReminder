namespace Document.Models
{
    public static class ContactFactory
    {
        public static ContactsModel ToEntity(this CreateUpdateContact contactModel, Guid contactID)
        {
            return contactModel == null
                ? null
                : new ContactsModel
                {
                    ID = contactID,
                    FirstName = contactModel.FirstName,
                    LastName = contactModel.LastName,
                    Email = contactModel.Email,
                    PhoneNumber = contactModel.PhoneNumber,
                    LocationID = contactModel.LocationID,
                };
        }


        public static ViewContact ToModel(this ContactsModel contactsModel) =>
            contactsModel == null
                ? null
                : new ViewContact
                {
                    ID = contactsModel.ID,
                    FirstName = contactsModel.FirstName,
                    LastName = contactsModel.LastName,
                    Email = contactsModel.Email,
                    PhoneNumber = contactsModel.PhoneNumber,
                    ViewLocation = contactsModel.LocationModel.ToModel(),
                };

    }
}
