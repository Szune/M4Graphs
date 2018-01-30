using M4Graphs.Core.General;
using System.Collections.Generic;

namespace M4Graphs.Core.DrawableModelElements
{
    public interface IDrawableEdge : IDrawableElement
    {
        List<PathPoint> Points { get; }
        string TargetId { get; }
        string SourceId { get; }
        IDrawableNode TargetNode { get; }
        IDrawableNode SourceNode { get; }
        IEdgeLabel Label { get; }
    }
}
