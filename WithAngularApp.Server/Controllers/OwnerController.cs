using Microsoft.AspNetCore.Mvc;
using WithAngularApp.Server.Common;
using WithAngularApp.Server.Data;
using WithAngularApp.Server.Data.Models;
using WithAngularApp.Server.Database.Models;
using WithAngularApp.Server.Services;

namespace WithAngularApp.Server.Controllers
{

    [ApiController]
	[Route("api/[controller]/[action]")]
	public class OwnerController : ControllerBase
	{
		private readonly DbService _service;

		public OwnerController(DbService service) =>
			_service = service;


		[HttpGet]
		public async Task<ActionResult<Owner>> GetOwnerByProviderId([FromBody] Request.Owner.GetOwnerByProviderId body)
		{
			var item = await _service.GetOwnerByProviderIdAsync(body.ProviderId, body.Provider);

			if (item is null)
			{
				return NotFound();
			}

			return item;
		}


		[HttpPut]
		public async Task<IActionResult> LinkProvider([FromBody] Request.Owner.LinkProvider body)
		{
			var item = await _service.GetOwnerAsync(body.Id);

			if (item is null)
			{
				return NotFound();
			}

			item.Provider = body.Provider;
			item.ProviderId = body.ProviderId;

			await _service.UpdateOwnerAsync(body.Id, item);

			return NoContent();
		}
	}
}
