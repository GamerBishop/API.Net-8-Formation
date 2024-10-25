using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Tests.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidatorTests
    {
        private readonly UpdateRestaurantCommandValidator p_Validator;
        private readonly Guid p_Guid = Guid.NewGuid();
        public UpdateRestaurantCommandValidatorTests()
        {
            p_Validator = new UpdateRestaurantCommandValidator();
        }

        [Fact]
        public void Validator_ForEmptyName_ShouldHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = string.Empty };
            var result = p_Validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }

        [Fact]
        public void Validator_ForValidName_ShouldNotHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = "Valid Name" };
            var result = p_Validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(c => c.Name);
        }



        [Fact]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            var command = new UpdateRestaurantCommand(p_Guid) { Name = "New Name", Description = "New Description", HasDelivery = true };
            var result = p_Validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}