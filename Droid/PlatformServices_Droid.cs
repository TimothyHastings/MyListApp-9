//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using System.Collections.Generic;
using SQLite;
using System.IO;
using System.Net;
using Xamarin.Forms;
using MyListApp;


[assembly: Dependency(typeof(PlatformServices_Droid))]
/// <summary>
/// SQlite IO.
/// </summary>
public class PlatformServices_Droid : IPlatformServices
{
	public PlatformServices_Droid() { }

	/// <summary>
	/// Gets the connection.
	/// </summary>
	/// <returns>The connection.</returns>
	/// <param name="dbName">Db name.</param>
	public SQLite.SQLiteConnection GetConnection(String dbName)
	{
		var path = GetFilePath(dbName);
		SQLiteConnection connection = new SQLiteConnection(path);
		return (connection);
	}
	/// <summary>
	/// Deletes the database.
	/// </summary>
	/// <param name="dbname">Dbname.</param>
	public bool DeleteDatabase(string dbname)
	{
		var filePath = GetFilePath(dbname);
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
			return true;
		}
		return false;
	}

	/// <summary>
	/// Test if the Database exists.
	/// </summary>
	/// <returns><c>true</c>, if exists was databased, <c>false</c> otherwise.</returns>
	/// <param name="dbname">Dbname.</param>
	public bool DatabaseExists(string dbname)
	{
		if (File.Exists(GetFilePath(dbname)))
			return true;
		return false;
	}

	/// <summary>
	/// Gets the file path.
	/// </summary>
	/// <returns>The file path.</returns>
	/// <param name="fileName">File name.</param>
	private string GetFilePath(string fileName)
	{
		var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
		var filePath = Path.Combine(documentsPath, fileName);
		return filePath;
	}

	/// <summary>
	/// Test is a server is available.
	/// </summary>
	/// <returns><c>true</c>, if server available was ised, <c>false</c> otherwise.</returns>
	/// <param name="url">URL.</param>
	public bool IsServerAvailable(String url)
	{
		try
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
			request.Timeout = 10000;
			request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
			request.Method = "HEAD";

			using (var response = request.GetResponse())
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	/// <summary>
	/// Login uaing the specified url, userName and password.
	/// </summary>
	/// <param name="url">URL.</param>
	/// <param name="userName">User name.</param>
	/// <param name="password">Password.</param>
	public bool Login(String url, String userName, String resource, String password)
	{
		return true;
	}

	/// <summary>
	/// Gets a list.
	/// </summary>
	/// <returns>The list.</returns>
	/// <param name="url">URL.</param>
	/// <param name="resource">Resource.</param>
	/// <typeparam name="T">The 1st type parameter.</typeparam>
	public List<T> GetList<T>(String url, String resource, String userName, String password)
	{
		return new List<T>();
	}
}
