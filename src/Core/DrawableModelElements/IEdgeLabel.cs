using M4Graphs.Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core.DrawableModelElements
{
    public interface IEdgeLabel
    {
        PathPoint GetActualPosition(double x, double y);
    }
}
