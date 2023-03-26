namespace CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;

public class CreateCategoryOutput
{
    public CreateCategoryOutput(Guid id, string name, DateTime createAt, string description, bool isActive)
    {
        Id = id;
        Name = name;
        CreateAt = createAt;
        Description = description;
        IsActive = isActive;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreateAt { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}