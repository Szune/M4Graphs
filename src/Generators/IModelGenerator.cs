using M4Graphs.Core.ModelElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Generators
{
    public interface IModelGenerator
    {
        void SetStartNode(ModelNode start);
        void SetMargins(int xMargin, int yMargin);
    }
}
