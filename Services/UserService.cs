// using System.Collections.Generic;
using System.Collections.Generic;
using Model;
using MongoDB.Driver;

namespace Services{
     public class UserService{
        private readonly IMongoCollection<User> _user;

        public UserService(ISetting setting)
        {
            var client = new MongoClient(setting.ConnectionString);
            var database = client.GetDatabase(setting.DatabaseName);
            _user = database.GetCollection<User>(setting.CollectionName);
        }

        public List<User> Get() =>
            _user.Find(user => true).ToList();

        public User Get(string[] id) =>
            _user.Find<User>(Builders<User>.Filter.In("_id", id)).FirstOrDefault();

        public List<User> Create(List<User> users)
        {
            _user.InsertMany(users);
            return users;
        }

        public void Update(string id, User userIn) =>
            _user.ReplaceOne(user => user._id == id, userIn);

        public void Remove(string[] id) => 
            _user.DeleteMany(Builders<User>.Filter.In("_id", id));
        public void RemoveAll() => 
            _user.DeleteManyAsync(Builders<User>.Filter.Empty);
    }
}