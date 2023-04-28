namespace Demo.OData.Api.ModelConfiguration;

using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.OData.ModelBuilder;

public class Order : IModelConfiguration
{
    protected void ConfigureV1(ODataModelBuilder builder)
    {
        var entity = ConfigureCurrent(builder);

        entity.Ignore(p => p.OrderRows);
    }
    protected EntityTypeConfiguration<Data.Entities.Order> ConfigureCurrent(ODataModelBuilder builder)
    {
        return builder.EntitySet<Data.Entities.Order>("Order")
            .EntityType
            .HasKey(p => p.OrderKey);
    }

    public void Apply(ODataModelBuilder builder, ApiVersion apiVersion, string? routePrefix)
    {
        if (routePrefix != "api")
        {
            return;
        }

        switch (apiVersion.MajorVersion)
        {
            case 1:
                ConfigureV1(builder);
                break;

            default:
                ConfigureCurrent(builder);
                break;
        }
    }
}
