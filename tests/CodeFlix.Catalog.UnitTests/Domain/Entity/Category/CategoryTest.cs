using CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = CodeFlix.Catalog.Domain.Entity;

namespace CodeFlix.Catalog.UnitTests.Domain.Entity.Category
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        private readonly CategoryTestFixture _categoryTestFixture;

        public CategoryTest(CategoryTestFixture categoryTestFixture)
            => _categoryTestFixture = categoryTestFixture;


        [Fact(DisplayName = nameof(Instatiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instatiate()
        {
            var dateTimeBefore = DateTime.Now;
            var validCategory = _categoryTestFixture.GetValidCategory();

            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description);

            subject.Should().NotBeNull();
            subject.Name.Should().Be(validCategory.Name);
            subject.Description.Should().Be(validCategory.Description);
            subject.Id.Should().NotBeEmpty();
            subject.IsActive.Should().BeTrue();

            var dateTimeAfter = DateTime.Now;
            subject.CreatedAt.Should().NotBeSameDateAs(default);
            subject.CreatedAt.Should().BeAfter(dateTimeBefore);
            subject.CreatedAt.Should().BeBefore(dateTimeAfter);
        }

        [Theory(DisplayName = nameof(InstatiateWithIsActiveStatus))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstatiateWithIsActiveStatus(bool isActive)
        {
            var dateTimeBefore = DateTime.Now;
            var validCategory = _categoryTestFixture.GetValidCategory();

            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);

            subject.Should().NotBeNull();
            subject.Name.Should().Be(validCategory.Name);
            subject.Description.Should().Be(validCategory.Description);
            subject.IsActive.Should().Be(isActive);

            var dateTimeAfter = DateTime.Now;
            subject.Id.Should().NotBeEmpty();
            subject.CreatedAt.Should().NotBeSameDateAs(default);
            subject.CreatedAt.Should().BeAfter(dateTimeBefore);
            subject.CreatedAt.Should().BeBefore(dateTimeAfter);
        }

        [Theory(DisplayName = nameof(ErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void ErrorWhenNameIsEmpty(string? name)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action act = () => new DomainEntity.Category(name, validCategory.Description);

            act.Should().Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");
        }

        [Fact(DisplayName = nameof(ErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void ErrorWhenDescriptionIsNull()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action act = () => new DomainEntity.Category(validCategory.Name, null);

            act.Should().Throw<EntityValidationException>()
                .WithMessage("Description should not be null");
        }

        [Theory(DisplayName = nameof(ErrorWhenNameIsLessThan3Characthers))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("a")]
        [InlineData("ab")]
        public void ErrorWhenNameIsLessThan3Characthers(string name)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            Action act = () => new DomainEntity.Category(name, validCategory.Description);

            act.Should().Throw<EntityValidationException>()
                .WithMessage("Name should have at least 3 characters");
        }

        [Fact(DisplayName = nameof(ErrorWhenNameIsGreaterThan255Characthers))]
        [Trait("Domain", "Category - Aggregates")]
        public void ErrorWhenNameIsGreaterThan255Characthers()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            string name = new('A', 256);

            Action act = () => new DomainEntity.Category(name, validCategory.Description);

            act.Should().Throw<EntityValidationException>()
                .WithMessage("Name should have less than 255 characters");
        }

        [Fact(DisplayName = nameof(ErrorWhenDescriptionIsGreaterThan10_000Characthers))]
        [Trait("Domain", "Category - Aggregates")]
        public void ErrorWhenDescriptionIsGreaterThan10_000Characthers()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            string description = new string('A', 11000);

            Action act = () => new DomainEntity.Category(validCategory.Name, description);

            act.Should().Throw<EntityValidationException>()
                .WithMessage("Description should have less than 10000 characters");
        }

        [Fact(DisplayName = nameof(Activate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Activate()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();

            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
            subject.Activate();
            subject.IsActive.Should().BeTrue();
        }

        [Fact(DisplayName = nameof(Deactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Deactivate()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
            subject.Deactivate();
            subject.IsActive.Should().BeFalse();
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var validCategoryNewValues = _categoryTestFixture.GetValidCategory();
            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description);

            subject.Update(validCategoryNewValues.Name, validCategoryNewValues.Description);

            subject.Name.Should().Be(validCategoryNewValues.Name);
            subject.Description.Should().Be(validCategoryNewValues.Description);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateOnlyName()
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var validCategoryNewValues = _categoryTestFixture.GetValidCategory();
            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description);

            subject.Update(validCategoryNewValues.Name);

            subject.Name.Should().Be(validCategoryNewValues.Name);
            subject.Description.Should().Be(validCategory.Description);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void UpdateErrorWhenNameIsEmpty(string? name)
        {
            var validCategory = _categoryTestFixture.GetValidCategory();
            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description);

            subject.Invoking(y => y.Update(name))
                .Should().Throw<EntityValidationException>()
                .WithMessage("Name should not be empty or null");
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characthers))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characthers()
        {
            string description = new string('A', 11000);

            var validCategory = _categoryTestFixture.GetValidCategory();
            var validCategoryNewValues = _categoryTestFixture.GetValidCategory();
            var subject = new DomainEntity.Category(validCategory.Name, validCategory.Description);

            subject.Invoking(y => y.Update(validCategoryNewValues.Name, description))
                .Should().Throw<EntityValidationException>()
                .WithMessage("Description should have less than 10000 characters");
        }
    }
}

