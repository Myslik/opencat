using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using OpenCat.Models;

namespace WebApp.Specs.Units
{
    public class NamedEntity : Entity
    {
        public string name { get; set; }
    }

    [TestFixture]
    public class RepositoryTest
    {
        protected Repository<NamedEntity> repository;
        protected Mock<MongoCollection<NamedEntity>> collection;

        [SetUp]
        protected void SetUp()
        {
            var mongo = new MongoMockHelper();
            collection = mongo.CollectionByEntity<NamedEntity>();
            repository = new Repository<NamedEntity>(mongo.DatabaseByEntity<NamedEntity>(collection).Object);
        }

        [Test]
        [Category("Repository")]
        public void CreateTest()
        {
            NamedEntity created = null;
            collection.Setup(c => c.Insert(It.IsAny<NamedEntity>())).Callback<NamedEntity>(e => created = e);

            var before = DateTime.UtcNow;
            repository.Create(new NamedEntity());
            var after = DateTime.UtcNow;

            Assert.IsNotNull(created);

            Assert.AreNotEqual(ObjectId.Empty.ToString(), created.id);
            Assert.AreEqual(created.created_at, created.updated_at);

            Assert.LessOrEqual(before, created.created_at);
            Assert.GreaterOrEqual(after, created.created_at);
        }

        [Test]
        [Category("Repository")]
        public void UpdateTest()
        {
            IMongoQuery query = null;
            IMongoUpdate update = null;
            collection.Setup(c => c.Update(It.IsAny<IMongoQuery>(), It.IsAny<IMongoUpdate>()))
                .Callback<IMongoQuery, IMongoUpdate>((q, u) =>
                {
                    query = q;
                    update = u;
                }).Returns(() =>
                {
                    return new MongoMockResult();
                });

            var entity = new NamedEntity { name = "First" };
            repository.Create(entity);

            var updateEntity = new NamedEntity { name = "Second" };
            repository.Update(entity.id, updateEntity);
            Assert.IsNotNull(query);
            Assert.IsNotNull(update);

            Assert.AreEqual(entity.id, query.ToBsonDocument()["_id"].ToString());
            var document = update.ToBsonDocument();
            Assert.IsNotNull(document["$set"]["updated_at"]);
            Assert.AreEqual(updateEntity.name, document["$set"]["name"].AsString);
            Assert.AreEqual(2, document["$set"].AsBsonDocument.ElementCount);
        }
    }
}
