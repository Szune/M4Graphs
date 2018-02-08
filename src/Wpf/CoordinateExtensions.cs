using System.Windows;
using M4Graphs.Core.Geometry;

namespace M4Graphs.Wpf
{
    /* The sole reason for this class' existence is so that M4Graphs.Core doesn't have to reference System.Windows (WindowsBase). */
    public static class CoordinateExtensions
    {
        public static Point ToPoint(this Coordinate p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
