using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.Domain.Entities;

namespace OnlineVocabulary.Domain.Services
{
    public class VocabularyService : IVocabularyService
    {
        private readonly IRepository<EntryTree> _entryTreeRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly ISearchService _searchService;

        public VocabularyService(
            IRepository<EntryTree> entryTreeRepository,
            IRepository<Category> categoryRepository,
            ISearchService searchService)
        {
            _entryTreeRepository = entryTreeRepository;
            _categoryRepository = categoryRepository;
            _searchService = searchService;
        }


        #region EntryTree

        public EntryTree GetEntryTree(Guid Id)
        {
            var entryTree = _entryTreeRepository.GetById(Id);
            return entryTree;
        }

        public PaginatedList<EntryTree> GetEntryTrees(int pageIndex, int pageSize) 
        {
            var entryTrees = _entryTreeRepository
                                .Paginate(pageIndex, pageSize, x => x.Name);
            return entryTrees;
        }

        public PaginatedList<EntryTree> GetEntryTreesBy(int pageIndex, int pageSize, Expression<Func<EntryTree,bool>> predicate)
        {
            var entryTrees = _entryTreeRepository
                                .Paginate(pageIndex, pageSize, x => x.Name, predicate);
            return entryTrees;
        }

        public PaginatedList<EntryTree> SearchEntryTreesBy(int pageIndex, int pageSize, string searchString)
        {
            var filterSearchString = _searchService.GetFilterSearchString(searchString);
            var entryTrees = _entryTreeRepository.GetMatchesBy(filterSearchString, t => t.Name)
                                .ToPaginatedList(pageIndex, pageSize);
            return entryTrees;
        }


        public IEnumerable<Category> GetEntryTreeCategories(Guid Id)
        {
            var entryTreeCategoriesId = _entryTreeRepository.GetById(Id).Categories;
            var entryTreeCategories = entryTreeCategoriesId
                                        .ConvertAll(x => _categoryRepository.GetById(x));
            return entryTreeCategories;
        }

        public OperationResult<EntryTree> AddEntryTree(EntryTree entryTree)
        {
            if (_entryTreeRepository.GetSingleByName(entryTree.Name) != null)
            {
                return new OperationResult<EntryTree>(false);
            }

            entryTree.Id = Guid.NewGuid();
            _entryTreeRepository.Insert(entryTree);

            return new OperationResult<EntryTree>(true)
            {
                Entity = entryTree
            };
        }

        public OperationResult<EntryTree> UpdateEntryTree(EntryTree entryTree)
        {
            if (_entryTreeRepository.GetById(entryTree.Id) == null) 
            {
                return new OperationResult<EntryTree>(false);
            }

            _entryTreeRepository.Update(entryTree);
            return new OperationResult<EntryTree>(true)
            {
                Entity = entryTree
            };
        }

        public OperationResult RemoveEntryTree(EntryTree entryTree)
        {
            if (_entryTreeRepository.GetById(entryTree.Id) == null)
            {
                return new OperationResult(false);
            }

            _entryTreeRepository.Delete(entryTree);
            return new OperationResult(true);
        }

        #endregion


        #region Category

        public Category GetCategory(Guid Id)
        {
            var category = _categoryRepository.GetById(Id);
            return category;
        }

        public PaginatedList<Category> GetCategories(int pageIndex, int pageSize)
        {
            var categories = _categoryRepository
                                .Paginate(pageIndex, pageSize, x => x.Name);
            return categories;
        }

        public PaginatedList<Category> GetCategoriesBy( int pageIndex, int pageSize, Expression<Func<Category, bool>> predicate)
        {
            var categories = _categoryRepository
                                .Paginate(pageIndex, pageSize, x => x.Name, predicate);
            return categories;
        }

        public PaginatedList<Category> SearchCategoriesBy(int pageIndex, int pageSize, string searchString)
        {
            var filterSearchString = _searchService.GetFilterSearchString(searchString);
            var categories = _categoryRepository.GetMatchesBy(filterSearchString, t => t.Name, k => k.Abbreviation)
                                .ToPaginatedList(pageIndex, pageSize);
            return categories;
        }

        public OperationResult<Category> AddCategory(Category category)
        {
            if (_categoryRepository.GetSingleByName(category.Name) != null)
            {
                return new OperationResult<Category>(false);
            }

            category.Id = Guid.NewGuid();
            _categoryRepository.Insert(category);

            return new OperationResult<Category>(true)
            {
                Entity = category
            };
        }

        public OperationResult<Category> UpdateCategory(Category category)
        {
            if (_categoryRepository.GetById(category.Id) == null)
            {
                return new OperationResult<Category>(false);
            }

            _categoryRepository.Update(category);
            return new OperationResult<Category>(true)
            {
                Entity = category
            };
        }

