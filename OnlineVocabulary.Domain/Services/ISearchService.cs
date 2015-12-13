using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace OnlineVocabulary.Domain.Services
{
    public interface ISearchService
    {
        string GetFilterSearchString(string searchString);
    }
}
