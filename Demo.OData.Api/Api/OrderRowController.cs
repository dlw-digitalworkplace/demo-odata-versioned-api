namespace Demo.OData.Api;

using Asp.Versioning;
using Demo.OData.Data;
using Demo.OData.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

[ApiVersion(2.0)]
public class OrderRowController : BaseODataController<OrderRow>
{
    public OrderRowController(DbContext dbContext)
        : base(dbContext)
    {
    }

    // GET ~/api/OrderRow/{key}
    [EnableQuery]
    public SingleResult<OrderRow?> Get([FromODataUri] int keyorderKey, [FromODataUri] int keylineNumber)
    {
        var entities = DbContext
            .OrderRows
            .Where(it => it.OrderKey == keyorderKey && it.LineNumber == keylineNumber);

        return SingleResult.Create<OrderRow?>(entities.AsQueryable());
    }

    // PATCH ~/api/OrderRow/{key}
    public async Task<IActionResult> Patch([FromODataUri] int keyorderKey, [FromODataUri] int keylineNumber, Delta<OrderRow> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var entity = await DbContext.Set<OrderRow>().FindAsync(keyorderKey, keylineNumber);

        if (entity == null)
        {
            return NotFound(ModelState);
        }

        delta.Patch(entity);
        await DbContext.SaveChangesAsync();

        return Updated(entity);
    }

    // DELETE ~/api/OrderRow/{key}
    public async Task<IActionResult> Delete([FromODataUri] int keyorderKey, [FromODataUri] int keylineNumber)
    {
        var entity = await DbContext.Set<OrderRow>().FindAsync(keyorderKey, keylineNumber);

        if (entity == null)
        {
            return NotFound();
        }

        DbContext.Remove(entity);
        await DbContext.SaveChangesAsync();

        return NoContent();
    }
}
