using M4Graphs.Core.Elements;

namespace M4Graphs.Core
{
    /// <summary>
    /// Represents a model.
    /// </summary>
    public interface IModel<TNode, TEdge> where TNode : INodeElement where TEdge : IEdgeElement
    {
        ElementCollection<TNode, TEdge> GetElements();
        void ClearElements();
    }
}
