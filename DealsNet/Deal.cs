using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace DealsNet
{
	public class Deal
	{
		public Deal(){
			likers = new List<UserId> ();
			dislikers = new List<UserId> ();
		}
		public ObjectId _id { get; set; }
		public string submitter_name { get; set; }
		public string product_name { get; set; }
		public double price { get; set; }
		public string store_name { get; set; }
		public int zip_code { get; set; }
		public DateTime expiry_date { get; set; }
		public IList<UserId> likers;
		public IList<UserId> dislikers;
	}
}

