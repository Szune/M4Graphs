using M4Graphs.Core.General;

namespace M4Graphs.Core.DrawableModelElements
{
    public interface IEdgeLabel
    {
        PathPoint GetActualPosition(double x, double y);
    }
}
