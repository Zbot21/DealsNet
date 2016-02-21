using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.CSharp;

namespace DealsNet
{
	public interface IDealDatabase
	{
		bool DealExists(Deal deal);
		bool AddDeal(string user, Deal deal);
		Deal GetDeal(string id);
		IList<Deal> DynamicDealSearch (dynamic search);
		bool LikeDeal(string user, string id);
		bool DislikeDeal(string user, string id);
	}
}

