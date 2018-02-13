//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using Xamarin.Forms;

namespace MyListApp
{
	/// <summary>
	/// The App.
	/// </summary>
	public class App : Application
	{
		public App()
		{
			if (!DatabaseServices.DatabaseExists(Database.dbName))
			{
				//	This is the first time so create the DB
				Database.CreateDatabase(Database.dbName);
			}

			//MainPage = new NavigationPage(new MyListPage());

			MainPage = new NavigationPage(new TabController()
			{
				// Tab bar format
				BarBackgroundColor = Color.FromHex("c0c0c0"),
				BarTextColor = Color.White
			})
			{
				// Navigation page format
				BarBackgroundColor = Color.FromHex("1275bb"),
				BarTextColor = Color.White
			};

			//MainPage = new NavigationPage(new TabController()
			//{
			//	// Tab bar format
			//	BarBackgroundColor = Color.Blue,
			//	BarTextColor = Color.White
			//});
		}

		protected override void OnStart()
		{
			// Handle when your app starts

			Database.connection = DatabaseServices.OpenDatabase(Database.dbName);
			Database.LoadModel();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
			Database.SaveModel();
			DatabaseServices.CloseDatabase(Database.connection);
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
			Database.connection = DatabaseServices.OpenDatabase(Database.dbName);
			Database.LoadModel();
		}
	}
}
