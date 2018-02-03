using M4Graphs.Core.General;
using System.Windows;

namespace M4Graphs.Wpf
{
    /* The sole reason for this class' existence is so that M4Graphs.Core doesn't have to reference System.Windows (WindowsBase). */
    public static class PathPointExtensions
    {
        public static Point ToPoint(this PathPoint p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
