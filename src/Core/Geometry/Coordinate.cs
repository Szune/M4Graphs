using System;
using System.Collections.Generic;

namespace M4Graphs.Core.Geometry
{
    /// <inheritdoc />
    /// <summary>
    /// Immutable class representing a point in a model.
    /// </summary>
    public class Coordinate : IEquatable<Coordinate>
    {
        /// <summary>
        /// Returns a new point with <see cref="X"/> and <see cref="Y"/> values set to zero.
        /// </summary>
        public static Coordinate Zero => new Coordinate(0, 0);
        /// <summary>
        /// The x coordinate of the point.
        /// </summary>
        public double X { get; }
        /// <summary>
        /// The y coordinate of the point.
        /// </summary>
        public double Y { get; }
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Coordinate(double x, double y)
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
            return $"{X},{Y}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Coordinate);
        }

        public virtual bool Equals(Coordinate other)
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

        public static bool operator ==(Coordinate point1, Coordinate point2)
        {
            return EqualityComparer<Coordinate>.Default.Equals(point1, point2);
        }

        public static bool operator !=(Coordinate point1, Coordinate point2)
        {
            return !(point1 == point2);
        }
    }
}
