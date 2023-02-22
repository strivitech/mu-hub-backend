using System.Collections;

using Microsoft.EntityFrameworkCore;

namespace MuHub.Application.Structures;

public class PagedList<T> : IReadOnlyList<T>
{
    private readonly IList<T> _items;

    private PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        _items = items as IList<T> ?? new List<T>(items);
    }

    public int PageSize { get; }

    public int TotalPages { get; }

    public int PageNumber { get; }

    public bool IsFirstPage => PageNumber == 1;

    public bool IsLastPage => PageNumber == TotalPages;

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public int Count => _items.Count;

    public T this[int index] => _items[index];

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken);
        var items = new List<T>();
        if (count > 0)
        {
            items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        }

        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static PagedList<T> FromItems(IEnumerable<T> items, int count, int pageNumber, int pageSize) =>
        new(items, count, pageNumber, pageSize);

    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
}
