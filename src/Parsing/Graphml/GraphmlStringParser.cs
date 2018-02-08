using System.Collections.Generic;
using M4Graphs.Parsers.Graphml.EdgeLabels;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using M4Graphs.Core;
using M4Graphs.Core.Elements;
using M4Graphs.Core.Geometry;
using M4Graphs.Parsers.Graphml.Elements;

namespace M4Graphs.Parsers.Graphml
{
    /// <summary>
    /// Helper class for converting from yEd graphml to something usable by M4Graphs.
    /// </summary>
    public static class GraphmlStringParser
    {
        /// <summary>
        /// Namespace "y" in yEd graphml.
        /// </summary>
        public const string NamespaceY = "http://www.yworks.com/xml/graphml";
        /// <summary>
        /// Default namespace in yEd graphml.
        /// </summary>
        public const string NamespaceDefault = "http://graphml.graphdrawing.org/xmlns";

        /// <summary>
        /// Converts yEd graphml to a <see cref="ElementCollection{T, T}"/> usable by M4Graphs.
        /// </summary>
        /// <param name="graphml">A string containing the contents of a graphml file.</param>
        /// <returns></returns>
        public static ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> GetElements(string graphml)
        {
            // convert graphml (xml from yEd) to DrawableElementCollection
            var serializer = new XmlSerializer(typeof(GraphmlRoot), NamespaceDefault);
            GraphmlRoot root;
            using (var reader = new StringReader(graphml))
            {
                root = (GraphmlRoot) serializer.Deserialize(reader);
            }

            var collection = new ElementCollection<GraphmlNodeElement, GraphmlEdgeElement>();

            foreach (var node in root.Graph.Nodes)
                AddNode(collection, node);

            foreach (var edge in root.Graph.Edges)
                AddEdge(collection, edge);

            return collection;
        }

        private static void AddEdge(ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> collection, GraphmlEdge edge)
        {
            foreach (var data in edge.Data)
            {
                if (data?.PolyLineEdge == null) continue;

                if (data.PolyLineEdge != null)
                {
                    var polyLineEdge = GetPolyLineEdge(edge, data.PolyLineEdge);
                    collection.Add(polyLineEdge);
                }
                else
                    throw new UndefinedElementException($"Edge with id '{edge.Id}' is of an unknown type. Known types: PolyLineEdge");
            }
        }

        private static void AddNode(ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> collection, GraphmlNode node)
        {
            foreach (var data in node.Data)
            {
                if (AreAllNull(data?.GenericNode, data?.ShapeNode)) continue;

                if (data?.GenericNode != null)
                {
                    var genericNode = GetGenericNode(node, data.GenericNode);
                    collection.Add(genericNode);
                }
                else if (data?.ShapeNode != null)
                {
                    var shapeNode = GetShapeNode(node, data.ShapeNode);
                    collection.Add(shapeNode);
                }
                else
                    throw new UndefinedElementException($"Node with id '{node.Id}' is of an unknown type. Known types: ShapeNode, GenericNode");
            }
        }

        private static GraphmlEdgeElement GetPolyLineEdge(GraphmlEdge edge, GraphmlPolyLineEdge polyLineEdge)
        {
            var text = polyLineEdge.EdgeLabel?.Text ?? "";
            if(polyLineEdge.EdgeLabel == null || string.IsNullOrWhiteSpace(text))
                return new GraphmlEdgeElement(edge.Id, "", edge.SourceId, edge.TargetId, polyLineEdge.Path.GetPathPoints());

            var label = new EdgeLabel(polyLineEdge.EdgeLabel.X, polyLineEdge.EdgeLabel.Y);
            return new GraphmlEdgeElement(edge.Id, polyLineEdge.EdgeLabel?.Text ?? "", label, edge.SourceId, edge.TargetId, polyLineEdge.Path.GetPathPoints());
        }

        private static GraphmlNodeElement GetShapeNode(GraphmlNode node, GraphmlShapeNode shapeNode)
        {
            return new GraphmlNodeElement(node.Id, shapeNode.NodeLabel?.Text ?? "", shapeNode.Geometry.X, shapeNode.Geometry.Y, shapeNode.Geometry.Width, shapeNode.Geometry.Height);
        }

        private static GraphmlNodeElement GetGenericNode(GraphmlNode node, GraphmlGenericNode genericNode)
        {
            return new GraphmlNodeElement(node.Id, genericNode.NodeLabel?.Text ?? "", genericNode.Geometry.X, genericNode.Geometry.Y, genericNode.Geometry.Width, genericNode.Geometry.Height);
        }

        private static bool AreAllNull(params object[] objects)
        {
            return objects.All(obj => obj == null);
        }
    }
}
