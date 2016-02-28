using System;
using System.Web;
using Newtonsoft.Json;

namespace DealsNet
{
	public class GetAllDeals : IHttpHandler
	{
		protected static IDealDatabase database = new DealsDatabase();

		public GetAllDeals (){}

		public bool IsReusable { get { return true; } }

		public void ProcessRequest(HttpContext ctx) {
			ctx.Response.ContentType = "application/json";
			ctx.Response.Write (JsonConvert.SerializeObject(database.GetAllDeals()));
		}
	}
}

