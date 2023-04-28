namespace Demo.OData.Api.ModelConfiguration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;

public class Customer : IModelConfiguration
{
    protected void ConfigureV1(ODataModelBuilder builder)
    {
        ConfigureCurrent(builder);
    }
    protected EntityTypeConfiguration<Data.Entities.Customer> ConfigureCurrent(ODataModelBuilder builder)
    {
        return builder.EntitySet<Data.Entities.Customer>("Customer")
            .EntityType
            .HasKey(p => p.CustomerKey);
    }

    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        if (routePrefix != "api")
        {
            return;
        }

        switch (apiVersion.MajorVersion)
        {
            default:
                ConfigureCurrent(builder);
                break;
        }
    }
}
