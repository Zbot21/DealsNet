using System;
using MongoDB.Driver;

namespace DealsNet
{
	public abstract class HttpDbClient
	{
		protected static IMongoClient Client = new MongoClient();
		protected static IMongoDatabase Database = Client.GetDatabase ("DealsNet");
	}
}

