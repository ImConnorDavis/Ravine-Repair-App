using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Xamarin.Forms;

namespace RavineRepair.TicketPages
{
    public partial class NewTicketPage : ContentPage
    {
        [BsonElement("NewName")]
        public string NewName { get; set; }
        [BsonElement("NewDescription")]
        public string NewDescription { get; set; }
        [BsonElement("NewAddress")]
        public string NewAddress { get; set;}
        [BsonElement("NewIsComplete")]
        public string NewIsComplete { get; set; }

        public NewTicketPage()
        {
            InitializeComponent();
            //BindingContext = new Ticket();
            BindingContext = this;
        }
        //public async Task SaveCommand(Ticket ticket)
        //{
        //    Console.WriteLine("success");
        //}
        async void SaveCommand(object sender, EventArgs e)
        {
            Console.WriteLine("success");
            var settings = MongoClientSettings.FromConnectionString("mongodb://Connor:Conman00116@iphoenix1-shard-00-00.imsod.mongodb.net:27017,iphoenix1-shard-00-01.imsod.mongodb.net:27017,iphoenix1-shard-00-02.imsod.mongodb.net:27017/sample_airbnb?ssl=true&replicaSet=atlas-9scrlj-shard-0&authSource=admin&retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("RavineRepair");
            var collection = database.GetCollection<BsonDocument>("Tickets");
            NewName = HomePage.UserName;
            NewAddress = HomePage.UserAddress;

            if (NewDescription != null)
            {
                var document = new BsonDocument
                {
                    { "Name", NewName },
                    {"Description", NewDescription },
                    {"Address", NewAddress},
                    { "isComplete", "False" },
                };
                await collection.InsertOneAsync(document);
            }
            await Navigation.PopModalAsync();
        }
        void OnToggled(object sender, ToggledEventArgs e)
        {
            Console.WriteLine("Switched");

        }
    }
}
