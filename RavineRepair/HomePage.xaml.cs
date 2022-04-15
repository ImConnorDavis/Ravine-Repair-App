using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Identity.Client;
using Xamarin.Forms;

namespace RavineRepair
{
    public partial class HomePage : ContentPage
    {
        public static string UserName { get; set; }
        public static string UserRole { get; set; }
        public static string UserAddress { get; set; }

        //private Button Tickets;
        private AuthenticationResult authenticationResult;
        public static bool IsAdmin;

        public HomePage(AuthenticationResult authResult)
        {
            authenticationResult = authResult;
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            GetClaims();
            base.OnAppearing();
        }
        private void GetClaims()
        {
            var token = authenticationResult.IdToken;
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var data = handler.ReadJwtToken(authenticationResult.IdToken);
                var claims = data.Claims.ToList();
                if (data != null)
                {
                    this.welcome.Text = $"Welcome {data.Claims.FirstOrDefault(x => x.Type.Equals("given_name")).Value}";
                    UserName = data.Claims.FirstOrDefault(x => x.Type.Equals("given_name")).Value;
                    this.address.Text = $"Address: {data.Claims.FirstOrDefault(x => x.Type.Equals("streetAddress")).Value}";
                    UserAddress = data.Claims.FirstOrDefault(x => x.Type.Equals("streetAddress")).Value;
                    //this.jobtitle.Text = $"Welcome {data.Claims.FirstOrDefault(x => x.Type.Equals("given_name")).Value}";
                    //this.issuer.Text = $"Issuer: { data.Claims.FirstOrDefault(x => x.Type.Equals("iss")).Value}";
                    //this.subscription.Text = $"Subscription: {data.Claims.FirstOrDefault(x => x.Type.Equals("sub")).Value}";
                    //this.audience.Text = $"Audience: {data.Claims.FirstOrDefault(x => x.Type.Equals("aud")).Value}";
                    //this.role.Text = $"Role: {data.Claims.FirstOrDefault(x => x.Type.Equals("jobTitle")).Value}";
                    //UserRole = data.Claims.FirstOrDefault(x => x.Type.Equals("jobTitle")).Value;

                    try
                    {
                        this.role.Text = $"Role: {data.Claims.FirstOrDefault(x => x.Type.Equals("jobTitle")).Value}";
                        UserRole = data.Claims.FirstOrDefault(x => x.Type.Equals("jobTitle")).Value;
                    }
                    catch { }
                    //this.email.Text = $"Email: {data.Claims.FirstOrDefault(x => x.Type.Equals("email_address")).Value}";
                }
               
            }
            Console.WriteLine(HomePage.UserRole);

        }
        async void TicketsBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new TicketPages.TicketPage());
        }
        async void SignOutBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            await App.AuthenticationClient.RemoveAsync(authenticationResult.Account);
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
