using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.ParserModule.Entities
{
    public struct EntityLocation
    {
        public int Line;

        public int Offset;

        public override string ToString()
        {
            return string.Format("{0}:{1}", Line, Offset);
        }
    }
}
