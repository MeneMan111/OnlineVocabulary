using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Globalization;

namespace OnlineVocabulary.Domain.Services
{
    public class SearchService : ISearchService
    {

        private readonly Regex RegexStripHtml = new Regex("<[^>]*>", RegexOptions.Compiled);

        private readonly string[] DeleteSymbols = new string[] 
        {
            "\\","|","(",")","[","]","*","?","}","{","^","+"
        };


        public string GetFilterSearchString(string searchString) 
        {
            var term = CleanContent(searchString.ToLowerInvariant().Trim(), false);
            var terms = term.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var filter = string.Format(CultureInfo.InvariantCulture, "({0})", string.Join("|", terms));

            return filter;
        }

        private string StripHtml(string html)
        {
            return string.IsNullOrWhiteSpace(html) ? string.Empty :
                        RegexStripHtml.Replace(html, string.Empty).Trim();
        }

        private string CleanContent(string content, bool removeHtml)
        {
            if (removeHtml)
            {
                content = StripHtml(content);
            }

            foreach (var replaceSymbol in DeleteSymbols) 
            {
                content = content.Replace(replaceSymbol, string.Empty);
            }

            var words = content.Split(new[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var sb = new StringBuilder();
            foreach (var word in
                words.Select(t => t.ToLowerInvariant().Trim()).Where(word => word.Length > 1))
            {
                sb.AppendFormat("{0} ", word);
            }

            return sb.ToString();
        }
    }
}
