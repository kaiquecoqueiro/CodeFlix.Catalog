namespace CodeFlix.Catalog.Application.UseCases.Category.GetCategory;
using DomainEntity = Domain.Entity;

public class GetCategoryOutput
{
    public GetCategoryOutput(Guid id, string name, DateTime createdAt, string description, bool isActive)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        Description = description;
        IsActive = isActive;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public static GetCategoryOutput FromCategory(DomainEntity.Category category)
        => new GetCategoryOutput(category.Id,
                                    category.Name,
                                    category.CreatedAt,
                                    category.Description,
                                    category.IsActive);
}