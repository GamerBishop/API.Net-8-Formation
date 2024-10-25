using FluentValidation.TestHelper;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandValidatorTests
    {
        private readonly UpdateRestaurantCommandValidator _validator;
        private Guid p_Guid = Guid.NewGuid();
        public UpdateRestaurantCommandValidatorTests()
        {
            _validator = new UpdateRestaurantCommandValidator();
        }

        [Fact]
        public void Validator_ForEmptyName_ShouldHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Validator_ForValidName_ShouldNotHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = "Valid Name" };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }

        

        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = "New Name", Description = "New Description", HasDelivery = true };
            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}