using CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;
using CodeFlix.Catalog.Domain.Entity;
using CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = CodeFlix.Catalog.Application.UseCases.Category.CreateCategory;

namespace CodeFlix.Catalog.UnitTests.Application.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture) => _fixture = fixture;


    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

        var input = _fixture.GetInput();
        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().NotBeEmpty();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.CreateAt.Should().NotBeSameDateAs(default);
        output.IsActive.Should().Be(input.IsActive);

        repositoryMock.Verify(x => x.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstatiateAggregate))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(nameof(GetInvalidInputs))]
    public async void ThrowWhenCantInstatiateAggregate(CreateCategoryInput input, string exceptionMessage)
    {
        var useCase = new UseCases.CreateCategory(_fixture.GetRepositoryMock().Object,
                                                  _fixture.GetUnitOfWorkMock().Object);

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMessage);
    }

    private static IEnumerable<object[]> GetInvalidInputs()
    {
        var fixture = new CreateCategoryTestFixture();

        var invalidInputs = new List<object[]>();

        var inputShortName = fixture.GetInput();
        inputShortName.Name = inputShortName.Name[..2];
        invalidInputs.Add(new object[] { inputShortName, "Name should have at least 3 characters" });


        string tooLongName = fixture.Faker.Commerce.ProductName();
        while (tooLongName.Length <= 255)
            tooLongName = $"{tooLongName} {fixture.Faker.Commerce.ProductName}";

        var inputLongName = fixture.GetInput();
        inputLongName.Name = tooLongName;
        invalidInputs.Add(new object[] { inputLongName, "Name should have less than 255 characters" });

        return invalidInputs;
    }
}