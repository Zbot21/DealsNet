using System;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DealsNet
{
	public class HelloWorld : IHttpHandler
	{
		protected static IMongoClient Client = new MongoClient();
		protected static IMongoDatabase Database = Client.GetDatabase ("test");


		public bool IsReusable { get { return true; } }

		public void ProcessRequest(HttpContext ctx){
			ctx.Response.Write ("Hello World");
		}

	}
}

