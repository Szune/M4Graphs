using System.Collections.Generic;
using System.Linq;
using M4Graphs.Core.Elements;

namespace M4Graphs.Core
{
    public class ElementCollection<TNodeType, TEdgeType> where TNodeType : INodeElement where TEdgeType : IEdgeElement
    {
        private readonly Dictionary<string, TNodeType> _nodes = new Dictionary<string, TNodeType>();
        private readonly Dictionary<string, TEdgeType> _edges = new Dictionary<string, TEdgeType>();
        public int Count => _nodes.Count + _edges.Count;

        public IEnumerable<TNodeType> Nodes => _nodes.Values;
        public IEnumerable<TEdgeType> Edges => _edges.Values;

        /// <summary>
        /// Initializes an empty collection.
        /// </summary>
        public ElementCollection()
        {

        }

        public ElementCollection(IEnumerable<TNodeType> nodes, IEnumerable<TEdgeType> edges)
        {
            _nodes = nodes.ToDictionary(node => node.Id);
            _edges = edges.ToDictionary(edge => edge.Id);
        }

        public ElementCollection(Dictionary<string, TNodeType> nodes, Dictionary<string, TEdgeType> edges)
        {
            _nodes = nodes;
            _edges = edges;
        }

        public void Add(TEdgeType edge)
        {
            _edges.Add(edge.Id, edge);
        }

        public void Add(TNodeType node)
        {
            _nodes.Add(node.Id, node);
        }

        public TNodeType GetNode(string id)
        {
            return _nodes[id];
        }

        public TEdgeType GetEdge(string id)
        {
            return _edges[id];
        }

        public void Clear()
        {
            _nodes.Clear();
            _edges.Clear();
        }

        public void RemoveNode(string id)
        {
            _nodes.Remove(id);
        }

        public void RemoveEdge(string id)
        {
            _edges.Remove(id);
        }
    }
}
