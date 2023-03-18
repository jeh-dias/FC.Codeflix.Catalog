
using FC.Codeflix.Catalog.Domain.Exceptions;
using System.Net.Http.Headers;

namespace FC.Codeflix.Catalog.Domain.Entity
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public string Description { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public Category(string description, string name, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
            if (Name.Length < 3)
                throw new EntityValidationException($"{nameof(Name)} should not be at least 3 characters");
            if (Name.Length > 255)
                throw new EntityValidationException($"{nameof(Name)} should not be less or equal 255 characters");
            if (Description is null)
                throw new EntityValidationException($"{nameof(Description)} should not be empty or null");
            if (Description.Length > 10000)
                throw new EntityValidationException($"{nameof(Description)} should not be less or equal 10000 characters");
        }
    }
}
