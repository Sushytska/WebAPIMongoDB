using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication.Models
{
    public class UploadData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Code { get; set; }

        public byte[] Project { get; set; }

        public UploadData()
        {
        }

        public UploadData(string uploadId, string code)
        {
            Id = uploadId;
            Code = code;
        }

        public UploadData(string uploadId, byte[] project)
        {
            Id = uploadId;
            Project = project;
        }
    }
}
