using System;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace DealsNet
{
	public class CreateDeal: HttpDbClient, IHttpHandler
	{
		protected static IDealDatabase database = new DealsDatabase();

		public CreateDeal ()
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
			dynamic deal = JsonConvert.DeserializeObject (jsonString);
			string auth = deal.auth; // Get the authentication string

			if (auth != null && DbUtils.CheckAuth (auth)) {
				// Just checking authentication!
			} else { // User is not authenticated
				ctx.Response.StatusCode = 401;
				ctx.Response.StatusDescription = "Your authentication key is not valid";
				return;
			}

			Deal dealObj = JsonConvert.DeserializeObject<Deal> (jsonString);

			if (!database.AddDeal (auth, dealObj)) {
				ctx.Response.StatusCode = 400;
				ctx.Response.StatusDescription = "Could not add deal";
			};

			ctx.Response.ContentType = "application/json";
			ctx.Response.Write (deal);

		}
	}
}

