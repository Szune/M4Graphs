using System;
using System.Collections.Generic;
using System.Linq;

namespace M4Graphs.Core
{
    /// <summary>
    /// A heat map.
    /// </summary>
    public class HeatMap
    {
        /// <summary>
        /// key -> string -> elementId, value -> long -> amount of hits
        /// </summary>
        private Dictionary<string, double> ElementHits { get; } = new Dictionary<string, double>();
        private double _totalHits;

        /// <summary>
        /// Returns a list of element ids that have been visited so far.
        /// </summary>
        public IEnumerable<string> GetVisitedElements()
        {
            return ElementHits.Keys;
        }

        /// <summary>
        /// Returns the specified element's current heat.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        /// <returns></returns>
        public double GetHeat(string id)
        {
            if (!ElementHits.ContainsKey(id)) throw new KeyNotFoundException($"The element '{id}' has not yet been added to the heat map.");
            return ElementHits[id] / _totalHits;
        }

        public bool TryGetHeat(string id, out double heat)
        {
            if (ElementHits.TryGetValue(id, out var hits))
            {
                heat = hits / _totalHits;
                return true;
            }
            heat = default(double);
            return false;
        }

        /// <summary>
        /// Adds heat to the specified element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        public void AddHeat(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (ElementHits.ContainsKey(id))
                ElementHits[id] += 1;
            else
                ElementHits[id] = 1;
            _totalHits++;
        }

        /// <summary>
        /// Resets the heat map.
        /// </summary>
        public void Reset()
        {
            ElementHits.Clear();
            _totalHits = 0;
        }
    }
}
