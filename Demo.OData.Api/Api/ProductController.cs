namespace Demo.OData.Api;

using Asp.Versioning;
using Demo.OData.Data;
using Demo.OData.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class ProductController : BaseODataController<Product>
{
    public ProductController(DbContext dbContext)
        : base(dbContext)
    {
    }

    // GET ~/api/Product/{key}
    [EnableQuery]
    public SingleResult<Product?> Get(int key)
    {
        var entities = DbContext.Products.Where(it => it.ProductKey == key);

        return SingleResult.Create<Product?>(entities.AsQueryable());
    }

    // PATCH ~/api/Product/{key}
    public virtual async Task<IActionResult> Patch([FromODataUri] int key, Delta<Product> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await DbContext.Set<Product>().FindAsync(key);

        if (entity == null)
        {
            return NotFound(ModelState);
        }

        delta.Patch(entity);
        await DbContext.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE ~/api/Product/{key}
    public virtual async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var entity = await DbContext.Set<Product>().FindAsync(key);

        if (entity == null)
        {
            return NotFound();
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }
}
