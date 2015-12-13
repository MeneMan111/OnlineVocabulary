using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.API.Model.DTOs
{
    public class PaginatedDTO<TDto> : IPaginatedDTO<TDto> where TDto : IDTO 
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPageCount { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public IEnumerable<TDto> Items { get; set; }

    }       
}
