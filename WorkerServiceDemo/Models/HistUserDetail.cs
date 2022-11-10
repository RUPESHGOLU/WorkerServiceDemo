using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WorkerServiceDemo.Models
{
    internal class HistUserDetail
    {
        /// <summary>
        /// _id
        /// </summary>
        [BsonElement("_id")]
        [JsonPropertyName("_id")]
        [BsonIgnoreIfNull]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        [BsonIgnoreIfNull]
        public string? Name { get; set; }

        [BsonElement("City")]
        [JsonPropertyName("City")]
        [BsonIgnoreIfNull]
        public string? City { get; set; }

        [BsonElement("CreatedTimeStamp")]
        [JsonPropertyName("CreatedTimeStamp")]
        [BsonIgnoreIfNull]
        public DateTime? CreatedTimeStamp { get; set; } 
    }
}
