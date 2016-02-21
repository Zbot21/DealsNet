using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DealsNet
{
	public class DbUtils : HttpDbClient
	{
		public DbUtils (){}

		public static bool CheckAuth(string id){
			var collection = Database.GetCollection<BsonDocument> ("user");
			var object_id = new BsonObjectId (new ObjectId(id));
			var filter = Builders<BsonDocument>.Filter.Eq ("_id", object_id);
			if (collection.Find (filter).ToList().Count == 1) {
				return true;
			} else {
				return false;
			}
		}
	}
}

