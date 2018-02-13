//
// Copyright (c), 2017. Object Training Pty Ltd. All rights reserved.
// Written by Dr Tim Hastings.
//

using System;
using Xamarin.Forms;
namespace MyListApp
{
	/// <summary>
	/// Sync manager.
	/// </summary>
	public class SyncManager
	{
		//	Messages used by MessagingCenter
		public const String NotSynced = "Not Synced";
		public const String Syncing = "Syncing";
		public const String Synced = "Synced";

		//	Current state
		public String state { get; set; }

		public SyncManager()
		{
			state = NotSynced;
		}

		public void Send(String message)
		{
			state = message;
			MessagingCenter.Send<SyncManager>(this, message);
		}
	}
}
