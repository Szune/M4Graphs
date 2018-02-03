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
            var newX = collidingNode.CenterX;
            var newY = collidingNode.CenterY;


            var yPrecision = (collidingNode.Height / 2);
            var xPrecision = (collidingNode.Width / 2);

            var x2 = nextLastPoint.X;
            var y2 = nextLastPoint.Y;
            var x1 = collidingNode.CenterX;
            var y1 = collidingNode.CenterY;

            var angle = Math.Acos((y2 - y1) / Math.Sqrt(Math.Pow((y2 - y1), 2) + Math.Pow((x2 - x1), 2)));

            var nearbyCathetus = collidingNode.Height / 2;
            var opposingCathetus = nearbyCathetus * Math.Tan(angle);

            if (Math.Abs(collidingNode.CenterY - nextLastPoint.Y) > yPrecision)
            {
                // if the last point of the edge isn't on the same y-level,
                // aim for the center of the target node
                if (collidingNode.CenterY >= nextLastPoint.Y)
                {
                    newY -= nearbyCathetus;
                    if (collidingNode.CenterX >= nextLastPoint.X)
                    {
                        newX += opposingCathetus;
                    }
                    else
                    {
                        newX -= opposingCathetus;
                    }
                }
                else
                {
                    newY += nearbyCathetus;
                    if (collidingNode.CenterX >= nextLastPoint.X)
                    {
                        newX -= opposingCathetus;
                    }
                    else
                    {
                        newX += opposingCathetus;
                    }
                }
            }
            else
            {
                // if the last point of the edge is on the same y-level, we don't care about the angle
                // and instead just add or subtract half of the target node's width
                if (collidingNode.CenterX >= nextLastPoint.X)
                {
                    newX -= xPrecision;
                }
                else
                {
                    newX += xPrecision;
                }
            }

            return new PathPoint(newX.Clamp(collidingNode.X, collidingNode.X + collidingNode.Width), newY.Clamp(collidingNode.Y, collidingNode.Y + collidingNode.Height));
        }
    }
}
