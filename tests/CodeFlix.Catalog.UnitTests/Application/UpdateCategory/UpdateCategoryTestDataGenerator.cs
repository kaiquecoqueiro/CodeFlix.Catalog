using CodeFlix.Catalog.Application.UseCases.Category.UpdateCategory;

namespace CodeFlix.Catalog.UnitTests.Application.UpdateCategory
{
    public class UpdateCategoryTestDataGenerator
    {
        public static IEnumerable<object[]> GetCategoriesToUpdate(int times = 10)
        {
            var fixture = new UpdateCategoryTestFixture();
            for (int i = 0; i < times; i++)
            {
                var exampleCategory = fixture.GetValidCategory();
                var input = new UpdateCategoryInput(exampleCategory.Id,
                fixture.GetValidCategoryName(),
                fixture.GetValidCategorDescription(),
                fixture.GetRandomBoolean());
                yield return new object[] { exampleCategory, input };
            }
        }
    }
}