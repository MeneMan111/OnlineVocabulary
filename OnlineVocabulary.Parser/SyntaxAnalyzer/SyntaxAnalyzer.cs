using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineVocabulary.Parser.LexicalAnalyzer;
using OnlineVocabulary.Parser.Extensions;
using OnlineVocabulary.Parser.SyntaxTree;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.Parser.SyntaxAnalyzer
{
    partial class SyntaxAnalyzer
    {
        public List<SyntaxTree.SyntaxTree> Trees { get; private set; }

        private Lexem[] Lexems;
        private int LexemId;
        private ObjectId NodeId;

        public SyntaxAnalyzer(IEnumerable<Lexem> lexems)
        {
            Lexems = lexems.ToArray();
            Trees = new List<SyntaxTree.SyntaxTree>();
            var _Trees = Attempt(parseMain);
            if (_Trees != null)
            {
                Trees = _Trees;
            }
            else
            {
                throw new Exception(string.Format("Unable to match syntax handlers"));
            }
        }

        private List<SyntaxTree.SyntaxTree> parseMain()
        {
            while (!Peek(LexemType.EOF))
            {
                if (!Peek(LexemType.EngKeyword))
                {
                    return null;
                }

                List<SyntaxTree.SyntaxTree> _Trees = new List<SyntaxTree.SyntaxTree>();
                while (Peek(LexemType.EngKeyword))
                {
                    NodeId = default(ObjectId);
                    var _SyntaxTree = new SyntaxTree.SyntaxTree();
                    _SyntaxTree.TreeNodes.Add(Ensure(parseValueNode, "Unable to match syntax handlers"));

                    var parseLexems = Attempt(parseHierarchyLexems);
                    if (parseLexems != null)
                    {
                        _SyntaxTree.TreeNodes.AddRange(parseLexems);
                    }
                    _Trees.Add(_SyntaxTree);
                }
                return _Trees;
            }
            return null;
        }

        private List<SyntaxTreeNode> parseHierarchyLexems()
        {
            if (!Peek(LexemType.NumMomonyumValue, LexemType.NumGrammFuncValue, LexemType.NumLexValue))
            {
                return null;
            }

            var _SyntaxTreeNodes = new List<SyntaxTreeNode>();
            while (Peek(LexemType.NumMomonyumValue, LexemType.NumGrammFuncValue, LexemType.NumLexValue))
            {
                _SyntaxTreeNodes.Add(Ensure(parseValueNode, "Unable to match syntax handlers"));
                var recCall = Attempt(parseHierarchyLexems);
                if (recCall != null)
                {
                    _SyntaxTreeNodes.AddRange(recCall);
                }
            }
            return _SyntaxTreeNodes;
        }

        private SyntaxTreeNode parseValueNode()
        {
            if (!Peek(LexemType.EngKeyword, LexemType.NumMomonyumValue, LexemType.NumGrammFuncValue, LexemType.NumLexValue))
            {
                return null;
            }

            var _SyntaxTreeNode = new SyntaxTreeNode();
            var currentLexem = getCurrentLexem();
            _SyntaxTreeNode.Lexems.Add(currentLexem);
            _SyntaxTreeNode.ParentId = NodeId;

            if (currentLexem.Type == LexemType.EngKeyword ||
                currentLexem.Type == LexemType.NumMomonyumValue ||
                currentLexem.Type == LexemType.NumGrammFuncValue)
            {
                NodeId = _SyntaxTreeNode.Id;
            }

            var valueLexemString = Attempt(parseValueLexems);
            if (valueLexemString != null)
            {
                _SyntaxTreeNode.Lexems.AddRange(valueLexemString);
            }
            return _SyntaxTreeNode;
        }

        private List<Lexem> parseValueLexems()
        {
            if (!Peek(LexemType.Transcription, LexemType.EngAbbreviation, LexemType.RusAbbreviation,
                        LexemType.TranslationReference, LexemType.TranslationMeaning, LexemType.NumSubLexValue))
            {
                return null;
            }
            var ValueLexems = new List<Lexem>();
            while (Peek(LexemType.Transcription, LexemType.EngAbbreviation, LexemType.RusAbbreviation,
                            LexemType.TranslationReference, LexemType.TranslationMeaning, LexemType.NumSubLexValue))
            {
                ValueLexems.Add(getCurrentLexem());
            }
            return ValueLexems;
        }

        //
    }
}
