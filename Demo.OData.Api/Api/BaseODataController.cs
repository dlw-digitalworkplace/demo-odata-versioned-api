namespace Demo.OData.Api;

using Asp.Versioning;
using Demo.OData.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[ApiVersion(1.0)]
[ApiVersion(2.0)]
public abstract class BaseODataController<TEntity> : ODataController where TEntity : class
{
    public BaseODataController(DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public DbContext DbContext { get; }

    // GET ~/api/[TEntity]
    [HttpGet]
    [Produces("application/json")]
    [EnableQuery(PageSize = 2)]
    public virtual IEnumerable<TEntity> Get(ODataQueryOptions<TEntity> options)
    {
        var data = DbContext.Set<TEntity>().AsQueryable();

        return data;
    }

    /* --------------------
	 * ALTERNATIVE NOTATION
	 * --------------------
		[HttpGet]
		[Produces("application/json")]
		[ProducesResponseType(typeof(ODataValue<IEnumerable<Prospect>>), Status200OK)]
		[MapToApiVersion("2.0")]
		public virtual IActionResult Get(ODataQueryOptions<Prospect> options)
		{
			var data = DbContext.Prospecten.AsQueryable();

			// Return only the $count if requested
			if (options.Context.Path?.LastSegment is CountSegment)
			{
				return Ok(data.Count());
			}

			return Ok(
				options.ApplyTo(
					data,
					new ODataQuerySettings { PageSize = 10 }
				)
			);
		}
	* --------------------
	*/

    // POST ~/api/[TEntity]
    public virtual async Task<IActionResult> Post(TEntity entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        DbContext.Set<TEntity>().Add(entity);
        await DbContext.SaveChangesAsync();

        return Created(entity);
    }
}
