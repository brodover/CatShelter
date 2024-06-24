using Microsoft.AspNetCore.Mvc;
using WithAngularApp.Server.Database.Models;
using WithAngularApp.Server.Services;

namespace WithAngularApp.Server.Controllers
{

    [ApiController]
	[Route("api/[controller]")]
	public class BooksController : ControllerBase
	{
		private readonly DbService _booksService;

		public BooksController(DbService booksService) =>
			_booksService = booksService;

		[HttpGet]
		public async Task<List<Book>> Get() =>
			await _booksService.GetBooksAllAsync();

		[HttpGet("{id:length(24)}")]
		public async Task<ActionResult<Book>> Get(string id)
		{
			var book = await _booksService.GetBookAsync(id);

			if (book is null)
			{
				return NotFound();
			}

			return book;
		}

		[HttpPost]
		public async Task<IActionResult> Post(Book newBook)
		{
			await _booksService.CreateBookAsync(newBook);

			return CreatedAtAction(nameof(Get), new { id = newBook.Id }, newBook);
		}

		[HttpPut("{id:length(24)}")]
		public async Task<IActionResult> Update(string id, Book updatedBook)
		{
			var book = await _booksService.GetBookAsync(id);

			if (book is null)
			{
				return NotFound();
			}

			updatedBook.Id = book.Id;

			await _booksService.UpdateBookAsync(id, updatedBook);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public async Task<IActionResult> Delete(string id)
		{
			var book = await _booksService.GetBookAsync(id);

			if (book is null)
			{
				return NotFound();
			}

			await _booksService.RemoveBookAsync(id);

			return NoContent();
		}
	}
}
