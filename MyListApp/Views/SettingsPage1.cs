using System;

using Xamarin.Forms;

namespace MyListApp
{
    public class SettingsPage1 : ContentPage
    {
        void GetData()
        {
            if (Model.Users.Count > 0){
                firstNameEntry.Text = Model.Users[0].FirstName;
                surnameEntry.Text = Model.Users[0].Surname;
                userNameEntry.Text = Model.Users[0].UserName;
                passwordEntry.Text = Model.Users[0].Password;
                serverURLEntry.Text = Model.Users[0].ServerURL;
            }
            else{
                firstNameEntry.Text = "";
                surnameEntry.Text = "";
                userNameEntry.Text = "";
                passwordEntry.Text = "";
                serverURLEntry.Text = "";
            }
        }
        ActivityIndicator indicator = new ActivityIndicator()
        {
            IsVisible = true,
            IsRunning = true
        };

        Label descriptionLabel = new Label()
        {
            Text = "Please enter your credentials.",
        };

        Entry firstNameEntry = new Entry()
        {
            Placeholder="First Name",
            WidthRequest = 250,
            HorizontalOptions = LayoutOptions.Center
        };

        Entry surnameEntry = new Entry()
        {
            Placeholder = "Surname",
            WidthRequest = 250,
            HorizontalOptions = LayoutOptions.Center
        };

        Entry userNameEntry = new Entry()
        {
            Placeholder="User name",
            WidthRequest = 250,
            HorizontalOptions = LayoutOptions.Center
        };

        Entry passwordEntry = new Entry()
        {
            Placeholder = "Password",
            WidthRequest = 250,
            IsPassword = true,
            HorizontalOptions = LayoutOptions.Center
        };

        Entry serverURLEntry = new Entry()
        {
            Placeholder = "Server URL",
            WidthRequest = 250,
            Keyboard = Keyboard.Url,
            HorizontalOptions = LayoutOptions.Center
        };

        Button saveButton = new Button()
        {
            Text = "Save",
            WidthRequest = 100,
            TextColor = Color.White,
            FontAttributes = FontAttributes.Bold,
            BackgroundColor = Color.Green,
            HorizontalOptions = LayoutOptions.Center
        };

        Label statusLabel = new Label()
        {

        };


        public SettingsPage1()
        {
            Title = "Settings";
            Content = new StackLayout
            {
                Spacing = 20,
                Children ={
                    descriptionLabel,
                    firstNameEntry,
                    surnameEntry,
                    userNameEntry,
                    passwordEntry,
                    serverURLEntry,
                    saveButton,
                    statusLabel
                }
            };

            saveButton.Clicked += OnSaveButtonClicked;

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

            GetData();
        }

        void OnSaveButtonClicked(object sender, EventArgs args)
        {
            // TODO Validate
            User user = new User();
            user.FirstName = firstNameEntry.Text;
            user.Surname = surnameEntry.Text;
            user.UserName = userNameEntry.Text;
            user.Password = passwordEntry.Text;
            user.ServerURL = serverURLEntry.Text;

            // 
            //  Test the API
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

            Model.Users.Clear();
            Model.Users.Add(user);
            Database.SaveModel();


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
    }
}

