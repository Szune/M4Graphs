using M4Graphs.Core.Elements;
using M4Graphs.Core.General;

namespace M4Graphs.Core.Interfaces
{
    /// <summary>
    /// An interface representing a dynamic graph model implementation.
    /// </summary>
    public interface IDynamicGraphModel<out TModelType> where TModelType : IDynamicGraphModel<TModelType>
    {
        /// <summary>
        /// Resets the dynamic graph model.
        /// </summary>
        void Reset();
        /// <summary>
        /// Refreshes the dynamic graph model, without touching unchanged elements.
        /// </summary>
        void Refresh();
        /// <summary>
        /// Draws the dynamic graph model with the help of an element collection.
        /// </summary>
        /// <param name="renderer">The renderer to use when drawing.</param>
        void Draw(IRenderer<TModelType> renderer);
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
