using api_car.Domains.Interface;
using api_car.Domains.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace api_car.Domains.Repository
{
    public class CarRepository : ICarRepository
    {
        private const string databaseName = "Car";
        private const string collectionName = "car";
        public readonly IMongoCollection<Car> collection;
        private readonly FilterDefinitionBuilder<Car> filterBuilder = Builders<Car>.Filter;

        public CarRepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            collection = database.GetCollection<Car>(collectionName);
        }

        public async Task<long> CountAsync()
        {
            return await collection.CountDocumentsAsync(new BsonDocument());
        }

        public async Task CreateAsync(Car car)
        {
            car.CreatedAt = DateTime.UtcNow;

            await collection.InsertOneAsync(car);
        }

        public async Task DeleteAsync(string carId)
        {
            var filterById = filterBuilder.Eq(c => c.CarId, carId);

            await collection.DeleteOneAsync(filterById);
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Car> GetByIdAsync(string carId)
        {
            var filterById = filterBuilder.Eq(c => c.CarId, carId);

            return await collection.Find(filterById).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            var filterById = filterBuilder.Eq(c => c.CarId, car.CarId);

            car.CreatedAt = DateTime.UtcNow;

            await collection.ReplaceOneAsync(filterById, car);
        }
    }
}
