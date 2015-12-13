using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.API.Model.RequestModels
{
    public class CategoryRequestModel
    {
        [Required]
        [StringLength(50)]
        public string Abbreviation { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

    }
}
