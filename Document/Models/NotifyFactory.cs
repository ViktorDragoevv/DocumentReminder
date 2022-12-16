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
        };
    }
}
