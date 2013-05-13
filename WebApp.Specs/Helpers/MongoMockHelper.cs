using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;

namespace WebApp.Specs
{
    public class MongoMockHelper
    {
        public Mock<MongoServer> Server { get; private set; }
        public Mock<MongoDatabase> Database { get; private set; }

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
            Server = new Mock<MongoServer>(ServerSettings);
            Server.Setup(s => s.Settings).Returns(ServerSettings);
            Server.Setup(s => s.IsDatabaseNameValid(It.IsAny<string>(), out message)).Returns(true);

            DatabaseSettings = new MongoDatabaseSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = new ReadPreference(),
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };
            Database = new Mock<MongoDatabase>(Server.Object, "UnitTestDB", DatabaseSettings);
            Database.Setup(db => db.Settings).Returns(DatabaseSettings);
            Database.Setup(db => db.IsCollectionNameValid(It.IsAny<string>(), out message)).Returns(true);

            CollectionSettings = new MongoCollectionSettings()
            {
                GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard,
                ReadEncoding = new UTF8Encoding(),
                ReadPreference = new ReadPreference(),
                WriteConcern = new WriteConcern(),
                WriteEncoding = new UTF8Encoding()
            };
        }

        public Mock<MongoCollection<T>> CollectionByEntity<T>()
        {
            return new Mock<MongoCollection<T>>(Database.Object, typeof(T).Name, CollectionSettings);
        }

        public Mock<MongoDatabase> DatabaseByEntity<T>(Mock<MongoCollection<T>> collection)
        {
            Database.Setup(db => db.GetCollection<T>(typeof(T).Name)).Returns(collection.Object);
            return Database;
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
