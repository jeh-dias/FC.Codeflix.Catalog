
using FC.Codeflix.Catalog.Domain.Exceptions;
using System.Reflection;
using Xunit;
using DomainEntity = FC.Codeflix.Catalog.Domain.Entity;

namespace FC.Codefix.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTest
    {
        [Fact(DisplayName = nameof(Instanciate))]
        [Trait("Domain", "Category - Agregates")]
        public void Instanciate()
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description"
            };

            var datetimeBefore = DateTime.Now;

            var category = new DomainEntity.Category(validData.Description, validData.Name);

            var datetimeAfter = DateTime.Now;

            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.True(category.IsActive);
        }

        [Theory(DisplayName = nameof(InstanciateWithIsActive))]
        [Trait("Domain", "Category - Agregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstanciateWithIsActive(bool isActive)
        {
            var validData = new
            {
                Name = "category name",
                Description = "category description"
            };

            var datetimeBefore = DateTime.Now;

            var category = new DomainEntity.Category(validData.Description, validData.Name, isActive);

            var datetimeAfter = DateTime.Now;

            Assert.NotNull(category);
            Assert.Equal(validData.Name, category.Name);
            Assert.Equal(validData.Description, category.Description);
            Assert.NotEqual(default(Guid), category.Id);
            Assert.NotEqual(default(DateTime), category.CreatedAt);
            Assert.True(category.CreatedAt > datetimeBefore);
            Assert.True(category.CreatedAt < datetimeAfter);
            Assert.Equal(isActive, category.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Category - Agregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("      ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action =
                () => new DomainEntity.Category("Category Description", name!);

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Agregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            Action action =
                () => new DomainEntity.Category(null!, "Name");

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Description should not be empty or null", exception.Message);
        }

        // name must to have minimum 3 characters
        // name must to have maximum 255 characters
        // description must to have maximum 10.000 characters
    }
}