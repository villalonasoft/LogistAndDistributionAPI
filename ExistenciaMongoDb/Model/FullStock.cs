using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Model
{
    public class FullStock
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public decimal ProductCost { get; set; }
        public decimal Existence { get; set; }
        public int ProductFactor { get; set; }
        public int MaxStock { get; set; }
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
