using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Parser.Entitys
{
    public struct LexemLocation
    {
        public int Line;

        public int Offset;

        public override string ToString()
        {
            return string.Format("{0}:{1}", Line, Offset);
        }
    }
}
