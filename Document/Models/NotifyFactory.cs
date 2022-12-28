namespace Document.Models
{
    public static class NotifyFactory
    {
        public static ViewNotify ToModel(this NotifyModel notifyModel) =>
    notifyModel == null
        ? null
        : new ViewNotify
        {
            ID = notifyModel.ID,
            Days = notifyModel.Days,
            Send = notifyModel.Send,
            ContactModel = notifyModel.ContactModel.ToModel(),
        };

        public static NotifyModel ToEntity(this CreateUpdateNotify notifyModel, Guid notifyID)
        {
            return notifyModel == null
                ? null
                : new NotifyModel
                {
                    ID = notifyID,
                    Days = notifyModel.Days,
                    Send = notifyModel.Send,
                    ContactID = notifyModel.ContactID,
                    DocumentID = notifyModel.DocumentID,
                };
        }
    }
}
