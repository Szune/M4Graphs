using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M4Graphs.Core.General;

namespace M4Graphs.Core.DrawableModelElements
{
    public interface IDrawableNode : IDrawableElement
    {
        double Width { get; }
        double Height { get; }
        double CenterX { get; }
        double CenterY { get; }

        PathPoint Collide(PathPoint nextLastPoint);
    }
}
