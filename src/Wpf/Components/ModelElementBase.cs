using M4Graphs.Core.General;
using M4Graphs.Core.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace M4Graphs.Wpf.Components
{
    public abstract class ModelElementBase : UserControl, IModelElement
    {
        public abstract void Activate();
        public abstract void Deactivate();
        public abstract string Id { get; }
        public abstract Coordinate Position { get; set; }
        public abstract void UpdateHeat(double heat);
        public abstract void AddError(ExecutingElementMethodError error);
        public abstract bool HasErrors { get; }
        public abstract bool IsVisited { get; protected set; }
        public abstract void Filter(List<Predicate<IModelElement>> filter);
        public abstract ElementStates States { get; protected set; }
    }
}