        public OperationResult RemoveCategory(Category category)
        {
            if (_categoryRepository.GetById(category.Id) == null)
            {
                return new OperationResult(false);
            }

            _categoryRepository.Delete(category);
            return new OperationResult(true);
        }

        #endregion


        #region EntryTreeNode

        public EntryTreeNode GetEntryTreeNode(Guid entryTreeId, Guid entryTreeNodeId)
        {
            var entryTreeNode =  _entryTreeRepository.GetById(entryTreeId)
                                    .TreeNodes.FirstOrDefault(x => x.Id == entryTreeNodeId);
            return entryTreeNode;
        }

        public PaginatedList<EntryTreeNode> GetEntryTreeNodes(Guid entryTreeId, int pageIndex, int pageSize)
        {
            var entryTreeNodes = _entryTreeRepository.GetById(entryTreeId).
                                    TreeNodes.AsQueryable().ToPaginatedList(pageIndex, pageSize);
            return entryTreeNodes;
        }

        public PaginatedList<EntryTreeNode> GetEntryTreeNodesBy(Guid entryTreeId, int pageIndex, int pageSize, Expression<Func<EntryTreeNode, bool>> predicate)
        {
            var entryTreeNodes = _entryTreeRepository.GetById(entryTreeId)
                                    .TreeNodes.AsQueryable().Where(predicate).ToPaginatedList(pageIndex, pageSize);
            return entryTreeNodes;
        }

        public PaginatedList<EntryTreeNode> SearchEntryTreeNodesBy(Guid entryTreeId, string searchString, int pageIndex, int pageSize)
        {
            var filterSearchString = _searchService.GetFilterSearchString(searchString);
            var entryTreeNodes = _entryTreeRepository.GetById(entryTreeId).TreeNodes.AsQueryable()
                                    .Matches(filterSearchString, node => node.ToString()).ToPaginatedList(pageIndex, pageSize);
            return entryTreeNodes;

        }

        public OperationResult<EntryTreeNode> AddEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode)
        {
            var instanceEntryTree = _entryTreeRepository.GetById(entryTreeId);
            if (instanceEntryTree == null) 
            {
                return new OperationResult<EntryTreeNode>(false);
            }

            entryTreeNode.Id = Guid.NewGuid();
            instanceEntryTree.TreeNodes.Add(entryTreeNode);
            _entryTreeRepository.Update(instanceEntryTree);

            return new OperationResult<EntryTreeNode>(true)
            {
                Entity = entryTreeNode
            };
        }

        public OperationResult<EntryTreeNode> UpdateEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode)
        {
            var instanceEntryTree = _entryTreeRepository.GetById(entryTreeId);
            var instanceEntryTreeNode = instanceEntryTree.TreeNodes.FirstOrDefault(x => x.Id == entryTreeNode.Id);

            if ((instanceEntryTree == null) || (instanceEntryTreeNode == null))
            {
                return new OperationResult<EntryTreeNode>(false);
            }

            int indexEntryTreeNode = instanceEntryTree.TreeNodes.IndexOf(instanceEntryTreeNode);
            instanceEntryTree.TreeNodes[indexEntryTreeNode] = entryTreeNode;
            _entryTreeRepository.Update(instanceEntryTree);

            return new OperationResult<EntryTreeNode>(true)
            {
                Entity = entryTreeNode
            };
        }

        public OperationResult RemoveEntryTreeNode(Guid entryTreeId, EntryTreeNode entryTreeNode)
        {
            var instanceEntryTree = _entryTreeRepository.GetById(entryTreeId);
            var instanceEntryTreeNode = instanceEntryTree.TreeNodes.FirstOrDefault(x => x.Id == entryTreeNode.Id);

            if ((instanceEntryTree == null) || 
                (instanceEntryTreeNode == null) ||
                !IsEntryTreeNodeRemovable(entryTreeId, entryTreeNode))
            {
                return new OperationResult(false);
            }

            instanceEntryTree.TreeNodes.Remove(instanceEntryTreeNode);
            _entryTreeRepository.Update(instanceEntryTree);

            return new OperationResult(true);
        }

        #endregion


        #region Private Helpers

        private bool IsEntryTreeNodeRemovable(Guid entryTreeId, EntryTreeNode entryTreeNode) 
        {
            var entryTree = _entryTreeRepository.GetById(entryTreeId);
            var nodeId = entryTree.TreeNodes.FirstOrDefault(e => e.Equals(entryTreeNode)).Id;
            if (entryTree.TreeNodes.Exists(t => t.ParentId.Equals(nodeId))) 
            {
                return false;
            }
            return true;
        }
        
        #endregion
    }
}
