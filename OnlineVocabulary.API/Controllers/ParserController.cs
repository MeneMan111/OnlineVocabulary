using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineVocabulary.API.Model;
using OnlineVocabulary.Domain.Services;
using OnlineVocabulary.Domain.Entities;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;

namespace OnlineVocabulary.API.Controllers
{
    public class ParserController : ApiController
    {
        private readonly IVocabularyService _vocabularyService;
        private readonly IParserService _parserService;

        public ParserController(IVocabularyService vocabularyService, IParserService parserService) 
        {
            _vocabularyService = vocabularyService;
            _parserService = parserService;
        }

        [HttpPost]
        public HttpResponseMessage ParseString(string parseString)
        {
            var parsedStringResult = _parserService.TryParseString(parseString);
            if (!parsedStringResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }

            var entityTrees = Mapper.Map<List<SyntaxTree>,List<EntryTree>>(parsedStringResult.Entity);

            foreach (var entityTree in entityTrees) 
            {
                var addedEntityTree = _vocabularyService.AddEntryTree(entityTree);
                if (!addedEntityTree.IsSuccess)
                {
                    return new HttpResponseMessage(HttpStatusCode.Conflict);
                }
            }
            var response = Request.CreateResponse(
                                HttpStatusCode.Created,
                                entityTrees
                            );

            return response;
        }
    }
}
