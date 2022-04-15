using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using Xamarin.Forms;

namespace RavineRepair.TicketPages
{
    public partial class EditTicket : ContentPage
    {
        public static string EditDescription { get; set; }
        //public string EditIsComplete { get; set; }
        public EditTicket(string Name, string Address, string Description, string IsComplete)
        {
            InitializeComponent();
            bool EditIsComplete;
            //BindingContext = this;
            //Get Ticket Information
            Name = Name.Replace("Name: ", "");
            TicketName.Text = Name;
            if (Address != null) { 
                Address = Address.Replace("Address: ", "");
                TicketAddress.Text = Address;}
            Description = Description.Replace("Description: ", "");
            TicketDescription.Text = Description;
            IsComplete = IsComplete.Replace("isComplete: ", "");
            EditIsComplete = Convert.ToBoolean(IsComplete);
            //EditIsComplete = "tru";
            //Console.WriteLine(EditIsComplete);
            EditDescription = Description;
            SetSwitch.IsToggled = EditIsComplete;
        }
        void OnSwitched(object sender, ToggledEventArgs e)
        {
            //Console.WriteLine(EditDescription);
            var settings = MongoClientSettings.FromConnectionString("mongodb://Connor:Conman00116@iphoenix1-shard-00-00.imsod.mongodb.net:27017,iphoenix1-shard-00-01.imsod.mongodb.net:27017,iphoenix1-shard-00-02.imsod.mongodb.net:27017/sample_airbnb?ssl=true&replicaSet=atlas-9scrlj-shard-0&authSource=admin&retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("RavineRepair");
            var collection = database.GetCollection<BsonDocument>("Tickets");
            //collection.Find({"Name": Description });
            //Console.WriteLine("Description: " + EditDescription);
            collection.FindOneAndUpdateAsync(
                Builders<BsonDocument>.Filter.Eq("Description", EditDescription),
                Builders<BsonDocument>.Update.Set("isComplete", e.Value.ToString()));
            Console.WriteLine(e.Value);

        }
    }
}
