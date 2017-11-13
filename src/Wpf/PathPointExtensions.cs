using M4Graphs.Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace M4Graphs.Wpf
{
    public static class PathPointExtensions
    {
        public static Point ToPoint(this PathPoint p)
        {
            return new Point(p.X, p.Y);
        }
    }
}
