using System;
using M4Graphs.Core.General;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// A node prepared for drawing.
    /// </summary>
    public class DrawableNode : IDrawableNode, IEquatable<IDrawableElement>
    {
        /// <summary>
        /// Returns the x-coordinate of the node.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of the node.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Returns the node's width.
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// Returns the node's height.
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Returns the node's parent node's identifier.
        /// </summary>
        public string ParentNodeId { get; internal set; }

        /// <summary>
        /// Returns a value indicating whether this node was loaded or generated.
        /// </summary>
        public bool IsLoaded { get; }

        /// <summary>
        /// Returns the node's identifier.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Returns the node's text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Returns the x-coordinate of the center of the node.
        /// </summary>
        public double CenterX => (X + (Width / 2));
        /// <summary>
        /// Returns the y-coordinate of the center of the node.
        /// </summary>
        public double CenterY => (Y + (Height / 2));

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        public DrawableNode(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public DrawableNode(string id, string text, double x, double y) : this(id)
        {
            Text = text;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="isLoaded"></param>
        public DrawableNode(string id, string text, double x, double y, double width, double height, bool isLoaded) : this(id, text, x, y)
        {
            Width = width;
            Height = height;
            IsLoaded = isLoaded;
        }

        /// <summary>
        /// Sets the node's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines whether the specified <see cref="IDrawableElement"/> is equal to the current node.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDrawableElement other)
        {
            if (other == null) return false;
            if (!(other is DrawableNode)) return false;
            if (other.Id != Id) return false;
            return true;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is IDrawableElement drawable)
                return Equals(drawable);
            return false;
        }

        /// <summary>
        /// Returns the node's hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Sets the size of the node.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public PathPoint Collide(PathPoint nextLastPoint)
        {
            // TODO: Move this method somewhere else
            // starting point of the new position
            var newX = CenterX;
            var newY = CenterY;


            var yPrecision = (Height / 2);
            var xPrecision = (Width / 2);

            var x2 = nextLastPoint.X;
            var y2 = nextLastPoint.Y;
            var x1 = CenterX;
            var y1 = CenterY;
            
            var vinkel = Math.Acos((y2 - y1) / Math.Sqrt(Math.Pow((y2 - y1), 2) + Math.Pow((x2 - x1), 2)));

            var närliggandeKatet = Height / 2;
            var motståendeKatet = närliggandeKatet * Math.Tan(vinkel);

            if (Math.Abs(CenterY - nextLastPoint.Y) > yPrecision)
            {
                // if the last point of the edge isn't on the same y-level,
                // aim for the center of the target node
                if (CenterY >= nextLastPoint.Y)
                {
                    newY -= närliggandeKatet;
                    if (CenterX >= nextLastPoint.X)
                    {
                        newX += motståendeKatet;
                    }
                    else
                    {
                        newX -= motståendeKatet;
                    }
                }
                else
                {
                    newY += närliggandeKatet;
                    if (CenterX >= nextLastPoint.X)
                    {
                        newX -= motståendeKatet;
                    }
                    else
                    {
                        newX += motståendeKatet;
                    }
                }
            }
            else
            {
                // if the last point of the edge is on the same y-level, we don't care about the angle
                // and instead just add or subtract half of the target node's width
                if (CenterX >= nextLastPoint.X)
                {
                    newX -= xPrecision;
                }
                else
                {
                    newX += xPrecision;
                }
            }
            
            return new PathPoint(newX.Clamp(X, X + Width), newY.Clamp(Y, Y + Height));
        }
    }
}
