namespace Restaurants.Application.Users;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
    public bool IsEnroledIn(string role) => Roles.Contains(role);
    public bool IsAdministrator => IsEnroledIn("Admin");
}
