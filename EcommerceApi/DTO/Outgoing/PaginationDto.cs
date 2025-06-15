namespace EcommerceApi.DTO.Outgoing;

public class PaginationDto<T>
{
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IEnumerable<T> Items { get; set; } = [];
}
