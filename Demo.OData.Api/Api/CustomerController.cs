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
public class CustomerController : BaseODataController<Customer>
{
    public CustomerController(DbContext dbContext)
        : base(dbContext)
    {
    }

    // GET ~/api/Customer/{key}
    [EnableQuery]
    public SingleResult<Customer?> Get(int key)
    {
        var entities = DbContext.Customers.Where(it => it.CustomerKey == key);

        return SingleResult.Create<Customer?>(entities.AsQueryable());
    }

    // PATCH ~/api/Customer/{key}
    public virtual async Task<IActionResult> Patch([FromODataUri] int key, Delta<Customer> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await DbContext.Set<Customer>().FindAsync(key);

        if (entity == null)
        {
            return NotFound(ModelState);
        }

        delta.Patch(entity);
        await DbContext.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE ~/api/Customer/{key}
    public virtual async Task<IActionResult> Delete([FromODataUri] int key)
    {
        var entity = await DbContext.Set<Customer>().FindAsync(key);

        if (entity == null)
        {
            return NotFound();
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }
}
