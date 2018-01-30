using System;

namespace M4Graphs.Core.General
{
    /// <summary>
    /// A helper class for enforcing boundaries.
    /// </summary>
    public static class Boundaries
    {
        /// <summary>
        /// Requires <typeparamref name="TType"/> to implement <see cref="IComparable{T}"/>
        /// </summary>
        public static TType Clamp<TType>(this TType actual, TType min, TType max) where TType : IComparable<TType>
        {
            if (actual.CompareTo(max) > 0)
                return max;
            else if (actual.CompareTo(min) < 0)
                return min;
            return actual;
        }
    }
}
