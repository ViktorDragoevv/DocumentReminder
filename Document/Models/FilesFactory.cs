namespace Document.Models
{
    public static class FilesFactory
    {
        public static ViewFiles ToModel(this Files filesModel) =>
    filesModel == null
        ? null
        : new ViewFiles
        {
            ID = filesModel.ID,
            ImageName = filesModel.ImageName,
            ImagePath = filesModel.ImagePath
        };
    }
}
