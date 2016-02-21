using System;
using System.IO;
using System.Web;
using Newtonsoft.Json;
using Microsoft.CSharp;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DealsNet
{
	public class UserAuth : HttpDbClient, IHttpHandler
	{
		public bool IsReusable { get { return true; } }

		public void ProcessRequest(HttpContext ctx){
			// Check to see if the user wasn't dumb and didn't give me json, I want JSON!!!
			if (!ctx.Request.ContentType.Equals("application/json")) {
				ctx.Response.StatusCode = 400; // Bad Request, so return 400
				ctx.Response.StatusDescription = "The request must be sent in JSON format.";
				return;
			}

			// Convert the request into JSON
			String jsonString = Utils.GetStringFromStream (ctx.Request.InputStream);
			dynamic data = JsonConvert.DeserializeObject (jsonString);

			// Check that the user is not null
			if (data.user_name == null) {
				ctx.Response.StatusCode = 400;
				ctx.Response.StatusDescription = "The request must be sent in JSON format with the following field: user_name";
				return;
			}

			string user_name = data.user_name;

			// Check if the user is already in the database
			var collection = Database.GetCollection<BsonDocument> ("user");
			var filter = Builders<BsonDocument>.Filter.Eq ("user_name", user_name);
			if (collection.Find (filter).ToList ().Count != 0) {
				ctx.Response.StatusCode = 400;
				ctx.Response.StatusDescription = "Sorry, a user with the name \"" + user_name + "\" already exists";
				return;
			}

			// Set up the document and insert the user
			var document = new BsonDocument 
			{
				{ "user_name" , user_name }
			}; 
			collection.InsertOne (document);

			data.id = document.GetValue ("_id").ToString();

			ctx.Response.ContentType = "application/json";
			ctx.Response.Write (data);
		}
	}
}

