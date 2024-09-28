namespace Restaurants.Domain.Constants;

public static class UserRoles
{
    public static readonly (string Name, string NormalizedName) Admin = ("Admin", "ADMIN");
    public static readonly (string Name, string NormalizedName) Owner = ("Owner", "OWNER");
    public static readonly (string Name, string NormalizedName) User = ("User", "USER");
}
