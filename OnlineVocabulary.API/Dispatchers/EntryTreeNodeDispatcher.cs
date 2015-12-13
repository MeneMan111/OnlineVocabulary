using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using OnlineVocabulary.Domain.Services;
using System.Web.Http.Internal;

namespace OnlineVocabulary.API.Dispatchers
{
    public class EntryTreeNodeDispatcher : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            IHttpRouteData routeData = request.GetRouteData();
            Guid entryTreeKey = Guid.ParseExact(routeData.Values["key"].ToString(), "D");

            IVocabularyService vocabularyService = request.GetVocabularyService();
            if (vocabularyService.GetEntryTree(entryTreeKey) == null)
            {
                return Task.FromResult(
                    request.CreateResponse(HttpStatusCode.NotFound));
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}