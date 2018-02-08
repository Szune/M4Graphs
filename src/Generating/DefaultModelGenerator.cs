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

        public DefaultNodeElement StartNode;
        public readonly Dictionary<string, DefaultNodeElement> Nodes = new Dictionary<string, DefaultNodeElement>();
        public readonly Dictionary<string, DefaultEdgeElement> Edges = new Dictionary<string, DefaultEdgeElement>();
        
        /// <summary>
        /// dictionary&lt;y, amount of nodes&gt;
        /// </summary>
        public Dictionary<int, int> NodesAtY = new Dictionary<int, int>();
        /// <summary>
        /// dictionary&lt;Id, amount of edges&gt;
        /// </summary>
        public Dictionary<string, int> EdgesFromNode = new Dictionary<string, int>();

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
            //node.SetPosition(GetNodesAtY(0), 0);
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
            var parent = Edges[parentEdgeId].SourceNode;
            //var yLevel = parent.Position.Y + 1;
            Edges[parentEdgeId].SetTargetNode(node);
            node.SetParentNode(parent);
            //node.SetPosition(GetNodesAtY(yLevel), yLevel);
            AddNode(node);
        }

        /// <summary>
        /// Adds a <see cref="DefaultEdgeElement"/> to a <see cref="DefaultNodeElement"/>.
        /// </summary>
        /// <param name="parentNodeId">The parent node of the edge.</param>
        /// <param name="edgeElement">The edge to add.</param>
        public void AddElement(string parentNodeId, DefaultEdgeElement edgeElement)
        {
            var parent = Nodes[parentNodeId];
            //edgeElement.SetPosition(GetEdgesFromNode(parentNodeId), parent.Position.Y);
            edgeElement.SetParentNode(parent);
            AddEdge(edgeElement);
        }

        public void ClearElements()
        {
            Nodes.Clear();
            Edges.Clear();
            StartNode = new DefaultNodeElement(CLEARED_NODE, CLEARED_NODE);
        }

        /// <summary>
        /// Connects an edge to an already existing node.
        /// </summary>
        /// <param name="edgeId">The edge's identifier.</param>
        /// <param name="nodeId">The node's identifier.</param>
        public void Connect(string edgeId, string nodeId)
        {
            var node = Nodes[nodeId];
            Edges[edgeId].SetTargetNode(node);
        }

        /// <summary>
        /// Returns the elements generated.
        /// </summary>
        public ElementCollection<DefaultNodeElement, DefaultEdgeElement> GetElements()
        {
            //var nodes = Nodes.Select(node => node.Value.ToGeneratedDrawable(Margins.X, Margins.Y)).ToDictionary(node => node.Id);
            //var edges = Edges.Select(edge => edge.Value.ToGeneratedDrawable(Margins.X, Margins.Y)).ToDictionary(edge => edge.Id);

            var collection = new ElementCollection<DefaultNodeElement, DefaultEdgeElement>(Nodes, Edges);
            return collection;
        }

        private void AddEdge(DefaultEdgeElement edgeElement)
        {
            Edges.Add(edgeElement.Id, edgeElement);
            EdgesFromNode[edgeElement.SourceNode.Id] = GetEdgesFromNode(edgeElement.SourceNode.Id) + 1;
        }


        private void AddNode(DefaultNodeElement node)
        {
            Nodes.Add(node.Id, node);
            var width = Measurements.TextToXLevel(node.Text);
            //NodesAtY[node.Position.Y] = GetNodesAtY(node.Position.Y) + width;
        }



        private int GetEdgesFromNode(string nodeId)
        {
            if (EdgesFromNode.TryGetValue(nodeId, out int count))
            {
                return count;
            }
            return 0;
        }

        private int GetNodesAtY(int y)
        {
            if (NodesAtY.TryGetValue(y, out int count))
            {
                return count;
            }
            return 0;
        }

        private void Reset()
        {
            Nodes.Clear();
            Edges.Clear();
            StartNode = null;
        }
    }
}
