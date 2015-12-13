using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.API.Model.DTOs
{
    public class CategoryDTO : IDTO
    {
        public Guid Id { get; set; }

        public string Abbreviation { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

    }
}
