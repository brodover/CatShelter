using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WithAngularApp.Server.Config;
using WithAngularApp.Server.Database.Models;

namespace WithAngularApp.Server.Services
{
    public class DbService
	{
		private readonly IMongoCollection<Book> _booksCollection;
		private readonly IMongoCollection<Cat> _catsCollection;

		public DbService(
			IOptions<DatabaseSettings> databaseSettings)
		{
			var mongoClient = new MongoClient(
				databaseSettings.Value.ConnectionString);

			var mongoDatabase = mongoClient.GetDatabase(
				databaseSettings.Value.DatabaseName);

			_booksCollection = mongoDatabase.GetCollection<Book>(
				databaseSettings.Value.BooksCollectionName);

			_catsCollection = mongoDatabase.GetCollection<Cat>(
				databaseSettings.Value.CatsCollectionName);
		}

		//_booksCollection
		public async Task<List<Book>> GetBooksAllAsync() =>
			await _booksCollection.Find(_ => true).ToListAsync();

		public async Task<Book?> GetBookAsync(string id) =>
			await _booksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task CreateBookAsync(Book newBook) =>
			await _booksCollection.InsertOneAsync(newBook);

		public async Task UpdateBookAsync(string id, Book updatedBook) =>
			await _booksCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

		public async Task RemoveBookAsync(string id) =>
			await _booksCollection.DeleteOneAsync(x => x.Id == id);


		//_catsCollection
		public async Task<List<Cat>> GetCatsAllAsync() =>
			await _catsCollection.Find(_ => true).ToListAsync();

		public async Task<Cat?> GetCatAsync(string id) =>
			await _catsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

		public async Task<List<Cat>> GetCatsAdopterIdAsync(string id) =>
			await _catsCollection.Find(x => x.AdopterId == id).ToListAsync();

		public async Task CreateCatAsync(Cat cat) =>
			await _catsCollection.InsertOneAsync(cat);

		public async Task RemoveCatAsync(string id) =>
			await _catsCollection.DeleteOneAsync(x => x.Id == id);

		public async Task UpdateCatAsync(string id, Cat item) =>
			await _catsCollection.ReplaceOneAsync(x => x.Id == id, item);

	}
}
