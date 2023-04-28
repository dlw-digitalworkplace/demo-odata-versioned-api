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
public class StoreController : BaseODataController<Store>
{
    public StoreController(DbContext dbContext)
        : base(dbContext)
    {
    }

    // GET ~/api/Store/{key}
    [EnableQuery]
    public SingleResult<Store?> Get(int key)
    {
        var entities = DbContext.Stores.Where(it => it.StoreKey == key);

        return SingleResult.Create<Store?>(entities.AsQueryable());
    }

    // PATCH ~/api/Store/{key}
    public virtual async Task<IActionResult> Patch([FromODataUri] int key, Delta<Store> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await DbContext.Set<Store>().FindAsync(key);

        if (entity == null)
        {
            return NotFound(ModelState);
        }

        delta.Patch(entity);
        await DbContext.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE ~/api/Store/{key}
    public virtual async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var entity = await DbContext.Set<Store>().FindAsync(key);

        if (entity == null)
        {
            return NotFound();
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }
}
