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
	public class CatsController : ControllerBase
	{
		private readonly DbService _service;

		public CatsController(DbService service) =>
			_service = service;

		[HttpGet]
		public async Task<List<Cat>> Get() =>
			await _service.GetCatsAllAsync();

		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult<Cat>> Get(string id)
		{
			var item = await _service.GetCatAsync(id);

			if (item is null)
			{
				return NotFound();
			}

			return item;
		}

		[HttpGet("{id}")]
		public async Task<List<Cat>> GetAdopterId(string id)
		{
			return await _service.GetCatsAdopterIdAsync(id);
		}

		[HttpGet]
		public List<Cat> Visit()
		{
			var catList = new List<Cat>();
			for (int i = 0; i < 3; i++)
			{
				catList.Add(Generate());
			}

			return catList;
		}

		[HttpPost]
		public async Task<IActionResult> Adopt(Cat item)
		{
			await _service.CreateCatAsync(item);

			return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
		}

		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> Abandon(string id)
		{
			var item = await _service.GetCatAsync(id);

			if (item is null)
			{
				return NotFound();
			}

			await _service.RemoveCatAsync(id);

			return NoContent();
		}

		[HttpPut]
		public async Task<IActionResult> Rename([FromBody] Request.Rename body)
		{
			var item = await _service.GetCatAsync(body.Id);

			if (item is null)
			{
				return NotFound();
			}

			item.Name = body.Name;

			await _service.UpdateCatAsync(body.Id, item);

			return NoContent();
		}

		private Cat Generate()
		{
			var item = new Cat();

			var pick = ThreadSafeRandom.NextDouble();
			if (pick < Const.RainbowProb)
			{
				//rainbow color
				item.Pattern = (int) ThreadSafeRandom.Get().NextEnumExcludingNone<Const.Pattern>();
				item.Color = (int) Const.Color.Rainbow;
			}
			else
			{
				// normal colors
				var catTypeList = DataClient.GetCatTypeList();
				var catTypeTbl = catTypeList.RandomElement<CatType>();
				item.Pattern = catTypeTbl.Pattern;
				item.Color = catTypeTbl.Color;
			}

			// stat
			pick = ThreadSafeRandom.NextDouble();
			var catStatList = DataClient.GetCatStatList();
			foreach (var catStat in catStatList) {
				item.Stats = catStat.Stat;

				if (pick < catStat.Prob)
					break;
			}

			// name
			var catNameList = DataClient.GetCatNameList();
			var catName = catNameList.RandomElement<string>();
			item.Name = catName;

			return item;
		}
	}
}
