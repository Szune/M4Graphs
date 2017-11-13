using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Dictionary<string, double> _elementHits = new Dictionary<string, double>();
        private double _hitsTotal;

        /// <summary>
        /// Returns a list of element id:s that have been reached so far.
        /// </summary>
        public List<string> Elements => _elementHits.Select(kvp => kvp.Key).ToList();

        /// <summary>
        /// Returns the specified element's current heat.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        /// <returns></returns>
        public double GetHeat(string id)
        {
            if (!_elementHits.ContainsKey(id)) throw new KeyNotFoundException($"The element '{id}' has not yet been added to the heat map.");
            return (_elementHits[id] / _hitsTotal);
        }

        /// <summary>
        /// Adds heat to the specified element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        public void AddHeat(string id)
        {
            if (_elementHits.ContainsKey(id))
                _elementHits[id] += 1;
            else
                _elementHits[id] = 1;
            _hitsTotal++;
        }

        /// <summary>
        /// Resets the heat map.
        /// </summary>
        public void Reset()
        {
            _elementHits.Clear();
            _hitsTotal = 0;
        }
    }
}
