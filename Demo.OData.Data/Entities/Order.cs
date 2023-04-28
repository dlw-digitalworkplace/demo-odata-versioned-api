namespace Demo.OData.Data.Entities;

public partial class Order
{
    public int OrderKey { get; set; }

    public int? CustomerKey { get; set; }

    public int? StoreKey { get; set; }

    public DateTime? OrderDate { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? CurrencyCode { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual ICollection<OrderRow> OrderRows { get; } = new List<OrderRow>();

    public virtual Store? Store { get; set; }
}
