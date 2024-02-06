namespace ItPro.Core.Repository;

public sealed class PagedObject<T>
{
    private readonly List<T> items = new List<T>();

    public List<T> Items => this.items;

    public int CurrentPage { get; private set; }
    
    public int TotalPages { get; private set; }
    
    public int PageSize { get; private set; }
    
    public int TotalCount { get; private set; }
    
    public bool HasPrevious => CurrentPage > 1;
    
    public bool HasNext => CurrentPage < TotalPages;

    public PagedObject(
        IEnumerable<T> items,
        int count,
        int pageNumber,
        int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        
        this.items.AddRange(items);
    }
}