
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

        [Theory(DisplayName = nameof(Instanciate_With_Is_Active))]
        [Trait("Domain", "Category - Agregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void Instanciate_With_Is_Active(bool isActive)
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

        [Theory(DisplayName = nameof(Instantiate_Error_When_Name_IsEmpty))]
        [Trait("Domain", "Category - Agregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("      ")]
        public void Instantiate_Error_When_Name_IsEmpty(string? name)
        {
            Action action =
                () => new DomainEntity.Category("Category Description", name!);

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(Instantiate_Error_When_Description_IsNull))]
        [Trait("Domain", "Category - Agregates")]
        public void Instantiate_Error_When_Description_IsNull()
        {
            Action action =
                () => new DomainEntity.Category(null!, "Name");

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Description should not be empty or null", exception.Message);
        }

        // name must to have minimum 3 characters
        [Theory(DisplayName = nameof(Instantiate_Error_When_Name_IsLess_Than_3Characters))]
        [Trait("Domain", "Category - Agregates")]
        [InlineData("1")]
        [InlineData("2")]
        [InlineData("ac")]
        public void Instantiate_Error_When_Name_IsLess_Than_3Characters(string invalidName)
        {
            Action action =
                () => new DomainEntity.Category("Category Ok Description", invalidName);

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Name should not be at least 3 characters", exception.Message);
        }

        // name must to have maximum 255 characters
        [Fact(DisplayName = nameof(Instantiate_Error_When_Name_IsGreather_Than_255Characters))]
        [Trait("Domain", "Category - Agregates")]
        public void Instantiate_Error_When_Name_IsGreather_Than_255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(0, 256).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Category("Category Ok Description", invalidName);

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Name should not be less or equal 255 characters", exception.Message);
        }

        // description must to have maximum 10.000 characters
        [Fact(DisplayName = nameof(Instantiate_Error_When_Description_IsGreather_Than_10000Characters))]
        [Trait("Domain", "Category - Agregates")]
        public void Instantiate_Error_When_Description_IsGreather_Than_10000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Category(invalidDescription, "Name ok");

            var exception = Assert.Throws<EntityValidationException>(action);
            Assert.Equal($"Description should not be less or equal 10000 characters", exception.Message);
        }
    }
}