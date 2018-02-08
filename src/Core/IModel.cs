using M4Graphs.Core.Elements;

namespace M4Graphs.Core
{
    /// <summary>
    /// Represents a model.
    /// </summary>
    public interface IModel<TNodeType, TEdgeType> where TNodeType : INodeElement where TEdgeType : IEdgeElement
    {
        ElementCollection<TNodeType, TEdgeType> GetElements();
        void ClearElements();
    }
}
