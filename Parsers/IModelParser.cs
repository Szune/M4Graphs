using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Parsers
{
    public interface IModelParser
    {
        void SetOffset(int xOffset, int yOffset);
        void SetFilePath(string filePath);
        void SetModelString(string modelString);
        void DisableFileCaching();
    }
}
