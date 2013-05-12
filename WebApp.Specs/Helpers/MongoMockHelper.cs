using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace WebApp.Specs
{
    public class MongoMockHelper
    {
        public MongoServer Server { get; private set; }
        public MongoDatabase Database { get; private set; }

        public MongoServerSettings ServerSettings { get; private set; }
        public MongoDatabaseSettings DatabaseSettings { get; private set; }
        public MongoCollectionSettings CollectionSettings { get; private set; }

        public MongoMockHelper()
        {
            string message = string.Empty;
            ServerSettings = new MongoServerSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = new ReadPreference(),
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };
            var server = new Mock<MongoServer>(ServerSettings);
            server.Setup(s => s.Settings).Returns(ServerSettings);
            server.Setup(s => s.IsDatabaseNameValid(It.IsAny<string>(), out message)).Returns(true);
            Server = server.Object;

            DatabaseSettings = new MongoDatabaseSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = new ReadPreference(),
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };
            var database = new Mock<MongoDatabase>(Server, "UnitTestDB", DatabaseSettings);
            database.Setup(db => db.Settings).Returns(DatabaseSettings);
            database.Setup(db => db.IsCollectionNameValid(It.IsAny<string>(), out message)).Returns(true);
            Database = database.Object;

            CollectionSettings = new MongoCollectionSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = new ReadPreference(),
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };
        }

        public Mock<MongoCollection<T>> MockCollection<T>()
        {
            return new Mock<MongoCollection<T>>(Database, typeof(T).Name, CollectionSettings);
        }
    }

    public class MongoMockResult : WriteConcernResult
    {
        public MongoMockResult()
            : base(new BsonDocument())
        { }

        public new bool UpdatedExisting
        {
            get { return true; }
        }
    }
}
