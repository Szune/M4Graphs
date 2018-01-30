using System;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// A class representing a point in a model.
    /// </summary>
    public class PathPoint : IComparable<PathPoint>
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
        /// Compares the point to another point.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PathPoint other)
        {
            // TODO: change this implementation?
            if (X > other.X && Y > other.Y)
                return 1;
            else if (X == other.X && Y == other.Y)
                return 0;
            else
                return -1;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
