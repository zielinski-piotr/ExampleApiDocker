using MongoDB.Driver;

namespace ThirdExampleApi.Data
{
    public class ExampleMongoDatabase : IExampleMongoDatabase
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;

        public ExampleMongoDatabase(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _db = _client.GetDatabase(databaseName);
        }
    
        public IMongoCollection<T> GetCollection<T>() where T : class
        {
            return _db.GetCollection<T>($"{nameof(T)}Collection");
        }
    
        public IMongoClient GetClient()
        {
            return _client;
        }
    }

    public interface IExampleMongoDatabase
    {
        IMongoCollection<T> GetCollection<T>() where T : class;
        IMongoClient GetClient();
    }
}