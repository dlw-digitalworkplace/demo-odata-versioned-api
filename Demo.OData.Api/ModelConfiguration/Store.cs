namespace Demo.OData.Api.ModelConfiguration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;

public class Store : IModelConfiguration
{
    protected void ConfigureV1(ODataModelBuilder builder)
    {
        ConfigureCurrent(builder);
    }
    protected EntityTypeConfiguration<Data.Entities.Store> ConfigureCurrent(ODataModelBuilder builder)
    {
        return builder.EntitySet<Data.Entities.Store>("Store")
            .EntityType
            .HasKey(p => p.StoreKey);
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
