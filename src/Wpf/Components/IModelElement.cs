﻿using M4Graphs.Core.General;
using System;
using System.Collections.Generic;
using M4Graphs.Core.Geometry;
using M4Graphs.Core.Interfaces;

namespace M4Graphs.Wpf.Components
{
    /// <summary>
    /// Represents an element in a model.
    /// </summary>
    public interface IModelElement : IActivatable
    {
        /// <summary>
        /// The element's identifier.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// The element's position.
        /// </summary>
        Coordinate Position { get; }

        /// <summary>
        /// Updates the element's colorized heat.
        /// </summary>
        /// <param name="heat"></param>
        void UpdateHeat(double heat);
        /// <summary>
        /// Adds an error to the element.
        /// </summary>
        /// <param name="error"></param>
        void AddError(ExecutingElementMethodError error);
        /// <summary>
        /// A value indicating whether the element has any errors.
        /// </summary>
        bool HasErrors { get; }
        /// <summary>
        /// A value indicating whether the element has been visited.
        /// </summary>
        bool IsVisited { get; }

        /// <summary>
        /// Filters the element.
        /// </summary>
        /// <param name="filter"></param>
        void Filter(List<Predicate<IModelElement>> filter);

        ElementStates States { get; }
    }
}
