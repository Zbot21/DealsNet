using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DealsNet
{
	public class DbUtils : HttpDbClient
	{
		public DbUtils (){}

		public const string NO_SUCH_USER = "XXXXNOUSERXXXX";

		public static bool CheckAuth(string id){
			var object_id = CreateObjectId (id);
			if (object_id == null) {
				return false;
			}

			var collection = Database.GetCollection<BsonDocument> ("user");
			var filter = Builders<BsonDocument>.Filter.Eq ("_id", object_id);
			if (collection.Find (filter).ToList().Count == 1) {
				return true;
			} else {
				return false;
			}
		}

		public static string GetUserName(string id){
			var object_id = CreateObjectId (id);
			if (object_id == null) {
				return NO_SUCH_USER;
			}

			var collection = Database.GetCollection<BsonDocument> ("user");
			var filter = Builders<BsonDocument>.Filter.Eq ("_id", object_id);
			var results = collection.Find(filter);
			if (results.Count() == 0)
				return NO_SUCH_USER;

			return results.First ().GetElement ("user_name").Value.ToString();
		}

		public static BsonObjectId CreateObjectId(string id){
			if (id.Length != 24)
				return null;

			return new BsonObjectId (new ObjectId (id));
		}

	}
}

