
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
    }
}
