using CodeFlix.Catalog.Application.Exceptions;
using CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;
using FluentAssertions;
using Moq;
using UseCases = CodeFlix.Catalog.Application.UseCases.Category.DeleteCategory;

namespace CodeFlix.Catalog.UnitTests.Application.DeleteCategory
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async void DeleteCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var exampleCategory = _fixture.GetValidCategory();

            repositoryMock.Setup(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(exampleCategory);

            var useCase = new UseCases.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var input = new DeleteCategoryInput(exampleCategory.Id);
            await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(x => x.Delete(exampleCategory, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = nameof(ThrowWhenCategoryNotFound))]
        [Trait("Application", "DeleteCategory - Use Cases")]
        public async void ThrowWhenCategoryNotFound()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var exampleCategory = _fixture.GetValidCategory();

            var exceptionMessage = "Category not found";
            repositoryMock.Setup(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>()))
                          .ThrowsAsync(new NotFoundException(exceptionMessage));

            var useCase = new UseCases.DeleteCategory(repositoryMock.Object, unitOfWorkMock.Object);

            var input = new DeleteCategoryInput(exampleCategory.Id);
            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);
            await task.Should().ThrowAsync<NotFoundException>()
                .WithMessage(exceptionMessage);

            repositoryMock.Verify(x => x.Get(exampleCategory.Id, It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(x => x.Delete(exampleCategory, It.IsAny<CancellationToken>()), Times.Never);
            unitOfWorkMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}