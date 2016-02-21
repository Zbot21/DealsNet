using System;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace DealsNet
{
	public class FindDeal : IHttpHandler
	{
		protected static IDealDatabase database = new DealsDatabase();

		public FindDeal ()
		{
		}

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
			dynamic req = JsonConvert.DeserializeObject (jsonString);

			// Checking authentication
			string auth = req.auth;
			if (auth != null && DbUtils.CheckAuth (auth)) {
				// Just checking authentication!
			} else { // User is not authenticated
				ctx.Response.StatusCode = 401;
				ctx.Response.StatusDescription = "Your authentication key is not valid";
				return;
			}

			ctx.Response.ContentType = "application/json";
			ctx.Response.Write (JsonConvert.SerializeObject (database.DynamicDealSearch (req)));

		}
	}
}

