using M4Graphs.Core.General;
using System;
using System.Collections.Generic;
using M4Graphs.Wpf.Components;

namespace M4Graphs.Wpf.Filtering
{
    public class Filters : IFilter<IModelElement>
    {
        public readonly HeatMapFilter HeatMap;
        private bool _hideVisited;

        public List<Predicate<IModelElement>> Current { get; private set; }
        public Filters()
        {
            HeatMap = new HeatMapFilter(this);
            Current = new List<Predicate<IModelElement>>(); // default is to filter nothing
        }

        public List<Predicate<IModelElement>> GetFilters()
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
                Current.Add((target => !target.IsVisited));
        }
    }
}
