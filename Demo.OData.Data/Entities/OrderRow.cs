namespace Demo.OData.Data.Entities;

public partial class OrderRow
{
    public int OrderKey { get; set; }

    public int LineNumber { get; set; }

    public int? ProductKey { get; set; }

    public int? Quantity { get; set; }

    public decimal? UnitPrice { get; set; }

    public decimal? NetPrice { get; set; }

    public decimal? UnitCost { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
