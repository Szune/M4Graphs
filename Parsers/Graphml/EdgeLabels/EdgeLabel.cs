using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;

namespace M4Graphs.Parsers.Graphml.EdgeLabels
{
    public class EdgeLabel : IEdgeLabel
    {
        public double FileX { get; }
        public double FileY { get; }

        public EdgeLabel(double x, double y)
        {
            FileX = x;
            FileY = y;
        }

        /// <summary>
        /// For now, only works with
        /// Placement = "Free: Anywhere", Preferred Placement = "Anywhere Horizontal" on edge labels.
        /// </summary>
        /// <param name="firstEdgePointX"></param>
        /// <param name="firstEdgePointY"></param>
        /// <returns></returns>
        public PathPoint GetActualPosition(double firstEdgePointX, double firstEdgePointY)
        {
            /* For: Placement = "Free: Anywhere", Preferred Placement = "Anywhere Horizontal"
             * Begin at: The first x and y coordinates of the edge's first point,
             * that are outside of the source node.
             * From there: Simply add the x and y coordinates from the graphml (FileX and FileY)
             */

            return new PathPoint(firstEdgePointX + FileX, firstEdgePointY + FileY);
        }
    }
}
