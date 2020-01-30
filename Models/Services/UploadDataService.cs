using System.Collections.Generic;
using WebApplication.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebApplication.Models.Services
{
    public class UploadDataService : IUploadDataRepository
    {
        private readonly IMongoCollection<UploadData> _uploadData;
        
        /*
        protected string CollectionName => nameof(UploadData);
      
        private readonly MongoDBStoreDatabaseSettings _mongoDBStoreDatabaseSettings;
        
        private IMongoDatabase database;

        public UploadDataService(IOptionsMonitor<MongoDBStoreDatabaseSettings> optionsMonitor)
        {
            _mongoDBStoreDatabaseSettings = optionsMonitor.CurrentValue;
            _uploadData = database.GetCollection<UploadData>(CollectionName);
        }
         */

        private readonly MongoDBStoreDatabaseSettings _mongoDBStoreDatabaseSettings;
        protected string CollectionName => _mongoDBStoreDatabaseSettings.UploadDataCollectionName;
        //private IMongoDatabase database;

        public UploadDataService(IOptionsMonitor<MongoDBStoreDatabaseSettings> optionsMonitor)
        {
            _mongoDBStoreDatabaseSettings = optionsMonitor.CurrentValue;
            var client = new MongoClient(_mongoDBStoreDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(_mongoDBStoreDatabaseSettings.DatabaseName);
            _uploadData = database.GetCollection<UploadData>(CollectionName);
        }

        public List<UploadData> Get() =>
          _uploadData.Find(uploadData => true).ToList();
        
        public UploadData Get(string id) =>
            _uploadData.Find<UploadData>(uploadData => uploadData.Id == id).FirstOrDefault();

        public UploadData Add(UploadData uploadData)
        {
            _uploadData.InsertOne(uploadData);
            return uploadData;
        }

        public void Delete(string id) =>
            _uploadData.DeleteOne(uploadData => uploadData.Id == id);
    }
}
