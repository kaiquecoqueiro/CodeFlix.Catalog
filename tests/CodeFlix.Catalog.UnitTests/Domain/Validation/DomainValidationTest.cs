using Bogus;
using CodeFlix.Catalog.Domain.Exceptions;
using CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace CodeFlix.Catalog.UnitTests.Domain.Validation
{
    public class DomainValidationTest
    {
        private Faker Faker { get; set; } = new Faker();

        [Fact(DisplayName = nameof(NotNullOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOk()
        {
            var value = Faker.Commerce.ProductName();
            Action act = () => DomainValidation.NotNull(value, "Value");
            act.Should().NotThrow();
        }

        [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullThrowWhenNull()
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.NotNull(null, fieldName);
            act.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be null");
        }

        [Theory(DisplayName = nameof(NotNullOrEmptyThrowWhenEmpty))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NotNullOrEmptyThrowWhenEmpty(string? target)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.NotNullOrEmpty(target, fieldName);
            act.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should not be empty or null");
        }

        [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        public void NotNullOrEmptyOk()
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.NotNullOrEmpty("value", fieldName);
            act.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abc", 4)]
        [InlineData("abds", 8)]
        [InlineData("abcdefr", 10)]
        public void MinLengthThrowWhenLess(string target, int lenght)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.MinLength(target, lenght, fieldName);
            act.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should have at least {lenght} characters");
        }

        [Theory(DisplayName = nameof(MinLengthOk))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abc", 3)]
        [InlineData("abds", 4)]
        [InlineData("abcdefr", 7)]
        public void MinLengthOk(string target, int lenght)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.MinLength(target, lenght, fieldName);
            act.Should().NotThrow();
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abc", 2)]
        [InlineData("abds", 3)]
        [InlineData("abcdefr", 5)]
        public void MaxLengthThrowWhenGreater(string target, int lenght)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.MaxLength(target, lenght, fieldName);
            act.Should().Throw<EntityValidationException>().WithMessage($"{fieldName} should have less than {lenght} characters");
        }

        [Theory(DisplayName = nameof(MaxLengthThrowWhenGreater))]
        [Trait("Domain", "DomainValidation - Validation")]
        [InlineData("abc", 3)]
        [InlineData("abds", 4)]
        [InlineData("abcdefr", 7)]
        [InlineData("abcssfrr", 10)]
        public void MaxLengthOk(string target, int lenght)
        {
            string fieldName = Faker.Commerce.ProductName().Replace(" ", "");

            Action act = () => DomainValidation.MaxLength(target, lenght, fieldName);
            act.Should().NotThrow();
        }
    }
}