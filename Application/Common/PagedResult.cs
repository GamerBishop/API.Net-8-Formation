namespace Restaurants.Application.Common;

public class PagedResult<T>(IEnumerable<T> items, int totalItems, int pageSize, int pageNumber) where T : class
{
    public IEnumerable<T> Items { get; } = items;
    public int TotalItems { get; } = totalItems;
    public int TotalPages { get; } = (int)Math.Ceiling(totalItems / (double)pageSize);
    public int ItemsFrom { get; } = (pageNumber - 1) * pageSize + 1;
    public int ItemsTo { get; } = Math.Min(pageNumber * pageSize, totalItems);
}
