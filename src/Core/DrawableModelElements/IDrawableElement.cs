namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// Represents a model element that is ready for drawing.
    /// </summary>
    public interface IDrawableElement
    {
        /// <summary>
        /// The element's identifier.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// The element's x-coordinate.
        /// </summary>
        double X { get; }
        /// <summary>
        /// The element's y-coordinate.
        /// </summary>
        double Y { get; }
        /// <summary>
        /// The element's text.
        /// </summary>
        string Text { get; }
        /// <summary>
        /// Sets the element's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void SetPosition(double x, double y);
        /// <summary>
        /// Returns a value indicating whether the element was pre-loaded or not.
        /// </summary>
        bool IsLoaded { get; }
    }
}
