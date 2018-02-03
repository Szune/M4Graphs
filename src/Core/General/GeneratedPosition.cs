using System;
using System.Collections.Generic;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// Class representing a generated position.
    /// </summary>
    public class GeneratedPosition : IEquatable<GeneratedPosition>
    {
        /// <summary>
        /// Returns the x-coordinate.
        /// </summary>
        public int X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate.
        /// </summary>
        public int Y { get; private set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public GeneratedPosition()
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public GeneratedPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GeneratedPosition);
        }

        public virtual bool Equals(GeneratedPosition other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(GeneratedPosition position1, GeneratedPosition position2)
        {
            return EqualityComparer<GeneratedPosition>.Default.Equals(position1, position2);
        }

        public static bool operator !=(GeneratedPosition position1, GeneratedPosition position2)
        {
            return !(position1 == position2);
        }
    }
}
