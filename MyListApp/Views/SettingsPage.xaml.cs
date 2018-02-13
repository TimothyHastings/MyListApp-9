
//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace MyListApp
{
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage()
		{
			InitializeComponent();

			MessagingCenter.Subscribe<SyncManager>(this, SyncManager.Syncing, (sender) =>
			{
				Device.BeginInvokeOnMainThread(() =>
					{
						indicator.IsVisible = true;
						indicator.IsRunning = true;

					});
			});

			MessagingCenter.Subscribe<SyncManager>(this, SyncManager.Synced, (sender) =>
			{
				Device.BeginInvokeOnMainThread(() =>
					{
						indicator.IsVisible = false;
						indicator.IsRunning = false;
					});
			});
		}

		void firstNameEntryCompleted(object sender, EventArgs args)
		{
			var entry = (Entry)sender;
			// TODO Validate
		}

		void surnameEntryCompleted(object sender, EventArgs args)
		{
			var entry = (Entry)sender;
			// TODO Validate
		}

		void UserNameEntryCompleted(object sender, EventArgs args)
		{
			var entry = (Entry)sender;
			// TODO Validate
		}

		void PasswordEntryCompleted(object sender, EventArgs args)
		{
			var entry = (Entry)sender;
			// TODO Validate
		}

		void ServerURLEntryCompleted(object sender, EventArgs args)
		{
			var entry = (Entry)sender;
			// TODO Validate
			IsValidServer(entry.Text);
		}

		void OnSaveButtonClicked(object sender, EventArgs args)
		{
			// TODO Validate
			User user = new User();
			user.FirstName = firstNameEntry.Text;
			user.Surname = surnameNameEntry.Text;
			user.UserName = userNameEntry.Text;
			user.Password = passwordEntry.Text;
			user.ServerURL = serverURLEntry.Text;

			IsValidServer(user.ServerURL);	// could use as test

			Model.Users.Clear();
			Model.Users.Add(user);
			Database.SaveModel();

			// 
			//	Test the API
			//
			if (!WebServices.Login(user.ServerURL, user.UserName, user.Password))
			{
				statusLabel.TextColor = Color.Red;
				statusLabel.Text = "Login Failed";
			}
			else
			{
				statusLabel.TextColor = Color.Green;
				statusLabel.Text = "Login Valid";

				var list = WebServices.GetList<Property>(user.ServerURL, user.UserName, user.Password);
				statusLabel.Text += ", " + list.Count + " Results Retrieved";
			}
		}

		bool IsValidServer(String url)
		{
			if (WebServices.IsServerAvailable(url))
			{
				statusLabel.TextColor = Color.Green;
				statusLabel.Text = "Server Available";
				return true;
			}
			else
			{
				statusLabel.TextColor = Color.Red;
				statusLabel.Text = "Server Not Available";
				return false;
			}
		}

		//	Use to load saved user
		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (Model.Users.Count > 0)
			{
				// Only first user
				firstNameEntry.Text = Model.Users[0].FirstName;
				surnameNameEntry.Text = Model.Users[0].Surname;
				userNameEntry.Text = Model.Users[0].UserName;
				passwordEntry.Text = Model.Users[0].Password;
				serverURLEntry.Text = Model.Users[0].ServerURL;
			}

		}
	}
}
