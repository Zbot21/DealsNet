using System;
using System.IO;

namespace DealsNet
{
	public class Utils
	{
		public Utils (){}

		public static String GetStringFromStream(Stream stream){
			String rtn = String.Empty;
			stream.Position = 0;
			using (var inputStream = new StreamReader(stream)) {
				rtn = inputStream.ReadToEnd ();
			}
			return rtn;
		}

		public static bool DoesPropertyExist(dynamic thing, string name){
			return thing.GetType().GetProperty (name) != null;
		}

	}
}

