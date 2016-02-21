using System;
using System.Collections.Generic;
using System.Collections;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Microsoft.CSharp;
using System.Text.RegularExpressions;

namespace DealsNet
{
	public class DealsDatabase : HttpDbClient, IDealDatabase
	{
		protected static IMongoCollection<BsonDocument> deals_collection = Database.GetCollection<BsonDocument>("deal");
		public DealsDatabase (){}

		public bool DealExists(Deal deal){
			var builder = Builders<BsonDocument>.Filter;
			var filter = builder.Eq ("product_name", deal.product_name) & 
				         builder.Eq ("store_name", deal.store_name) &
					     builder.Eq ("expiry_date", deal.expiry_date) &
					     builder.Eq ("price", deal.price);

			return deals_collection.Find (filter).ToList().Count != 0;
		}
		public bool AddDeal(string user, Deal deal){
			string user_name = DbUtils.GetUserName (user);
			if (user_name.Equals(DbUtils.NO_SUCH_USER))
				return false;

			if (DealExists (deal)) {
				return false;
			}

			deal.submitter_name = user_name;
			var document = deal.ToBsonDocument ();
			deals_collection.InsertOne (document);

			return true;
		}
		public Deal GetDeal(string id){
			var object_id = DbUtils.CreateObjectId (id);
			if (object_id == null)
				return null; // Could not create the object id

			var filter = Builders<BsonDocument>.Filter.Eq ("_id", object_id);
			var results = deals_collection.Find (filter);
			if (results.Count () == 0)
				return null;

			return BsonSerializer.Deserialize<Deal> (results.First ());
		}

		public IList<Deal> GetAllDeals (){
			var filter = new BsonDocument ();
			return Database.GetCollection<Deal> ("deal").Find (filter).ToList ();
		}

		public IList<Deal> DynamicDealSearch(dynamic search){
			var filter = Builders<Deal>.Filter.Empty;
			if (search.product_name != null) {
				System.Diagnostics.Debug.Write("Product Name: " + search.product_name);
				filter = filter & Builders<Deal>.Filter.Regex ("product_name", new BsonRegularExpression((string)search.product_name, "i"));
			} 
			if (search.store_name != null) {
				System.Diagnostics.Debug.Write("Store Name: " + search.store_name);
				filter = filter & Builders<Deal>.Filter.Eq ("store_name", new BsonRegularExpression((string)search.store_name, "i"));
			} 
			if (search.zip_code != null) {
				System.Diagnostics.Debug.Write("Zip Code: " + search.zip_code);
				filter = filter & Builders<Deal>.Filter.Eq ("zip_code", (int)search.zip_code);
			}
			return Database.GetCollection<Deal> ("deal").Find (filter).ToList ();
		}

		public bool LikeDeal(string user, string id){
			throw new NotImplementedException ();
		}
		public bool DislikeDeal(string user, string id){
			throw new NotImplementedException ();
		}


	}
}

