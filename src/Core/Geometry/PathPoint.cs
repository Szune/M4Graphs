using System;
using System.Collections.Generic;

namespace M4Graphs.Core.General
{
    /// <inheritdoc />
    /// <summary>
    /// A class representing a point in a model.
    /// </summary>
    public class PathPoint : IEquatable<PathPoint>
    {
        /// <summary>
        /// Returns a new point with <see cref="X"/> and <see cref="Y"/> values set to zero.
        /// </summary>
        public static PathPoint Zero => new PathPoint(0, 0);
        /// <summary>
        /// The x coordinate of the point.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// The y coordinate of the point.
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public PathPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Sets the position of the point.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PathPoint);
        }

        public virtual bool Equals(PathPoint other)
        {
            const double tolerance = 0.01;
            return other != null &&
                   Math.Abs(X - other.X) < tolerance &&
                   Math.Abs(Y - other.Y) < tolerance;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(PathPoint point1, PathPoint point2)
        {
            return EqualityComparer<PathPoint>.Default.Equals(point1, point2);
        }

        public static bool operator !=(PathPoint point1, PathPoint point2)
        {
            return !(point1 == point2);
        }
    }
}
