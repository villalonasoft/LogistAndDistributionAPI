using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExistenciaMongoDb.Model
{
    public class FullStockDetail
    {
        [BsonId]
        
        public DateTime LastUpdatedAt { get; set; } = DateTime.Now;
    }
}
