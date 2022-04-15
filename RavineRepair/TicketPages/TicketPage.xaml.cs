using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RavineRepair.TicketPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicketPage : TabbedPage
    {
        //string Info;
        public TicketPage()
        {
            InitializeComponent();
            BindingContext = new TicketControl();

            //var settings = MongoClientSettings.FromConnectionString("mongodb://Connor:Conman00116@iphoenix1-shard-00-00.imsod.mongodb.net:27017,iphoenix1-shard-00-01.imsod.mongodb.net:27017,iphoenix1-shard-00-02.imsod.mongodb.net:27017/sample_airbnb?ssl=true&replicaSet=atlas-9scrlj-shard-0&authSource=admin&retryWrites=true&w=majority");
            //var client = new MongoClient(settings);
            //var database = client.GetDatabase("RavineRepair");
            //var dbList = client.ListDatabases().ToList();
            //var dataList = database.ListCollections().ToList();
            //var collection = database.GetCollection<BsonDocument>("Tickets");
            //var cursor = collection.Find(new BsonDocument()).ToCursor();
            //foreach (var document in cursor.ToEnumerable())
            //{
            //    BsonElement NameSet;
            //    Console.WriteLine(document);
            //    //Console.WriteLine(document.GetElement("Name"));
            //    NameSet = document.GetElement("Name");
            //    Console.WriteLine(NameSet.ToString());

            //    new Ticket
            //    {
            //        Name = NameSet.ToString(),
            //    };
            //}
        }
        async void NewTicketBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewTicketPage()));
        }
        async void TicketSelected(object sender, ItemTappedEventArgs e)
        {
            var mySelectedItem = e.Item as Ticket;
            await Navigation.PushAsync(new EditTicket(mySelectedItem.Name,mySelectedItem.Address,mySelectedItem.Description,mySelectedItem.IsComplete));
        }
    }
}
