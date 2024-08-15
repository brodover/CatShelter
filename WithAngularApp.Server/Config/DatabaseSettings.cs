namespace WithAngularApp.Server.Config
{
    public class DatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string BooksCollectionName { get; set; } = null!;
        public string CatsCollectionName { get; set; } = null!;
    }
}
