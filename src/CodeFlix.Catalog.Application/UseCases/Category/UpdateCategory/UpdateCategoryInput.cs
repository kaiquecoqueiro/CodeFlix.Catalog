using CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory
{
    public class UpdateCategoryInput : IRequest<CategoryModelOutput>
    {
        public UpdateCategoryInput(Guid id, string name, string? description = null, bool isActive = true)
        {
            Name = name;
            Description = description ?? "";
            IsActive = isActive;
            Id = id;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}