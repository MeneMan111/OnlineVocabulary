using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.API.Model.DTOs
{
    public class EntryTreeDTO : IDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        //public List<CategoryDTO> Categories { get; set; }

        public List<EntryTreeNodeDTO> TreeNodes { get; set; }

    }
}
