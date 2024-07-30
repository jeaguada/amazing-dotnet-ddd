using BuildingBlocks.CQRS;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, decimal Price, string PictureFileName, List<string> Categories) : ICommand<CreateProductResult>;
    public  record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest req, ISender sender) =>
            {
                var command = req.Adapt<CreateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateProductResponse>();
                return Results.Created($"/products/{response.Id}", response);
            })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(201)
                .ProducesProblem(400)
                .WithSummary("Creates a new product")
                .WithDescription("Creates a new product in the catalog");
        }
    }
}
