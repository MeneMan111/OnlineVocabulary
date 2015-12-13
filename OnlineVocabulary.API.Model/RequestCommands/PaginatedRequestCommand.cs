using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.API.Model.Validation;

namespace OnlineVocabulary.API.Model.RequestCommands
{
   public class PaginatedRequestCommand : IRequestCommand {

        public PaginatedRequestCommand() 
        {
            //
        }
        public PaginatedRequestCommand(int page, int take) 
        {
            Page = page;
            Take = take;
        }

        [Minimum(1)]
        public int Page { get; set; }

        [Minimum(1)]
        [Maximum(50)]
        public int Take { get; set; }
    }
}
