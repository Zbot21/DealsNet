using System;
using System.Web;

namespace DealsNet
{
	public class HelloWorld : IHttpHandler
	{
		public bool IsReusable { get { return true; } }

		public void ProcessRequest(HttpContext ctx){
			ctx.Response.Write ("Hello World");
		}

	}
}

