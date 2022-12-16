namespace Document.Models
{
    public static class CategoryFactory
    {
        public static ViewCategory ToModel(this CategoryModel categoryModel) =>
    categoryModel == null
        ? null
        : new ViewCategory
        {
            ID = categoryModel.ID,
            CategoryName = categoryModel.CategoryName,
        };
    }
}
