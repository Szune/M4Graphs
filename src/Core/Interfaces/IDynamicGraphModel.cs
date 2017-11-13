using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;

namespace M4Graphs.Core.Interfaces
{
    /// <summary>
    /// An interface representing a dynamic graph model implementation.
    /// </summary>
    public interface IDynamicGraphModel
    {
        /// <summary>
        /// Resets the dynamic graph model.
        /// </summary>
        void Reset();
        /// <summary>
        /// Sets the associated model, used for getting elements to draw.
        /// </summary>
        /// <param name="model"></param>
        void Set(IModel model);
        /// <summary>
        /// Refreshes the dynamic graph model, without touching unchanged elements.
        /// </summary>
        void Refresh();
        /// <summary>
        /// Draws the dynamic graph model with the help of the associated model.
        /// </summary>
        void Draw();
        /// <summary>
        /// Draws the dynamic graph model with the help of a <see cref="DrawableElementCollection"/>.
        /// </summary>
        /// <param name="collection">The collection of elements to draw.</param>
        void Draw(DrawableElementCollection collection);
        /// <summary>
        /// Activates the specified element, deactivating the last activated element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        void ActivateElement(string id);
        /// <summary>
        /// Adds a generic error to the specified element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        void AddElementError(string id);
        /// <summary>
        /// Adds an error to the specified element element.
        /// </summary>
        /// <param name="id">The element's identifier.</param>
        /// <param name="error">The associated error.</param>
        void AddElementError(string id, ExecutingElementMethodError error);
    }
}
