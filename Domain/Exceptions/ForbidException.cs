namespace Restaurants.Domain.Exceptions;

public class ForbidException(string message) : Exception(message)
{
}
