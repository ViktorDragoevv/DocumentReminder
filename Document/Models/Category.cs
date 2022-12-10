namespace Document.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string categoryName { get; set; }

        public Category(int ID, string categoryName) {this.ID = ID; this.categoryName = categoryName; }
        public Category() { }
    }


public static class CategoryEndpoints
{



	public static void MapCategoryEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Category");

            /*config.EnableCors();
            [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]*/
            group.MapGet("/", () =>
        {
            return new [] { new Category(1,"af"), new Category(23, "af") };
        })
        .WithName("GetAllCategorys");
        
        group.MapGet("/{id}", (int id) =>
        {
            //return new Category { ID = id };
        })
        .WithName("GetCategoryById");

        group.MapPut("/{id}", (int id, Category input) =>
        {
            return TypedResults.NoContent();
        })
        .WithName("UpdateCategory");

        group.MapPost("/", (Category model) =>
        {
            //return Results.Created($"/Categorys/{model.ID}", model);
        })
        .WithName("CreateCategory");

        group.MapDelete("/{id}", (int id) =>
        {
            //return Results.Ok(new Category { ID = id });
        })
        .WithName("DeleteCategory");  
    }
}}
