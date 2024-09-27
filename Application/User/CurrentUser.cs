namespace Restaurants.Application.User;

public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
{
    public bool isEnroledIn(string role) => Roles.Contains(role);
    public bool isAdministrator => isEnroledIn("Admin");
}
