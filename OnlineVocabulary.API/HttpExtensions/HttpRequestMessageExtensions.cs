using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Dependencies;
using OnlineVocabulary.Domain.Services;

namespace OnlineVocabulary.API
{
    internal static class HttpRequestMessageExtensions
    {
        internal static IVocabularyService GetVocabularyService(this HttpRequestMessage request)
        {
            return request.GetService<IVocabularyService>();
        }

        private static TService GetService<TService>(this HttpRequestMessage request)
        {
            IDependencyScope dependencyScope = request.GetDependencyScope();
            TService service = (TService)dependencyScope.GetService(typeof(TService));

            return service;
        }
    }
}