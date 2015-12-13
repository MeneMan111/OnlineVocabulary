using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Domain.Entities
{
    public static class EntryTreeRepositoryExtensions
    {
        public static EntryTree GetSingleByName(this IRepository<EntryTree> entryTreeRepository, string name)
        {
            return entryTreeRepository.GetBy(x => x.Name == name).FirstOrDefault();
        }
    }
}
