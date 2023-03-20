using System.Collections;

using Microsoft.EntityFrameworkCore;

namespace MuHub.Application.Structures;

/// <summary>
/// Paginated list.
/// </summary>
/// <typeparam name="T">Page item type.</typeparam>
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

    /// <summary>
    /// Page size.
    /// </summary>
    public int PageSize { get; }

    /// <summary>
    /// Total pages.
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// Page number.
    /// </summary>
    public int PageNumber { get; }

    /// <summary>
    /// Determines whether the page is first page.
    /// </summary>
    public bool IsFirstPage => PageNumber == 1;

    /// <summary>
    /// Determines whether the page is last page.
    /// </summary>
    public bool IsLastPage => PageNumber == TotalPages;

    /// <summary>
    /// Determines whether the page has previous page.
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Determines whether the page has next page.
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Count of items.
    /// </summary>
    public int Count => _items.Count;

    /// <summary>
    /// Indexer.
    /// </summary>
    /// <param name="index">Index.</param>
    public T this[int index] => _items[index];

    /// <summary>
    /// Creates a paginated list.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="pageNumber">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Paginated list.</returns>
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

    /// <summary>
    /// Creates a paginated list.
    /// </summary>
    /// <param name="items">Items.</param>
    /// <param name="count">Count.</param>
    /// <param name="pageNumber">Page number.</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>Paginated list.</returns>
    public static PagedList<T> FromItems(IEnumerable<T> items, int count, int pageNumber, int pageSize) =>
        new(items, count, pageNumber, pageSize);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _items.GetEnumerator();
}
