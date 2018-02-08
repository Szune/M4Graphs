using System;
using M4Graphs.Core.Geometry;

namespace M4Graphs.Core.Elements.Labels
{
    public class DefaultNodeLabel : INodeLabel
    {
        private Coordinate _cachedCoordinate;
        public Coordinate GetViewPosition(double x, double y)
        {
            const double tolerance = 0.001;
            if (_cachedCoordinate != null && Math.Abs(x - _cachedCoordinate.X) < tolerance && Math.Abs(y - _cachedCoordinate.Y) < tolerance)
                return _cachedCoordinate;
            _cachedCoordinate = new Coordinate(x, y);
            return _cachedCoordinate;
        }
    }
}
