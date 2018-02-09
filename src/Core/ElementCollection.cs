using System.Collections.Generic;
using System.Linq;
using M4Graphs.Core.Elements;

namespace M4Graphs.Core
{
    public class ElementCollection<TNode, TEdge> where TNode : INodeElement where TEdge : IEdgeElement
    {
        private readonly Dictionary<string, TNode> _nodes = new Dictionary<string, TNode>();
        private readonly Dictionary<string, TEdge> _edges = new Dictionary<string, TEdge>();
        public int Count => _nodes.Count + _edges.Count;

        public IEnumerable<TNode> Nodes => _nodes.Values;
        public IEnumerable<TEdge> Edges => _edges.Values;

        /// <summary>
        /// Initializes an empty collection.
        /// </summary>
        public ElementCollection()
        {

        }

        public ElementCollection(IEnumerable<TNode> nodes, IEnumerable<TEdge> edges)
        {
            _nodes = nodes.ToDictionary(node => node.Id);
            _edges = edges.ToDictionary(edge => edge.Id);
        }

        public ElementCollection(Dictionary<string, TNode> nodes, Dictionary<string, TEdge> edges)
        {
            _nodes = nodes;
            _edges = edges;
        }

        public void Add(TEdge edge)
        {
            _edges.Add(edge.Id, edge);
        }

        public void Add(TNode node)
        {
            _nodes.Add(node.Id, node);
        }

        public TNode GetNode(string id)
        {
            return _nodes[id];
        }

        public TEdge GetEdge(string id)
        {
            return _edges[id];
        }

        public bool TryGetNode(string id, out TNode node)
        {
            return _nodes.TryGetValue(id, out node);
        }

        public bool TryGetEdge(string id, out TEdge edge)
        {
            return _edges.TryGetValue(id, out edge);
        }

        public void Clear()
        {
            _nodes.Clear();
            _edges.Clear();
        }

        public bool RemoveNode(string id)
        {
            return _nodes.Remove(id);
        }

        public bool RemoveEdge(string id)
        {
            return _edges.Remove(id);
        }
    }
}
