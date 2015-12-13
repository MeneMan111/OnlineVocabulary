using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.API.Model.DTOs
{
    public interface IDTO
    {
        public Guid Id { get; set; }
    }
}
