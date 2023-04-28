namespace Demo.OData.Data.Entities;

public partial class Product
{
    public int ProductKey { get; set; }

    public string? ProductCode { get; set; }

    public string? ProductName { get; set; }

    public string? Manufacturer { get; set; }

    public string? Brand { get; set; }

    public string Color { get; set; } = null!;

    public string? WeightUnitMeasure { get; set; }

    public double? Weight { get; set; }

    public decimal? UnitCost { get; set; }

    public decimal? UnitPrice { get; set; }

    public string? SubcategoryCode { get; set; }

    public string? Subcategory { get; set; }

    public string? CategoryCode { get; set; }

    public string? Category { get; set; }

    public virtual ICollection<OrderRow> OrderRows { get; } = new List<OrderRow>();
}
