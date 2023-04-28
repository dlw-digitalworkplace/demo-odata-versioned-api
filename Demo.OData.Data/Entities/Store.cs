namespace Demo.OData.Data.Entities;

public partial class Store
{
    public int StoreKey { get; set; }

    public int? StoreCode { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Name { get; set; }

    public int? SquareMeters { get; set; }

    public DateTime? OpenDate { get; set; }

    public DateTime? CloseDate { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
