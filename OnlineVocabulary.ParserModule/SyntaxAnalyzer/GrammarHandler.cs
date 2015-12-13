using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.ParserModule.Entities;
using OnlineVocabulary.ParserModule.Entities.LexemDefine;
using OnlineVocabulary.ParserModule.Entities.TreeDefine;
using OnlineVocabulary.ParserModule.Entities.Extensions;
using OnlineVocabulary.ParserModule.Entities.Helpers;

namespace OnlineVocabulary.ParserModule.SyntaxAnalyzer
{
    public class GrammarHandler : IGrammarHandler<SyntaxTree,SyntaxNode>
    {
        private readonly IUtilsHandler<Lexem,LexemType> UtilsHandler;
        private readonly List<SyntaxTree> SyntaxTrees;

        public List<SyntaxTree> GetSyntaxTrees 
        {
            get { return SyntaxTrees; }
        }


        public GrammarHandler(IUtilsHandler<Lexem, LexemType> _UtilsHandler) 
        {
            UtilsHandler = _UtilsHandler;
            SyntaxTrees = parseMain().ToList();
        }

        public IEnumerable<SyntaxTree> parseMain()
        {
            while (!UtilsHandler.Peek((LexemType.EOF))) 
            {
                OperationResult<SyntaxTree> syntaxTreeOperationResult;
                if ((syntaxTreeOperationResult = UtilsHandler.Attempt(parseVocabularyEntry)).IsSuccess) 
                {
                    yield return syntaxTreeOperationResult.Entity;
                }
            }
        }

        public SyntaxTree parseVocabularyEntry()
        {
            OperationResult<Lexem> lexemOperationResult;
            if (!(lexemOperationResult = UtilsHandler.Attempt(LexemType.EngKeyword)).IsSuccess)
            {
                return null;
            }

            SyntaxTree tree = new SyntaxTree(lexemOperationResult.Entity);
            OperationResult<SyntaxNode> nodeOperationResult;
            while ((nodeOperationResult = UtilsHandler.Attempt(parseTranslationValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseHomonymValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseGrammarFuncValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseLexicalValue)).IsSuccess)
            {
                nodeOperationResult.Entity.SetParent(tree.RootNode);
                tree.RootNode.Children.Add(nodeOperationResult.Entity);
            }
            if (tree.RootNode.Children.Count().Equals(0))
            {
                return null;
            }
            else
            {
                return tree;
            }
        }

        public SyntaxNode parseHomonymValue()
        {
            OperationResult<Lexem> lexemOperationResult;
            if (!(lexemOperationResult = UtilsHandler.Attempt(LexemType.NumHomonymValue)).IsSuccess)
            {
                return null;
            }
           
            var node = new SyntaxNode(lexemOperationResult.Entity);
            OperationResult<SyntaxNode> nodeOperationResult;
            while ((nodeOperationResult = UtilsHandler.Attempt(parseTranslationValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseGrammarFuncValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseLexicalValue)).IsSuccess)
            {
                nodeOperationResult.Entity.SetParent(node);
                node.Children.Add(nodeOperationResult.Entity);
            }
            if (node.Children.Count().Equals(0))
            {
                return null;
            }
            else
            {
                return node;
            }
        }

        public SyntaxNode parseGrammarFuncValue()
        {
            OperationResult<Lexem> lexemOperationResult;
            if (!(lexemOperationResult = UtilsHandler.Attempt(LexemType.NumGrammFuncValue)).IsSuccess)
            {
                return null;
            }

            var node = new SyntaxNode(lexemOperationResult.Entity);
            OperationResult<SyntaxNode> nodeOperationResult;
            while ((nodeOperationResult = UtilsHandler.Attempt(parseTranslationValue)).IsSuccess ||
                (nodeOperationResult = UtilsHandler.Attempt(parseLexicalValue)).IsSuccess)
            {
                nodeOperationResult.Entity.SetParent(node);
                node.Children.Add(nodeOperationResult.Entity);
            }
            if (node.Children.Count().Equals(0))
            {
                return null;
            }
            else
            {
                return node;
            }
        }

        public SyntaxNode parseLexicalValue()
        {
            OperationResult<Lexem> lexemOperationResult;
            if (!(lexemOperationResult = UtilsHandler.Attempt(LexemType.NumLexValue)).IsSuccess)
            {
                return null;
            }

            var node = new SyntaxNode(lexemOperationResult.Entity);
            OperationResult<SyntaxNode> nodeOperationResult;
            if ((nodeOperationResult = UtilsHandler.Attempt(parseTranslationValue)).IsSuccess)
            {
                nodeOperationResult.Entity.SetParent(node);
                node.Children.Add(nodeOperationResult.Entity);
                return node;
            }
            else
            {
                return null;
            }
        }

        public SyntaxNode parseTranslationValue()
        {
            var node = new SyntaxNode();
            OperationResult<Lexem> operationResult;

            while ((operationResult = UtilsHandler.Attempt(LexemType.Transcription)).IsSuccess ||
                (operationResult = UtilsHandler.Attempt(LexemType.EngAbbreviation)).IsSuccess ||
                (operationResult = UtilsHandler.Attempt(LexemType.RusAbbreviation)).IsSuccess ||
                (operationResult = UtilsHandler.Attempt(LexemType.TranslationReference)).IsSuccess ||
                (operationResult = UtilsHandler.Attempt(LexemType.TranslationMeaning)).IsSuccess ||
                (operationResult = UtilsHandler.Attempt(LexemType.NumSubLexValue)).IsSuccess)
            {
                node.Value.Add(operationResult.Entity);
            }
            if (node.Value.Count().Equals(0))
            {
                return null;
            }
            else 
            {
                return node;
            }
        }

        //
    }
}
