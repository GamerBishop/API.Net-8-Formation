using AutoMapper.Internal.Mappers;

namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
    public bool IsEnroledIn(string role) => Roles.Contains(role);
    public bool IsAdministrator => IsEnroledIn("Admin");

    public override string ToString()
    {
        return $"User : [ Id: {Id}, Email: {Email}, Roles: {string.Join(", ", Roles)}]";
    }
}
