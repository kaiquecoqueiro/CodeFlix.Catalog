using DomainEntity = CodeFlix.Catalog.Domain.Entity;

namespace CodeFlix.Catalog.Application.UseCases.Category.Common
{
    public class CategoryModelOutput
    {
        public CategoryModelOutput(Guid id, string name, DateTime createAt, string description, bool isActive)
        {
            Id = id;
            Name = name;
            CreatedAt = createAt;
            Description = description;
            IsActive = isActive;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public static CategoryModelOutput FromCategory(DomainEntity.Category category)
            => new CategoryModelOutput(category.Id,
                                        category.Name,
                                        category.CreatedAt,
                                        category.Description,
                                        category.IsActive);
    }
}