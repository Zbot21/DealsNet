using System;
using System.Web;
using Newtonsoft.Json;

namespace DealsNet
{
	public class ExpressFeelings : IHttpHandler
	{
		protected static IDealDatabase database = new DealsDatabase();

		public ExpressFeelings (){}

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

			// Getting deal id and feelings
			string deal_id = req.deal_id;
			int feelings = req.feelings;
			if (deal_id == null || feelings == 0) {
				ctx.Response.StatusCode = 400;
				ctx.Response.StatusDescription = "Request must contain both a deal id and feelings"; // Request with feeling dammit!
				return;
			}

			bool result = false;
			switch (feelings) {
				case -1:
					result = database.DislikeDeal (auth, deal_id);
					break;
				case 0:
					result = database.RemoveFeelings (auth, deal_id);
					break;
				case 1:
					result = database.LikeDeal (auth, deal_id);
					break;
			}

			if (result) {
				ctx.Response.StatusCode = 200;
			} else {
				ctx.Response.StatusCode = 400;
				ctx.Response.StatusDescription = "Could not update feelings on that deal.";
			}
		}


	}
}

