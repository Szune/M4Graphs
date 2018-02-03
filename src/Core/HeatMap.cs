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
        private double _hitsTotal;

        /// <summary>
        /// Returns a list of element ids that have been visited so far.
        /// </summary>
        public List<string> GetElements()
        {
            return ElementHits.Select(kvp => kvp.Key).ToList();
        }

        /// <summary>
        /// Returns the specified element's current heat.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        /// <returns></returns>
        public double GetHeat(string id)
        {
            if (!ElementHits.ContainsKey(id)) throw new KeyNotFoundException($"The element '{id}' has not yet been added to the heat map.");
            return (ElementHits[id] / _hitsTotal);
        }

        /// <summary>
        /// Adds heat to the specified element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        public void AddHeat(string id)
        {
            if (ElementHits.ContainsKey(id))
                ElementHits[id] += 1;
            else
                ElementHits[id] = 1;
            _hitsTotal++;
        }

        /// <summary>
        /// Resets the heat map.
        /// </summary>
        public void Reset()
        {
            ElementHits.Clear();
            _hitsTotal = 0;
        }
    }
}
