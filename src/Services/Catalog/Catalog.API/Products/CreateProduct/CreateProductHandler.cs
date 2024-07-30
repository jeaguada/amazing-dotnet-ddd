using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, decimal Price, string PictureFileName, List<string> Categories) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                PictureFileName = request.PictureFileName,
                Categories = request.Categories
            };

            return new Task<CreateProductResult>(() => new CreateProductResult(Guid.NewGuid()));
        }
    }
}
