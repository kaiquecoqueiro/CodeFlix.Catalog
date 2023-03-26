using CodeFlix.Catalog.UnitTests.Common;
using DomainEntity = CodeFlix.Catalog.Domain.Entity;
namespace CodeFlix.Catalog.UnitTests.Domain.Entity.Category;

public class CategoryTestFixture : BaseFixture
{
    public CategoryTestFixture()
        : base() { }

    public string GetValidCategoryName()
    {
        var categoryName = string.Empty;

        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategorDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();

        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..255];

        return categoryDescription;
    }

    public DomainEntity.Category GetValidCategory() => new(GetValidCategoryName(), GetValidCategorDescription());
}

[CollectionDefinition(nameof(CategoryTestFixture))]
public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }