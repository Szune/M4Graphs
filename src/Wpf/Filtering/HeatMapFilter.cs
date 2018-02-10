using System;
using System.Collections.Generic;
using M4Graphs.Core.General;
using M4Graphs.Wpf.Components;

namespace M4Graphs.Wpf.Filtering
{
    public class HeatMapFilter : IFilter<IModelElement>
    {
        private bool _hideGreen;
        private bool _hideRed;
        private readonly Filters _parent;

        public HeatMapFilter(Filters parent)
        {
            _parent = parent;
        }

        public void ToggleGreen()
        {
            _hideGreen = !_hideGreen;
            _parent.Update();
        }

        public void ToggleRed()
        {
            _hideRed = !_hideRed;
            _parent.Update();
        }

        public List<Predicate<IModelElement>> GetFilters()
        {
            var filters = new List<Predicate<IModelElement>>();
            if (_hideRed)
                filters.Add(target => target.HasErrors);
            if (_hideGreen)
                filters.Add(target => target.IsVisited && !target.HasErrors);
            return filters;
        }
    }
}
