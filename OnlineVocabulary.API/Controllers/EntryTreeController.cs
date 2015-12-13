using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineVocabulary.API.Model;
using OnlineVocabulary.API.Model.DTOs;
using OnlineVocabulary.API.Model.RequestCommands;
using OnlineVocabulary.API.Model.RequestModels;
using OnlineVocabulary.Domain.Services;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.API.Controllers
{
    public class EntryTreeController : ApiController
    {
        private readonly IVocabularyService _vocabularyService;

        public EntryTreeController(IVocabularyService vocabularyService) 
        {
            _vocabularyService = vocabularyService;
        }


        public PaginatedDTO<EntryTreeDTO> GetEntryTrees(PaginatedRequestCommand cmd)
        {
            var entryTrees = _vocabularyService.GetEntryTrees(cmd.Page, cmd.Take);

            var result = Mapper.Map<PaginatedList<EntryTree>, PaginatedDTO<EntryTreeDTO>>(entryTrees);
            return result;
        }

        public PaginatedDTO<EntryTreeDTO> GetEntryTreesBy(PaginatedRequestCommand cmd, string searchString)
        {
            var entryTrees = _vocabularyService.SearchEntryTreesBy(cmd.Page, cmd.Take, searchString);

            var result = Mapper.Map<PaginatedList<EntryTree>, PaginatedDTO<EntryTreeDTO>>(entryTrees);
            return result;
        }

        public EntryTreeDTO GetEntryTree(Guid key)
        {
            var entryTree = _vocabularyService.GetEntryTree(key);
            if (entryTree == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var result = Mapper.Map<EntryTree, EntryTreeDTO>(entryTree);
            return result;
        }

        public HttpResponseMessage DeleteEntryTree(Guid key)
        {
            var entryTree = _vocabularyService.GetEntryTree(key);
            if (entryTree == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var entryTreeRemoveResult = _vocabularyService.RemoveEntryTree(entryTree);
            if (!entryTreeRemoveResult.IsSuccess)
            {

                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
