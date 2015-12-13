using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using OnlineVocabulary.API.Model.DTOs;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.API.Mappers
{
    public class PaginatedListDTOConverter<T, G> : ITypeConverter<PaginatedList<T>, PaginatedDTO<G>> where G : IDTO
    {
        public PaginatedDTO<G> Convert(ResolutionContext context) 
        {
            var paginatedList = (PaginatedList<T>)context.SourceValue;

            return new PaginatedDTO<G>
            {
                PageIndex = paginatedList.PageIndex,
                PageSize = paginatedList.PageSize,
                TotalCount = paginatedList.TotalCount,
                TotalPageCount = paginatedList.TotalPageCount,
                HasNextPage = paginatedList.HasNextPage,
                HasPreviousPage = paginatedList.HasPreviousPage
                //Items = items
            };
        }
    }
}