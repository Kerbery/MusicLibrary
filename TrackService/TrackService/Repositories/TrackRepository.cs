using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrackService.Models;

namespace TrackService.Repositories
{
    public class TrackRepository
    {
        private const string collectionName = "tracks";
        private readonly IMongoCollection<Track> dbCollection;

        private readonly FilterDefinitionBuilder<Track> filterBuilder = Builders<Track>.Filter;

        public TrackRepository( )
        {
            var mongoClient = new MongoClient("mongodb://mongodb:27017");
            var database = mongoClient.GetDatabase("Tracks");
            dbCollection = database.GetCollection<Track>(collectionName);

        }

        public async Task<IReadOnlyCollection<Track>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<Track> GetAsync(Guid id)
        {
            FilterDefinition<Track> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Track entity)
        {
            if (entity==null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(Track entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            FilterDefinition<Track> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(Guid id)
        {
            FilterDefinition<Track> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
