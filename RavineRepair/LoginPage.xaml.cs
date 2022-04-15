using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace RavineRepair
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            try
            {
                // Look for existing account
                var accounts = await App.AuthenticationClient.GetAccountsAsync();
                if (accounts.Count() >= 1)
                {
                    AuthenticationResult result = await App.AuthenticationClient
                        .AcquireTokenSilent(Constants.Scopes, accounts.FirstOrDefault())
                        .ExecuteAsync();

                await Navigation.PushAsync(new HomePage(result));
                }
            }
            catch
            {
                // Do nothing - the user isn't logged in
            }
            base.OnAppearing();
        }
        async void OnSignInClicked(Object Sender, EventArgs e)
        {
            AuthenticationResult result;
            try
            {
                result = await App.AuthenticationClient
                    .AcquireTokenInteractive(Constants.Scopes)
                    .WithPrompt(Prompt.ForceLogin)
                    .WithParentActivityOrWindow(App.UIParent)
                    .ExecuteAsync();
                //await Navigation.PushAsync(new LoginResultPage(result));
                await Navigation.PushAsync(new HomePage(result));
            }
            catch(MsalClientException ex)
            {
                Console.WriteLine("Fail");
            }
        }
    }
}
