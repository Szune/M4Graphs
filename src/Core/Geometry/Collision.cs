using System;
using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;

namespace M4Graphs.Core.Geometry
{
    /// <summary>
    /// Helper class for collision detection.
    /// </summary>
    public static class Collision
    {
        public static PathPoint GetPointOfEdgeCollision(DrawableNode collidingNode, PathPoint nextLastPoint)
        {
            // starting point of the new position
            var collisionPointX = collidingNode.CenterX;
            var collisionPointY = collidingNode.CenterY;


            var yPrecision = (collidingNode.Height / 2);
            var xPrecision = (collidingNode.Width / 2);

            var x2 = nextLastPoint.X;
            var y2 = nextLastPoint.Y;
            var x1 = collidingNode.CenterX;
            var y1 = collidingNode.CenterY;

            var angle = Math.Acos((y2 - y1) / Math.Sqrt(Math.Pow((y2 - y1), 2) + Math.Pow((x2 - x1), 2)));

            var nearbyCathetus = collidingNode.Height / 2;
            var opposingCathetus = nearbyCathetus * Math.Tan(angle);

            if (Math.Abs(collidingNode.CenterY - nextLastPoint.Y) <= yPrecision)
            {
                // if the last point of the edge is on the same y-level, we don't care about the angle
                // and instead just add or subtract half of the target node's width
                if (collidingNode.CenterX >= nextLastPoint.X)
                    collisionPointX -= xPrecision;
                else
                    collisionPointX += xPrecision;
                return new PathPoint(
                    collisionPointX.Clamp(collidingNode.X, collidingNode.X + collidingNode.Width),
                    collisionPointY.Clamp(collidingNode.Y, collidingNode.Y + collidingNode.Height));
            }


            // if the last point of the edge isn't on the same y-level,
            // aim for the center of the target node
            if (collidingNode.CenterY >= nextLastPoint.Y)
            {
                collisionPointY -= nearbyCathetus; // node is _below_ the edge's current last point
                collisionPointX += collidingNode.CenterX >= nextLastPoint.X
                    ? opposingCathetus // node is to the _right_ of the edge's current last point
                    : -opposingCathetus; // node is to the _left_ of the edge's current last point
            }
            else
            {
                collisionPointY += nearbyCathetus; // node is _above_ the edge's current last point
                collisionPointX += collidingNode.CenterX >= nextLastPoint.X
                    ? -opposingCathetus // node is to the _right_ of the edge's current last point
                    : opposingCathetus; // node is to the _left_ of the edge's current last point
            }
            return new PathPoint(
                collisionPointX.Clamp(collidingNode.X, collidingNode.X + collidingNode.Width),
                collisionPointY.Clamp(collidingNode.Y, collidingNode.Y + collidingNode.Height));
        }
    }
}
