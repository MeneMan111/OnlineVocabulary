using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.Domain.Services
{
    public interface IVocabularyService
    {

        #region EntryTree


        EntryTree GetEntryTree(Guid Id);

        PaginatedList<EntryTree> GetEntryTrees(int pageIndex, int pageSize);

        PaginatedList<EntryTree> GetEntryTreesBy( 
            int pageIndex, int pageSize, 
            Expression<Func<EntryTree, bool>> predicate);

        PaginatedList<EntryTree> SearchEntryTreesBy(
            int pageIndex, int pageSize,
            string searchString);

        IEnumerable<Category> GetEntryTreeCategories(Guid Id);

        OperationResult<EntryTree> AddEntryTree(EntryTree entryTree);

        OperationResult<EntryTree> UpdateEntryTree(EntryTree entryTree);

        OperationResult RemoveEntryTree(EntryTree entryTree);


        #endregion



        #region Category


        Category GetCategory(Guid Id);

        PaginatedList<Category> GetCategories(int pageIndex, int pageSize);

        PaginatedList<Category> GetCategoriesBy(
            int pageIndex, int pageSize, 
            Expression<Func<Category, bool>> predicate);

        PaginatedList<Category> SearchCategoriesBy(
            int pageIndex, int pageSize,
            string searchString);

        OperationResult<Category> AddCategory(Category category);

        OperationResult<Category> UpdateCategory(Category category);

        OperationResult RemoveCategory(Category category);


        #endregion


        #region EntryTreeNode


        EntryTreeNode GetEntryTreeNode(Guid entryTreeId, Guid entryTreeNodeId);

        PaginatedList<EntryTreeNode> GetEntryTreeNodes(
            Guid entryTreeId, 
            int pageIndex, int pageSize);

        PaginatedList<EntryTreeNode> GetEntryTreeNodesBy(
            Guid entryTreeId, 
            int pageIndex, int pageSize, 
            Expression<Func<EntryTreeNode, bool>> predicate);

        PaginatedList<EntryTreeNode> SearchEntryTreeNodesBy(
            Guid entryTreeId, 
            string searchString, 
            int pageIndex, int pageSize);

        OperationResult<EntryTreeNode> AddEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode);

        OperationResult<EntryTreeNode> UpdateEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode);

        OperationResult RemoveEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode);


        #endregion
    }
}
