namespace Document.Models
{
    public static class DocumentFactory
    {
        public static ViewDocument ToModel(this DocumentModel documentModel) =>
            documentModel == null
                ? null
                : new ViewDocument
                {
                    ID = documentModel.ID,
                    ViewCategory = documentModel.CategoryModel.ToModel(),
                    Name = documentModel.Name,
                    Status = documentModel.Status,
                    Files = documentModel.Files.Select(x => x.ToModel()),
                    ViewContact = documentModel.ContactsModel.ToModel(),
                    ViewLocation = documentModel.LocationModel.ToModel(),
                    ViewCompany = documentModel.CompanyModel.ToModel(),
                    Comments = documentModel.Comments,
                    ExpirationDate = documentModel.ExpirationDate,
                    CreatedDate = documentModel.CreatedDate,
                    Notify = documentModel.NotifyModels.Select(x => x.ToModel()),

                };
    }
}
