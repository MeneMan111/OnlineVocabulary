using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Ninject;
using OnlineVocabulary.Domain.Entities;
using OnlineVocabulary.Domain.Services;
using OnlineVocabulary.API.Config;


namespace OnlineVocabulary.API.DependencyResolvers
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            IConfig mongoDbConnections = new APIConfig();
            var mongoDbConnection = mongoDbConnections.mongoDbConnections
                                        .Where(p => string.Compare(p.Name, "sourcemongodb", true) == 0).FirstOrDefault();

            kernel.Bind(typeof(IRepository<>))
                .ToMethod(c => Activator.CreateInstance(
                    typeof(MongoRepository<>),
                    mongoDbConnection.Connection,
                    mongoDbConnection.Database
                    //new object[] { mongoDbConnection.Connection, mongoDbConnection.Database }
            ));

            kernel.Bind<ISearchService>().To<SearchService>();

            kernel.Bind<IParserService>().To<ParserService>();

            kernel.Bind<IVocabularyService>().To<VocabularyService>();

        }
    }
}