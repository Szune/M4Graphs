using M4Graphs.Core;
using M4Graphs.Core.General;
using System.Collections.Generic;
using M4Graphs.Core.Elements;

namespace M4Graphs.Generators
{
    /// <summary>
    /// An <see cref="IModel"/> generating the model during runtime.
    /// </summary>
    public class DefaultModelGenerator : IModel<DefaultNodeElement, DefaultEdgeElement>, IModelGenerator
    {
        private const string CLEARED_NODE = "CLEARED_NODE";
        public Margin Margins { get; private set; }

        public DefaultNodeElement StartNode { get; private set; }
        private readonly ElementCollection<DefaultNodeElement, DefaultEdgeElement> _elements = new ElementCollection<DefaultNodeElement, DefaultEdgeElement>();
        
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DefaultModelGenerator()
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DefaultModelGenerator(int xMargin, int yMargin)
        {
            Margins = new Margin(xMargin, yMargin);
        }

        /// <summary>
        /// Sets the margins used when drawing.
        /// </summary>
        public void SetMargins(int xMargin, int yMargin)
        {
            Margins = new Margin(xMargin, yMargin);
        }

        /// <summary>
        /// Sets the start node.
        /// </summary>
        public void SetStartNode(DefaultNodeElement start)
        {
            Reset();
            StartNode = start;
            AddNode(start);
        }

        /// <summary>
        /// Adds another start node.
        /// </summary>
        public void AddStartNode(DefaultNodeElement node)
        {
            AddNode(node);
            if (StartNode == null)
                StartNode = node;
        }

        /// <summary>
        /// Adds a <see cref="DefaultNodeElement"/> as the target of a <see cref="DefaultEdgeElement"/>.
        /// </summary>
        /// <param name="parentEdgeId">The parent edge of the node.</param>
        /// <param name="node">The node to add.</param>
        public void AddElement(string parentEdgeId, DefaultNodeElement node)
        {
            var parent = _elements.GetEdge(parentEdgeId).SourceNode;
            _elements.GetEdge(parentEdgeId).SetTargetNode(node);
            node.SetParentNode(parent);
            AddNode(node);
        }

        /// <summary>
        /// Adds a <see cref="DefaultEdgeElement"/> to a <see cref="DefaultNodeElement"/>.
        /// </summary>
        /// <param name="parentNodeId">The parent node of the edge.</param>
        /// <param name="edgeElement">The edge to add.</param>
        public void AddElement(string parentNodeId, DefaultEdgeElement edgeElement)
        {
            var parent = _elements.GetNode(parentNodeId);
            edgeElement.SetParentNode(parent);
            AddEdge(edgeElement);
        }

        public void ClearElements()
        {
            _elements.Clear();
            StartNode = new DefaultNodeElement(CLEARED_NODE, CLEARED_NODE);
        }

        /// <summary>
        /// Connects an edge to an already existing node.
        /// </summary>
        /// <param name="edgeId">The edge's identifier.</param>
        /// <param name="nodeId">The node's identifier.</param>
        public void Connect(string edgeId, string nodeId)
        {
            var node = _elements.GetNode(nodeId);
            _elements.GetEdge(edgeId).SetTargetNode(node);
        }

        /// <summary>
        /// Returns the elements generated.
        /// </summary>
        public ElementCollection<DefaultNodeElement, DefaultEdgeElement> GetElements()
        {
            var collection = new ElementCollection<DefaultNodeElement, DefaultEdgeElement>(_elements.Nodes, _elements.Edges);
            return collection;
        }

        private void AddEdge(DefaultEdgeElement edgeElement)
        {
            _elements.Add(edgeElement);
        }


        private void AddNode(DefaultNodeElement node)
        {
            _elements.Add(node);
        }

        private void Reset()
        {
            _elements.Clear();
            StartNode = null;
        }
    }
}
