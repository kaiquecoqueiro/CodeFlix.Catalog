namespace CodeFlix.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryTestDataGenerator
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputs = new List<object[]>();

            var totalInvalidCases = 4;
            for (int i = 0; i < times; i++)
            {
                switch (i % totalInvalidCases)
                {
                    case 0:
                        invalidInputs.Add(new object[] { fixture.GetInvalidInputShortName(), "Name should have at least 3 characters" });
                        break;
                    case 1:
                        invalidInputs.Add(new object[] { fixture.GetInvalidInputTooLongName(), "Name should have less than 255 characters" });
                        break;
                    case 2:
                        invalidInputs.Add(new object[] { fixture.GetInvalidInputNullDescription(), "Description should not be null" });
                        break;
                    case 3:
                        invalidInputs.Add(new object[] { fixture.GetInvalidInputTooLongDescription(), "Description should have less than 10000 characters" });
                        break;
                    default:
                        break;
                }
            }



            return invalidInputs;
        }
    }
}