namespace Demo.OData.Data.Entities;

public partial class Customer
{
    public int CustomerKey { get; set; }

    public string? Gender { get; set; }

    public string? Title { get; set; }

    public string? GivenName { get; set; }

    public string? MiddleInitial { get; set; }

    public string? Surname { get; set; }

    public string? StreetAddress { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? StateFull { get; set; }

    public string? ZipCode { get; set; }

    public string? Country { get; set; }

    public string? CountryFull { get; set; }

    public DateTime? Birthday { get; set; }

    public int? Age { get; set; }

    public string? Occupation { get; set; }

    public string? Company { get; set; }

    public string? Vehicle { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Continent { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
