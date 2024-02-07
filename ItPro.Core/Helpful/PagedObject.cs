namespace ItPro.Core.Helpful;

/// <summary>
/// Объект, описывающий страницу.
/// </summary>
/// <typeparam name="T">Хранимый тип данных.</typeparam>
public sealed class PagedObject<T>
{
    private readonly List<T> items = new List<T>();

    /// <summary>
    /// Список элементов.
    /// </summary>
    public List<T> Items => this.items;

    /// <summary>
    /// Текущая страница.
    /// </summary>
    public int CurrentPage { get; private set; }
    
    /// <summary>
    /// Общее количество страниц.
    /// </summary>
    public int TotalPages { get; private set; }
    
    /// <summary>
    /// Размер текущей страницы.
    /// </summary>
    public int PageSize { get; private set; }
    
    /// <summary>
    /// Общее количество элементов.
    /// </summary>
    public int TotalCount { get; private set; }
    
    /// <summary>
    /// Налилие предыдущей страницы.
    /// </summary>
    public bool HasPrevious => this.CurrentPage > 1;
    
    /// <summary>
    /// Наличие следующей страницы.
    /// </summary>
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