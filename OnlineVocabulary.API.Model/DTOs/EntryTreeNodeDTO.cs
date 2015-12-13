using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.API.Model.DTOs
{
    public class EntryTreeNodeDTO : IDTO
    {
        public Guid Id { get; set; }

        //public int Depth { get; set; }

        public List<KeyValuePair<string,string>> Lexems { get; set; }
        
        public Guid ParentId { get; set; }


        public override string ToString()
        {
            return String.Join(" ", Lexems.Select(ts => ts.Value));
        }
    }
}
