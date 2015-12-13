using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Domain.Entities
{
    public static class CategoryRepositoryExtensions
    {
        public static Category GetSingleByName(this IRepository<Category> categoryRepository, string name)
        {
            return categoryRepository.GetBy(x => x.Name == name).FirstOrDefault();
        }
    }
}
