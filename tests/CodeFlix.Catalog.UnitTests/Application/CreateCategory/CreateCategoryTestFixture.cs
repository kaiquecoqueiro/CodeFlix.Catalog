using CodeFlix.Catalog.Application.Interfaces;
using CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using CodeFlix.Catalog.Domain.Repository;
using CodeFlix.Catalog.UnitTests.Common;
using Moq;

namespace CodeFlix.Catalog.UnitTests.Application.CreateCategory;

public class CreateCategoryTestFixture : BaseFixture
{
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

    public bool GetRandomBoolean() => (new Random()).NextDouble() < 0.5;

    public CreateCategoryInput GetInput()
        => new(GetValidCategoryName(), GetValidCategorDescription(), GetRandomBoolean());

    public CreateCategoryInput GetInvalidInputShortName()
    {
        var inputInvalidShortName = GetInput();
        inputInvalidShortName.Name = inputInvalidShortName.Name[..2];
        return inputInvalidShortName;
    }

    public CreateCategoryInput GetInvalidInputTooLongName()
    {
        string tooLongName = Faker.Commerce.ProductName();

        while (tooLongName.Length <= 255)
            tooLongName = $"{tooLongName} {Faker.Commerce.ProductName}";

        var inputInvalidLongName = GetInput();
        inputInvalidLongName.Name = tooLongName;

        return inputInvalidLongName;
    }

    public CreateCategoryInput GetInvalidInputTooLongDescription()
    {
        string tooLongDescription = Faker.Commerce.ProductDescription();

        while (tooLongDescription.Length <= 10000)
            tooLongDescription = $"{tooLongDescription} {Faker.Commerce.ProductDescription()}";

        var inputInvalidLongDescription = GetInput();
        inputInvalidLongDescription.Description = tooLongDescription;

        return inputInvalidLongDescription;
    }

    public CreateCategoryInput GetInvalidInputNullDescription()
    {
        var invalidNullDescription = GetInput();
        invalidNullDescription.Description = null;
        return invalidNullDescription;
    }

    public Mock<ICategoryRepository> GetRepositoryMock() => new();
    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
}

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }