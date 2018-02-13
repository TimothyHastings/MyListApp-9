//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using System.Collections.Generic;
using SQLite;
namespace MyListApp
{
	public interface IPlatformServices
	{
		// Database Services
		SQLiteConnection GetConnection(String dbName);
		bool DatabaseExists(string dbname);
		bool DeleteDatabase(string dbname);

		// Web Services
		bool IsServerAvailable(String url);
		bool Login(String url, String resource, String userName, String password);
		List<T> GetList<T>(String url, String resource, String userName, String password);
	}
}
