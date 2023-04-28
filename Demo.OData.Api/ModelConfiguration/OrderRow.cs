namespace Demo.OData.Api.ModelConfiguration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;

public class OrderRow : IModelConfiguration
{
    protected void ConfigureV2(ODataModelBuilder builder)
    {
        ConfigureCurrent(builder);
    }
    protected EntityTypeConfiguration<Data.Entities.OrderRow> ConfigureCurrent(ODataModelBuilder builder)
    {
        return builder.EntitySet<Data.Entities.OrderRow>("OrderRow")
            .EntityType
            .HasKey(p => new { p.OrderKey, p.LineNumber });
    }

    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        if (routePrefix != "api")
        {
            return;
        }

        switch (apiVersion.MajorVersion)
        {
            case 2:
                ConfigureV2(builder);
                break;
        }
    }
}
