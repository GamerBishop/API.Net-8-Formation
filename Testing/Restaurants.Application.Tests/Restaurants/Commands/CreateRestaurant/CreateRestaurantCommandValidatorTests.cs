using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Fast Food",
            Description = "Test Restaurant",
            ContactEmail = "test@test.com",
            ContactNumber = "0540254621",
            ZipCode = "31-000",
            City = "Toulouse",
            Street = "Rue de la paix",
            HasDelivery = false
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInvalidCommand_ShouldHaveValidationErrors()
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "",
            Description = "",
            ContactEmail = "@test.com",
            ContactNumber = "45621",
            ZipCode = "31000",
            City = "ToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouse",
            Street = "Rue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paix"
        };

        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.Description);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
        result.ShouldHaveValidationErrorFor(c => c.ZipCode);
        result.ShouldHaveValidationErrorFor(c => c.City);
        result.ShouldHaveValidationErrorFor(c => c.Street);
    }

    [Theory]
    [InlineData("Fast Food")]
    [InlineData("Traditional")]
    [InlineData("Italian")]
    [InlineData("Chinese")]
    [InlineData("Indian")]
    [InlineData("Mexican")]
    [InlineData("American")]
    [InlineData("Other")]
    [InlineData("French")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrors(string category)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Category = category
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory]
    [InlineData("")]
    [InlineData("InvalidCategory")]
    public void Validator_ForInvalidCategory_ShouldHaveValidationErrors(string? category)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Category = category ?? string.Empty
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Category);
    }

    [Theory]
    [InlineData("17-340")]
    [InlineData("31-000")]
    [InlineData("75-000")]
    public void Validator_ForValidZipCode_ShouldNotHaveValidationErrors(string zipCode)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ZipCode = zipCode
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.ZipCode);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("17-3400")]
    [InlineData("17-34")]
    [InlineData("17340")]
    [InlineData("17-340-")]
    [InlineData("17-340-0")]
    [InlineData("17-340-00")]
    public void Validator_ForInvalidZipCode_ShouldHaveValidationErrors(string? zipCode)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ZipCode = zipCode
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ZipCode);
    }

    [Theory]
    [InlineData("test@test.com")]
    [InlineData("example@example.org")]
    [InlineData("user@domain.co")]
    public void Validator_ForValidContactEmail_ShouldNotHaveValidationErrors(string contactEmail)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ContactEmail = contactEmail
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.ContactEmail);
    }


    /* NOTE : 
    
    The email address validator can work in 2 modes. The default mode just performs a simple check
    that the string contains an “@” sign which is not at the beginning or the end of the string. 
    This is an intentionally naive check to match the behaviour of ASP.NET Core’s EmailAddressAttribute, 
    which performs the same check.
    https://github.com/dotnet/runtime/issues/27592#issuecomment-466578595*/
    [Theory]
    [InlineData("")]
    [InlineData("invalid-email")]
    [InlineData("testtest.com")]
    [InlineData("@test.com")]
    public void Validator_ForInvalidContactEmail_ShouldHaveValidationErrors(string? contactEmail)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ContactEmail = contactEmail
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
    }

    [Theory]
    [InlineData("0540254621")]
    [InlineData("1234567890")]
    [InlineData("0987654321")]
    public void Validator_ForValidContactNumber_ShouldNotHaveValidationErrors(string contactNumber)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ContactNumber = contactNumber
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.ContactNumber);
    }

    [Theory]
    [InlineData("")]
    [InlineData("12345")]
    [InlineData("123456789012345")]
    public void Validator_ForInvalidContactNumber_ShouldHaveValidationErrors(string? contactNumber)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            ContactNumber = contactNumber
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.ContactNumber);
    }

    [Theory]
    [InlineData("Toulouse")]
    [InlineData("Paris")]
    [InlineData("Lyon")]
    public void Validator_ForValidCity_ShouldNotHaveValidationErrors(string city)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            City = city
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.City);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("ToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouseToulouse")]
    public void Validator_ForInvalidCity_ShouldHaveValidationErrors(string? city)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            City = city
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.City);
    }

    [Theory]
    [InlineData("Rue de la paix")]
    [InlineData("Avenue des Champs-Élysées")]
    [InlineData("Boulevard Saint-Germain")]
    public void Validator_ForValidStreet_ShouldNotHaveValidationErrors(string street)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Street = street
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Street);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("Rue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paixRue de la paix")]
    public void Validator_ForInvalidStreet_ShouldHaveValidationErrors(string? street)
    {
        // Arrange
        var command = new CreateRestaurantCommand()
        {
            Street = street
        };
        var validator = new CreateRestaurantCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Street);
    }
}
