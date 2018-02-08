using M4Graphs.Core.Geometry;

namespace M4Graphs.Core.Elements.Labels
{
    public interface INodeLabel
    {
        Coordinate GetViewPosition(double x, double y);
    }
}
