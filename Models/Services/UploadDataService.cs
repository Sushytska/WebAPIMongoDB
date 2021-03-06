﻿using System;
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

        public UploadDataService(IOptionsMonitor<MongoDBStoreDatabaseSettings> optionsMonitor)
        {
            _mongoDBStoreDatabaseSettings = optionsMonitor.CurrentValue;
            var client = new MongoClient(_mongoDBStoreDatabaseSettings.ConnectionString);
            var database = client.GetDatabase(_mongoDBStoreDatabaseSettings.DatabaseName);
            _uploadData = database.GetCollection<UploadData>(CollectionName);
        }
        public UploadData Get(Guid id) =>
            _uploadData.Find<UploadData>(uploadData => uploadData.Id == id).FirstOrDefault();

        public UploadData Add(UploadData uploadData)
        {
            _uploadData.InsertOne(uploadData);
            return uploadData;
        }

        public void Delete(Guid id) =>
            _uploadData.DeleteOne(uploadData => uploadData.Id == id);
    }
}