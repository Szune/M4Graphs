using M4Graphs.Core.General;
using M4Graphs.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace M4Graphs.Wpf.Filtering
{
    public class Filters : IFilter<IDynamicModelElement>
    {
        public readonly HeatMapFilter HeatMap;
        private bool _hideVisited;

        public List<Predicate<IDynamicModelElement>> Current { get; private set; }
        public Filters()
        {
            HeatMap = new HeatMapFilter(this);
            Current = new List<Predicate<IDynamicModelElement>>(); // default is to filter nothing
        }

        public List<Predicate<IDynamicModelElement>> GetFilters()
        {
            Update();
            return Current;
        }

        public void ToggleVisited()
        {
            _hideVisited = !_hideVisited;
            Update();
        }

        public void Update()
        {
            Current = HeatMap.GetFilters();
            if(_hideVisited)
                Current.Add(new Predicate<IDynamicModelElement>(target => !target.IsVisited));
        }
    }
}
