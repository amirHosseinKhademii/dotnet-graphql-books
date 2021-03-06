using Graph_Demo.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Graph_Demo.Repositories
{
    public record MDBBooksRepo : IBooksRepository
    {
        private const string databaseName = "graph-demo";
        private const string collectionName = "books";
        private readonly IMongoCollection<Book> booksCollection;

        private readonly FilterDefinitionBuilder<Book> filterBuilder = Builders<Book>.Filter;

        public MDBBooksRepo(IMongoClient client)
        {
            IMongoDatabase database = client.GetDatabase(databaseName);
            booksCollection = database.GetCollection<Book>(collectionName);
        }

        public async Task<IEnumerable<Book>> GetBooksAsync() => await booksCollection.Find(new BsonDocument()).ToListAsync();

        public async Task<Book> GetBookAsync(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            return await booksCollection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task CreateBookAsync(string title, string author)

        {
            Author author1 = new()
            {
                Name = author
            };
            Book book = new()
            {
                Title = title,
                Author = author1
            };
            await booksCollection.InsertOneAsync(book);
        }

        public async Task UpdateBookAsync(Guid Id, string title, string author)
        {
            Author author1 = new() { Name = author };
            var filter = filterBuilder.Eq(item => item.Id, Id);
            var update = Builders<Book>.Update.Set(item => item.Title, title).Set(item => item.Author, author1);
            await booksCollection.UpdateOneAsync(filter, update);
        }

        public async Task DeleteBookAsync(Guid Id)
        {
            var filter = filterBuilder.Eq(item => item.Id, Id);
            await booksCollection.DeleteOneAsync(filter);
        }

    }
}