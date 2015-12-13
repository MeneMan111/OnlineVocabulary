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
    public class EntryTreeNodeController : ApiController
    {
        private const string RouteName = "EntryTreeNodeHttpRoute";
        private readonly IVocabularyService _vocabularyService;

        public EntryTreeNodeController(IVocabularyService vocabularyService) 
        {
            _vocabularyService = vocabularyService;
        }


        public PaginatedDTO<EntryTreeNodeDTO> GetEntryTreeNodes(Guid key, PaginatedRequestCommand cmd)
        {
            var entryTreeNodes = _vocabularyService.GetEntryTreeNodes(key, cmd.Page, cmd.Take);

            var result = Mapper.Map<PaginatedList<EntryTreeNode>, PaginatedDTO<EntryTreeNodeDTO>>(entryTreeNodes);
            return result;
        }

        public PaginatedDTO<EntryTreeNodeDTO> GetEntryTreeNodesBy(Guid key, PaginatedRequestCommand cmd, string searchString)
        {
            var entryTreeNodes = _vocabularyService.SearchEntryTreeNodesBy(key, searchString, cmd.Page, cmd.Take);

            var result = Mapper.Map<PaginatedList<EntryTreeNode>, PaginatedDTO<EntryTreeNodeDTO>>(entryTreeNodes);
            return result;
        }

        public EntryTreeNodeDTO GetEntryTreeNode(Guid key, Guid nodeKey)
        {
            var entryTreeNode = _vocabularyService.GetEntryTreeNode(key, nodeKey);
            if (entryTreeNode == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var result = Mapper.Map<EntryTreeNode, EntryTreeNodeDTO>(entryTreeNode);
            return result;
        }

        public HttpResponseMessage DeleteEntryTreeNode(Guid key, Guid nodeKey)
        {
            var entryTreeNode = _vocabularyService.GetEntryTreeNode(key, nodeKey);
            if (entryTreeNode == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var entryTreeNodeRemoveResult = _vocabularyService.RemoveEntryTreeNode(key, entryTreeNode);
            if (!entryTreeNodeRemoveResult.IsSuccess)
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
