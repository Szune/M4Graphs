using System;
using M4Graphs.Core.Elements;
using M4Graphs.Core.General;

namespace M4Graphs.Core.Geometry
{
    /// <summary>
    /// Helper class for collision detection.
    /// </summary>
    public static class Collision
    {
        public static Coordinate GetPointOfEdgeCollision(int targetX, int targetY, int targetWidth, int targetHeight, Coordinate nextLastPoint)
        {
            var targetCenter = new Coordinate(targetX + (targetWidth / 2), targetY + (targetHeight / 2));
            // starting point of the new position
            var collisionPointX = targetCenter.X;
            var collisionPointY = targetCenter.Y;


            var yPrecision = targetHeight / 2;
            var xPrecision = targetWidth / 2;

            // TODO: Rewrite in a way that is easy to understand yet doesn't require instantiating more variables
            var x2 = nextLastPoint.X;
            var y2 = nextLastPoint.Y;
            var x1 = targetCenter.X;
            var y1 = targetCenter.Y;

            var angle = Math.Acos((y2 - y1) / Math.Sqrt(Math.Pow(y2 - y1, 2) + Math.Pow(x2 - x1, 2)));

            var nearbyCathetus = targetHeight / 2;
            var opposingCathetus = nearbyCathetus * Math.Tan(angle);

            if (Math.Abs(targetCenter.Y - nextLastPoint.Y) <= yPrecision)
            {
                // if the last point of the edge is on the same y-level, we don't care about the angle
                // and instead just add or subtract half of the target node's width
                if (targetCenter.X >= nextLastPoint.X)
                    collisionPointX -= xPrecision;
                else
                    collisionPointX += xPrecision;
                return new Coordinate(
                    collisionPointX.Clamp(targetX, targetX + targetWidth),
                    collisionPointY.Clamp(targetY, targetY + targetHeight));
            }


            // if the last point of the edge isn't on the same y-level,
            // aim for the center of the target node
            if (targetCenter.Y >= nextLastPoint.Y)
            {
                collisionPointY -= nearbyCathetus; // node is _below_ the edge's current last point
                collisionPointX += targetCenter.X >= nextLastPoint.X
                    ? opposingCathetus // node is to the _right_ of the edge's current last point
                    : -opposingCathetus; // node is to the _left_ of the edge's current last point
            }
            else
            {
                collisionPointY += nearbyCathetus; // node is _above_ the edge's current last point
                collisionPointX += targetCenter.X >= nextLastPoint.X
                    ? -opposingCathetus // node is to the _right_ of the edge's current last point
                    : opposingCathetus; // node is to the _left_ of the edge's current last point
            }
            return new Coordinate(
                collisionPointX.Clamp(targetX, targetX + targetWidth),
                collisionPointY.Clamp(targetY, targetY + targetHeight));
        }
    }
}
