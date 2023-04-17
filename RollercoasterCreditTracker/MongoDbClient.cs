namespace RollercoasterCreditTracker
{
    public static class MongoDbClient
    {
        //MAYBE JUST GET SHIT SAVED THEN START DOING THIS REFACTORING

        public static string ConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");

        //var client = new MongoClient(connectionString);
      
        //var collection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("movies");

        //var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");

        //var document = collection.Find(filter).First();        
    }
}
