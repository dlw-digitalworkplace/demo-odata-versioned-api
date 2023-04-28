namespace Demo.OData.Api;

using Asp.Versioning;
using Demo.OData.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using DbContext = Data.DbContext;

[ApiVersion(1.0)]
[ApiVersion(2.0)]
public class OrderController : BaseODataController<Order>
{
    public OrderController(DbContext dbContext)
        : base(dbContext)
    {
    }

    // GET ~/api/Order/{key}
    [EnableQuery]
    public SingleResult<Order?> Get(int key)
    {
        var entities = DbContext.Orders.Where(it => it.OrderKey == key);

        return SingleResult.Create<Order?>(entities.AsQueryable());
    }

    // PATCH ~/api/Order/{key}
    public virtual async Task<IActionResult> Patch([FromODataUri] int key, Delta<Order> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await DbContext.Set<Order>().FindAsync(key);

        if (entity == null)
        {
            return NotFound(ModelState);
        }

        delta.Patch(entity);
        await DbContext.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE ~/api/Order/{key}
    [MapToApiVersion(2.0)]
    public virtual async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var entity = await DbContext.Set<Order>().FindAsync(key);

        if (entity == null)
        {
            return NotFound();
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }
}
