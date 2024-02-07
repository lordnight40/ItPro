namespace ItPro.Core.Helpful;

public sealed class PagedObject<T>
{
    private readonly List<T> items = new List<T>();

    public List<T> Items => this.items;

    public int CurrentPage { get; private set; }
    
    public int TotalPages { get; private set; }
    
    public int PageSize { get; private set; }
    
    public int TotalCount { get; private set; }
    
    public bool HasPrevious => this.CurrentPage > 1;
    
    public bool HasNext => this.CurrentPage < this.TotalPages;

    public PagedObject(
        IEnumerable<T> items,
        int count,
        int pageNumber,
        int pageSize)
    {
        this.TotalCount = count;
        this.PageSize = pageSize;
        this.CurrentPage = pageNumber;
        this.TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        
        this.items.AddRange(items);
    }
}