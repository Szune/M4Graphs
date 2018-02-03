/*
 * Slight modification of http://shrinandvyas.blogspot.se/2013/01/wpf-drag-drop-how-to-detect-when-drag.html
 */

using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace M4Graphs.Wpf
{
    /// <summary>
    /// A helper class for doing mouse drag and drop.
    /// </summary>
    public static class MouseDrag
    {
        private static bool _isMoving;
        private static Point _dragBeginPoint;
        private static Point _lastMovePoint;

        public static bool Begin(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 1) return false;
            _isMoving = true;
            _dragBeginPoint = e.GetPosition(e.Source as FrameworkElement);
            _lastMovePoint = e.GetPosition(e.Source as FrameworkElement);
            return true;
        }

        public static MouseDragFinishedEventArgs End(object sender, MouseButtonEventArgs e)
        {
            if (!_isMoving) return MouseDragFinishedEventArgs.NoMovement;
            _isMoving = false;
            var dragEnd = e.GetPosition(e.Source as FrameworkElement);
            var change = new Point(dragEnd.X - _dragBeginPoint.X, dragEnd.Y - _dragBeginPoint.Y);
            var delta = new Point(Math.Abs(dragEnd.X - _dragBeginPoint.X), Math.Abs(dragEnd.Y - _dragBeginPoint.Y));
            Mouse.Capture(null);
            return new MouseDragFinishedEventArgs(_dragBeginPoint, dragEnd, delta, change);
        }

        public static MouseDragMovingEventArgs Move(object sender, MouseEventArgs e)
        {
            if (!_isMoving) return MouseDragMovingEventArgs.NotMoving;
            if (!ReferenceEquals(null, FindAncestor<ScrollBar>((DependencyObject)e.OriginalSource))) return MouseDragMovingEventArgs.NotMoving;

            var current = e.GetPosition(e.Source as FrameworkElement);
            if (!IsDragGesture(_dragBeginPoint,current)) return MouseDragMovingEventArgs.NotMoving;

            Mouse.Capture(sender as UIElement);
            var change = new Point(current.X - _lastMovePoint.X, current.Y - _lastMovePoint.Y);
            _lastMovePoint = current;
            var delta = new Point(Math.Abs(current.X - _dragBeginPoint.X), Math.Abs(current.Y - _dragBeginPoint.Y));
            var retVal = new MouseDragMovingEventArgs(_dragBeginPoint, current, delta, change);

            return retVal;
        }

        private static bool IsDragGesture(Point start, Point end)
        {
            var horizontalMove = Math.Abs(end.X - start.X);
            var verticalMove = Math.Abs(end.Y - start.Y);
            var hGesture = horizontalMove > SystemParameters.MinimumHorizontalDragDistance;
            var vGesture = verticalMove > SystemParameters.MinimumVerticalDragDistance;
            return (hGesture || vGesture);
        }

        private static T FindAncestor<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);
            if (parent == null) return null;
            var parentT = parent as T;
            return parentT ?? FindAncestor<T>(parent);
        }
    }

    public struct MouseDragFinishedEventArgs
    {
        public Point Start { get; }
        public Point End { get; }
        public Point Delta { get; }
        public Point Change { get; }
        public bool IsMoved { get; }

        private MouseDragFinishedEventArgs(bool isMoved, Point start, Point end, Point delta, Point change)
        {
            Start = start;
            End = end;
            Delta = delta;
            Change = change;
            IsMoved = isMoved;
        }

        public MouseDragFinishedEventArgs(Point start, Point end, Point delta, Point change) : this(true, start, end, delta, change)
        {
        }

        public static MouseDragFinishedEventArgs NoMovement => new MouseDragFinishedEventArgs(false, new Point(0, 0),
            new Point(0, 0), new Point(0, 0), new Point(0, 0));
    }

    public struct MouseDragMovingEventArgs
    {
        public Point Start { get; }
        public Point Current { get; }
        public Point Delta { get; }
        public Point Change { get; }
        public bool IsMoving { get; }

        private MouseDragMovingEventArgs(bool isMoving, Point start, Point current, Point delta, Point change)
        {
            IsMoving = isMoving;
            Start = start;
            Current = current;
            Delta = delta;
            Change = change;
        }

        public MouseDragMovingEventArgs(Point start, Point current, Point delta, Point change) : this(true, start,
            current, delta, change)
        {            
        }

        public static MouseDragMovingEventArgs NotMoving => new MouseDragMovingEventArgs(false,new Point(0,0), new Point(0,0), new Point(0,0), new Point(0,0));
    }
}
