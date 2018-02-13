//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MyListApp
{
	
	public static class WebServices
	{
		private static String Users = "/api/users?userName=";
		private static String Properties = "/api/grazing/properties";
		/// <summary>
		/// Is the server available.
		/// </summary>
		/// <returns><c>true</c>, if server available was ised, <c>false</c> otherwise.</returns>
		/// <param name="url">URL.</param>
		public static bool IsServerAvailable(String url)
		{
			return (DependencyService.Get<IPlatformServices>().IsServerAvailable(url));

		}

		public static bool Login(String url, String userName, String password)
		{
			return (DependencyService.Get<IPlatformServices>().Login(url, Users, userName, password));
		}

		public static List<T> GetList<T>(String url, String userName, String password)
		{
			return (DependencyService.Get<IPlatformServices>().GetList<T>(url, Properties, userName, password));
		}

	}
}
