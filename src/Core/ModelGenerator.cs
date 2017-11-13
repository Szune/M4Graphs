using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.ModelElements;
using M4Graphs.Core.General;

namespace M4Graphs.Core
{
    /// <summary>
    /// An <see cref="IModel"/> generating the model during runtime.
    /// </summary>
    public class ModelGenerator : IModel
    {
        internal int _xMargin;
        internal int _yMargin;

        internal ModelNode StartNode;
        internal readonly Dictionary<string, ModelNode> Nodes = new Dictionary<string, ModelNode>();
        internal readonly Dictionary<string, ModelEdge> Edges = new Dictionary<string, ModelEdge>();

        /// <summary>
        /// dictionary&lt;y, amount of nodes&gt;
        /// </summary>
        public Dictionary<int, int> NodesAtY = new Dictionary<int, int>();
        /// <summary>
        /// dictionary&lt;Id, amount of edges&gt;
        /// </summary>
        public Dictionary<string, int> EdgesFromNode = new Dictionary<string, int>();

        internal ModelGenerator()
        {

        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="xMargin"></param>
        /// <param name="yMargin"></param>
        public ModelGenerator(int xMargin, int yMargin)
        {
            _xMargin = xMargin;
            _yMargin = yMargin;
        }

        /// <summary>
        /// Sets the margins used when drawing.
        /// </summary>
        /// <param name="xMargin"></param>
        /// <param name="yMargin"></param>
        public void SetMargins(int xMargin, int yMargin)
        {
            _xMargin = xMargin;
            _yMargin = yMargin;
        }

        /// <summary>
        /// Sets the start node.
        /// </summary>
        /// <param name="start"></param>
        public void SetStartNode(ModelNode start)
        {
            Reset(start);
            StartNode = start;
            AddNode(start);
        }

        /// <summary>
        /// Adds another start node.
        /// </summary>
        /// <param name="node"></param>
        public void AddStartNode(ModelNode node)
        {
            node.SetPosition(GetNodesAtY(0), 0);
            AddNode(node);
            if (StartNode == null)
                StartNode = node;
        }

        /// <summary>
        /// Adds an <see cref="IModelElement"/> to the model.
        /// </summary>
        /// <param name="parentId">The identifier of the element's parent.</param>
        /// <param name="element">The element to add.</param>
        public void AddElement(string parentId, IModelElement element)
        {
            if (element is ModelNode node)
                AddElement(parentId, node);
            else if (element is ModelEdge edge)
                AddElement(parentId, edge);
            else
                throw new UnknownElementException($"element is unknown, neither a {nameof(ModelNode)} nor a {nameof(ModelEdge)}");
        }

        /// <summary>
        /// Adds a <see cref="ModelNode"/> as the target of a <see cref="ModelEdge"/>.
        /// </summary>
        /// <param name="parentEdgeId">The parent edge of the node.</param>
        /// <param name="node">The node to add.</param>
        public void AddElement(string parentEdgeId, ModelNode node)
        {
            var parent = Edges[parentEdgeId].SourceNode;
            var yLevel = parent.Position.Y + 1;
            Edges[parentEdgeId].SetTargetNode(node);
            node.SetParentNode(parent);
            node.SetPosition(GetNodesAtY(yLevel), yLevel);
            AddNode(node);
        }

        /// <summary>
        /// Adds a <see cref="ModelEdge"/> to a <see cref="ModelNode"/>.
        /// </summary>
        /// <param name="parentNodeId">The parent node of the edge.</param>
        /// <param name="edge">The edge to add.</param>
        public void AddElement(string parentNodeId, ModelEdge edge)
        {
            var parent = Nodes[parentNodeId];
            edge.SetPosition(GetEdgesFromNode(parentNodeId), parent.Position.Y);
            edge.SetParentNode(parent);
            AddEdge(edge);
        }

        public void ClearElements()
        {
            Nodes.Clear();
            Edges.Clear();
            StartNode = new ModelNode("CLEARED_NODE", "CLEARED_NODE");
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
        /// <returns></returns>
        public DrawableElementCollection GetElements()
        {
            var nodes = Nodes.Select(node => node.Value.ToGeneratedDrawable(_xMargin, _yMargin)).ToDictionary(node => node.Id);
            var edges = Edges.Select(edge => edge.Value.ToGeneratedDrawable(_xMargin, _yMargin)).ToDictionary(edge => edge.Id);

            var collection = new DrawableElementCollection
            {
                Nodes = nodes,
                Edges = edges
            };
            return collection;
        }

        private void AddEdge(ModelEdge edge)
        {
            Edges.Add(edge.Id, edge);
            EdgesFromNode[edge.SourceNode.Id] = GetEdgesFromNode(edge.SourceNode.Id) + 1;
        }


        private void AddNode(ModelNode node)
        {
            Nodes.Add(node.Id, node);
            var width = Measurements.TextToXLevel(node.Text);
            NodesAtY[node.Position.Y] = GetNodesAtY(node.Position.Y) + width;
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

        private void Reset(ModelNode start)
        {
            Nodes.Clear();
            Edges.Clear();
            start.SetPosition(0, 0);
        }
    }
}
