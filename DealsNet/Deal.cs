using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DealsNet
{
	public class Deal
	{
		public ObjectId _id { get; set; }
		public string submitter_name { get; set; }
		public string product_name { get; set; }
		public double price { get; set; }
		public string store_name { get; set; }
		public int zip_code { get; set; }
		public DateTime expiry_date { get; set; }
	}
}

