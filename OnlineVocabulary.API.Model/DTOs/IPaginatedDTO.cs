using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.API.Model.DTOs
{

    public interface IPaginatedDTO<out TDto> where TDto : IDTO
    {
        int PageIndex { get; set; }

        int PageSize { get; set; }
        
        int TotalCount { get; set; }
        
        int TotalPageCount { get; set; }
        
        bool HasNextPage { get; set; }
        
        bool HasPreviousPage { get; set; }
        
        IEnumerable<TDto> Items { get; }
    }

}
