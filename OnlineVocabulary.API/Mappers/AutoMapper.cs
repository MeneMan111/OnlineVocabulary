using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using Ninject;
using AutoMapper;
using OnlineVocabulary.Domain.Entities;
using OnlineVocabulary.Domain.Services;
using OnlineVocabulary.API.Model.RequestModels;
using OnlineVocabulary.API.Model.DTOs;

namespace OnlineVocabulary.API.Mappers
{
    public class AutoMapperConfig
    {

        public static void RegisterMappings()
        {

            #region Entitys to DTO

            Mapper.CreateMap<Category, CategoryDTO>()
                    .ForMember(dest => dest.Id,
                                opt => opt.MapFrom(v => v.Id))
                    .ForMember(dest => dest.Abbreviation,
                                opt => opt.MapFrom(v => v.Abbreviation))
                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(v => v.Name))
                    .ForMember(dest => dest.Type,
                                opt => opt.MapFrom(v => v.Type));

            Mapper.CreateMap<EntryTreeNode, EntryTreeNodeDTO>()
                    .ForMember(dest => dest.Id,
                                opt => opt.MapFrom(v => v.Id))
                    .ForMember(dest => dest.Lexems,
                                opt => opt.MapFrom(v => v.Lexems))
                    .ForMember(dest => dest.ParentId,
                                opt => opt.MapFrom(v => v.ParentId));

            Mapper.CreateMap<EntryTree, EntryTreeDTO>()
                    .ForMember(dest => dest.Id,
                                opt => opt.MapFrom(v => v.Id))
                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(v => v.Name))
                    .ForMember(dest => dest.TreeNodes,
                                opt => opt.MapFrom(v => v.TreeNodes));

            #endregion

            #region PaginatedList to PaginatedDTO

            Mapper.CreateMap<PaginatedList<Category>, PaginatedDTO<CategoryDTO>>()
                    .ForMember(dest => dest.Items,
                                opt => opt.MapFrom(v => v.ToList()))
                    .ForMember(dest => dest.PageIndex,
                                opt => opt.MapFrom(v => v.PageIndex))
                    .ForMember(dest => dest.PageSize,
                                opt => opt.MapFrom(v => v.PageSize))
                    .ForMember(dest => dest.TotalCount,
                                opt => opt.MapFrom(v => v.TotalCount))
                    .ForMember(dest => dest.TotalPageCount,
                                opt => opt.MapFrom(v => v.TotalPageCount))
                    .ForMember(dest => dest.HasNextPage,
                                opt => opt.MapFrom(v => v.HasNextPage))
                    .ForMember(dest => dest.HasPreviousPage,
                                opt => opt.MapFrom(v => v.HasPreviousPage));

            Mapper.CreateMap<PaginatedList<EntryTree>, PaginatedDTO<EntryTreeDTO>>()
                    .ForMember(dest => dest.Items,
                                opt => opt.MapFrom(v => v.ToList()))
                    .ForMember(dest => dest.PageIndex,
                                opt => opt.MapFrom(v => v.PageIndex))
                    .ForMember(dest => dest.PageSize,
                                opt => opt.MapFrom(v => v.PageSize))
                    .ForMember(dest => dest.TotalCount,
                                opt => opt.MapFrom(v => v.TotalCount))
                    .ForMember(dest => dest.TotalPageCount,
                                opt => opt.MapFrom(v => v.TotalPageCount))
                    .ForMember(dest => dest.HasNextPage,
                                opt => opt.MapFrom(v => v.HasNextPage))
                    .ForMember(dest => dest.HasPreviousPage,
                                opt => opt.MapFrom(v => v.HasPreviousPage));

            Mapper.CreateMap<PaginatedList<EntryTreeNode>, PaginatedDTO<EntryTreeNodeDTO>>()
                    .ForMember(dest => dest.Items,
                                opt => opt.MapFrom(v => v.ToList()))
                    .ForMember(dest => dest.PageIndex,
                                opt => opt.MapFrom(v => v.PageIndex))
                    .ForMember(dest => dest.PageSize,
                                opt => opt.MapFrom(v => v.PageSize))
                    .ForMember(dest => dest.TotalCount,
                                opt => opt.MapFrom(v => v.TotalCount))
                    .ForMember(dest => dest.TotalPageCount,
                                opt => opt.MapFrom(v => v.TotalPageCount))
                    .ForMember(dest => dest.HasNextPage,
                                opt => opt.MapFrom(v => v.HasNextPage))
                    .ForMember(dest => dest.HasPreviousPage,
                                opt => opt.MapFrom(v => v.HasPreviousPage));

            #endregion

            #region RequestModel to Entitys

            Mapper.CreateMap<CategoryRequestModel, Category>()
                    .ForMember(dest => dest.Abbreviation,
                                opt => opt.MapFrom(v => v.Abbreviation))
                    .ForMember(dest => dest.Name,
                                opt => opt.MapFrom(v => v.Name))
                    .ForMember(dest => dest.Type,
                                opt => opt.MapFrom(v => v.Type));


            #endregion
        }
    }
}