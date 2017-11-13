using M4Graphs.Core.DrawableModelElements;

namespace M4Graphs.Core
{
    /// <summary>
    /// Represents a model.
    /// </summary>
    public interface IModel
    {
        DrawableElementCollection GetElements();
        void ClearElements();
    }
}
