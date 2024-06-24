using Microsoft.AspNetCore.Mvc;
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

		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> Update(string id, string catName)
		{
			var item = await _service.GetCatAsync(id);

			if (item is null)
			{
				return NotFound();
			}

			item.Name = catName;

			await _service.UpdateCatAsync(id, item);

			return NoContent();
		}

		private Cat Generate()
		{
			var item = new Cat();

			var catTypeList = DataClient.GetCatTypeList();
			var catTypeTbl = catTypeList.RandomElement<CatType>();
			item.Pattern = catTypeTbl.Pattern;
			item.Color = catTypeTbl.Color;

			var pick = ThreadSafeRandom.NextDouble();
			var catStatList = DataClient.GetCatStatList();
			foreach (var catStat in catStatList) {
				if (catStat.Prob < pick)
				{
					item.Stats = catStat.Stat;
				}
			}

			var catNameList = DataClient.GetCatNameList();
			var catName = catNameList.RandomElement<string>();
			item.Name = catName;

			return item;
		}
	}
}
