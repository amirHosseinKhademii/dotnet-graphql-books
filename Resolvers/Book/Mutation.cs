using Graph_Demo.Models;
using Graph_Demo.Repositories;

namespace Graph_Demo.Resolvers
{
    [ExtendObjectType(Name = "Mutation")]
    public class BookMutation
    {
        private readonly IBooksRepository repository;

        public BookMutation(IBooksRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Boolean> CreateBookAsync(string title, string author)
        {
            await repository.CreateBookAsync(title, author);
            return true;
        }

        public async Task<Boolean> UpdateBookAsync(Guid id, string title, string author)
        {
            await repository.UpdateBookAsync(id, title, author);
            return true;
        }

        public async Task<Boolean> DeleteBookAsync(Guid id)
        {
            await repository.DeleteBookAsync(id);
            return true;
        }
    }
}