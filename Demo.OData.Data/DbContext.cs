using Demo.OData.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Demo.OData.Data;

public partial class DbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbContext()
    {
    }

    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderRow> OrderRows { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerKey)
                .HasName("PK_Customer_CustomerKey")
                .IsClustered(false);

            entity.ToTable("Customer", "Data");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Company).HasMaxLength(50);
            entity.Property(e => e.Continent).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.CountryFull).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.GivenName).HasMaxLength(150);
            entity.Property(e => e.MiddleInitial).HasMaxLength(150);
            entity.Property(e => e.Occupation).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.StateFull).HasMaxLength(50);
            entity.Property(e => e.StreetAddress).HasMaxLength(150);
            entity.Property(e => e.Surname).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.Vehicle).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderKey)
                .HasName("PK_Orders_OrderKey")
                .IsClustered(false);

            entity.ToTable("Orders", "Data");

            entity.Property(e => e.OrderKey).ValueGeneratedNever();
            entity.Property(e => e.CurrencyCode)
                .HasMaxLength(3)
                .IsFixedLength()
                .HasColumnName("Currency Code");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("Delivery Date");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("Order Date");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerKey)
                .HasConstraintName("FK_Orders_CustomerKey");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreKey)
                .HasConstraintName("FK_Orders_StoreKey");
        });

        modelBuilder.Entity<OrderRow>(entity =>
        {
            entity.HasKey(e => new { e.OrderKey, e.LineNumber }).HasName("PK_OrderRow");

            entity.ToTable("OrderRows", "Data");

            entity.Property(e => e.LineNumber).HasColumnName("Line Number");
            entity.Property(e => e.NetPrice)
                .HasColumnType("money")
                .HasColumnName("Net Price");
            entity.Property(e => e.UnitCost)
                .HasColumnType("money")
                .HasColumnName("Unit Cost");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("Unit Price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderRows)
                .HasForeignKey(d => d.OrderKey)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderRows_OrderKey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderRows)
                .HasForeignKey(d => d.ProductKey)
                .HasConstraintName("FK_OrderRows_ProductKey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductKey).HasName("PK__Product__A15E99B320B72F25");

            entity.ToTable("Product", "Data");

            entity.Property(e => e.ProductKey).ValueGeneratedNever();
            entity.Property(e => e.Brand).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(30);
            entity.Property(e => e.CategoryCode)
                .HasMaxLength(100)
                .HasColumnName("Category Code");
            entity.Property(e => e.Color).HasMaxLength(20);
            entity.Property(e => e.Manufacturer).HasMaxLength(50);
            entity.Property(e => e.ProductCode)
                .HasMaxLength(255)
                .HasColumnName("Product Code");
            entity.Property(e => e.ProductName)
                .HasMaxLength(500)
                .HasColumnName("Product Name");
            entity.Property(e => e.Subcategory).HasMaxLength(50);
            entity.Property(e => e.SubcategoryCode)
                .HasMaxLength(100)
                .HasColumnName("Subcategory Code");
            entity.Property(e => e.UnitCost)
                .HasColumnType("money")
                .HasColumnName("Unit Cost");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("money")
                .HasColumnName("Unit Price");
            entity.Property(e => e.WeightUnitMeasure)
                .HasMaxLength(20)
                .HasColumnName("Weight Unit Measure");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreKey).HasName("PK__Store__ADC1E1AD827ED66C");

            entity.ToTable("Store", "Data");

            entity.Property(e => e.StoreKey).ValueGeneratedNever();
            entity.Property(e => e.CloseDate)
                .HasColumnType("date")
                .HasColumnName("Close Date");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OpenDate)
                .HasColumnType("date")
                .HasColumnName("Open Date");
            entity.Property(e => e.SquareMeters).HasColumnName("Square Meters");
            entity.Property(e => e.State).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.StoreCode).HasColumnName("Store Code");
        });
    }
}
