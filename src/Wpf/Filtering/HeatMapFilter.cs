using M4Graphs.Core.General;
using M4Graphs.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace M4Graphs.Wpf.Filtering
{
    public class HeatMapFilter : IFilter<IDynamicModelElement>
    {
        private bool _hideGreen;
        private bool _hideRed;
        private readonly Filters parent;

        public HeatMapFilter(Filters parent)
        {
            this.parent = parent;
        }

        public void ToggleGreen()
        {
            _hideGreen = !_hideGreen;
            parent.Update();
        }

        public void ToggleRed()
        {
            _hideRed = !_hideRed;
            parent.Update();
        }

        public List<Predicate<IDynamicModelElement>> GetFilters()
        {
            var filters = new List<Predicate<IDynamicModelElement>>();
            if (_hideRed)
                filters.Add(new Predicate<IDynamicModelElement>(target => target.HasErrors));
            if (_hideGreen)
                filters.Add(new Predicate<IDynamicModelElement>(target => target.IsVisited && !target.HasErrors));
            return filters;
        }
    }
}
