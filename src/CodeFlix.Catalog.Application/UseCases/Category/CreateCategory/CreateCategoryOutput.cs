namespace CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using DomainEntity = Domain.Entity;

public class CreateCategoryOutput
{
    public CreateCategoryOutput(Guid id, string name, DateTime createAt, string description, bool isActive)
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

    public static CreateCategoryOutput FromCategory(DomainEntity.Category category)
        => new CreateCategoryOutput(category.Id,
                                    category.Name,
                                    category.CreatedAt,
                                    category.Description,
                                    category.IsActive);
}