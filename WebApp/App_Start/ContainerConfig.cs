using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MongoDB.Driver;
using OpenCat.Models;

namespace OpenCat
{
    public static class ContainerConfig
    {
        public static void RegisterContainer() 
        {
            // Create the container builder.
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register(db =>
            {
                var dbName = ConfigurationManager.AppSettings["dbName"];
                return new MongoClient().GetServer().GetDatabase(dbName);
            }).As<MongoDatabase>().InstancePerHttpRequest();

            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerHttpRequest();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service"))
                .AsSelf().InstancePerHttpRequest();

            // Build the container.
            var container = builder.Build();

            // Create the depenedency resolver.
            var resolver = new AutofacWebApiDependencyResolver(container);

            // Configure Web API with the dependency resolver.
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            // Configure MVC with the dependency resolver.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}