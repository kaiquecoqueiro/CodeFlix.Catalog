using CodeFlix.Catalog.Domain.Exceptions;

namespace CodeFlix.Catalog.Domain.Validation
{
    public class DomainValidation
    {
        public static void MaxLength(string target, int lenght, string fieldName)
        {
            if (target.Length > lenght)
                throw new EntityValidationException($"{fieldName} should have less than {lenght} characters");
        }

        public static void MinLength(string target, int lenght, string fieldName)
        {
            if (target.Length < lenght)
                throw new EntityValidationException($"{fieldName} should have at least {lenght} characters");
        }

        public static void NotNull(object target, string fieldName)
        {
            if (target is null)
                throw new EntityValidationException($"{fieldName} should not be null");
        }

        public static void NotNullOrEmpty(string target, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(target))
                throw new EntityValidationException($"{fieldName} should not be empty or null");
        }
    }
}