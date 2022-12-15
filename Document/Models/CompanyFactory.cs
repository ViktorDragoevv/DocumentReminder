namespace Document.Models
{
    public static class CompanyFactory
    {

        public static ViewCompany ToModel(this CompanyModel companyModel) =>
            companyModel == null
                ? null
                : new ViewCompany
                {
                    ID = companyModel.ID,
                    Name = companyModel.Name,
                    TradeName = companyModel.TradeName,
                    Phone = companyModel.Phone,
                    Type = companyModel.Type,
                    ViewLocation = companyModel.LocationModel.ToModel(),
                };


        public static CompanyModel ToEntity(this CreateUpdateCompany createUpdateCompany, Guid companyID)
        {
            return createUpdateCompany == null
                ? null
                : new CompanyModel
                {
                    ID = companyID,
                    Name = createUpdateCompany.Name,
                    TradeName = createUpdateCompany.TradeName,
                    Phone = createUpdateCompany.Phone,
                    Type = createUpdateCompany.Type,
                    LocationID = createUpdateCompany.LocationID,
                };
        }

    }

}
