using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace RavineRepair
{
    public class TicketControl
    {
        private ObservableCollection<Ticket> tickets;
        public ObservableCollection<Ticket> Tickets
        {
            get { return tickets; }
            set
            {
                tickets = value;
            }
        }

        private ObservableCollection<Ticket> donetickets;
        public ObservableCollection<Ticket> DoneTickets
        {
            get { return donetickets; }
            set
            {
                donetickets = value;
            }
        }

        private ObservableCollection<Ticket> nametickets;
        public ObservableCollection<Ticket> NameTickets
        {
            get { return nametickets; }
            set
            {
                nametickets = value;
            }
        }

        public TicketControl()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://Connor:Conman00116@iphoenix1-shard-00-00.imsod.mongodb.net:27017,iphoenix1-shard-00-01.imsod.mongodb.net:27017,iphoenix1-shard-00-02.imsod.mongodb.net:27017/sample_airbnb?ssl=true&replicaSet=atlas-9scrlj-shard-0&authSource=admin&retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("RavineRepair");
            var collection = database.GetCollection<BsonDocument>("Tickets");
            var cursor = collection.Find(new BsonDocument()).ToCursor();
            Tickets = new ObservableCollection<Ticket>();
            DoneTickets = new ObservableCollection<Ticket>();
            NameTickets = new ObservableCollection<Ticket>();

            foreach (var document in cursor.ToEnumerable())
            {
                BsonElement NameSet, DescriptionSet, AddressSet, IsCompleteSet;
                NameSet = document.GetElement("Name");
                DescriptionSet = document.GetElement("Description");
                try
                {
                    AddressSet = document.GetElement("Address");
                }
                catch { }
                IsCompleteSet = document.GetElement("isComplete");
                //Add Tickets
                if (IsCompleteSet.ToString() == "isComplete=True")
                {
                    DoneTickets.Add(new Ticket()
                    {
                        Name = NameSet.ToString().Replace("=", ": "),
                        Description = DescriptionSet.ToString().Replace("=", ": "),
                        IsComplete = IsCompleteSet.ToString().Replace("=", ": "),
                    });
                }
                if (HomePage.UserRole == "Admin")
                {
                    if (IsCompleteSet.ToString() == "isComplete=False")
                    {
                        Tickets.Add(new Ticket()
                        {
                            Name = NameSet.ToString().Replace("=", ": "),
                            Description = DescriptionSet.ToString().Replace("=", ": "),
                            Address = AddressSet.ToString().Replace("=", ": "),
                            IsComplete = IsCompleteSet.ToString().Replace("=", ": "),
                        });
                    }
                }
                if (HomePage.UserRole != "Admin")
                {
                    if (NameSet.ToString() == "Name=" + HomePage.UserName)
                    {
                        Tickets.Add(new Ticket()
                        {
                            Name = NameSet.ToString().Replace("=", ": "),
                            Description = DescriptionSet.ToString().Replace("=", ": "),
                            Address = AddressSet.ToString().Replace("=", ": "),
                            IsComplete = IsCompleteSet.ToString().Replace("=", ": "),
                        });
                    }
                }
            }
        }
    }
}
