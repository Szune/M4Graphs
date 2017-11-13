using M4Graphs.Core.Converters.Graphml.EdgeLabels;
using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace M4Graphs.Core.Converters.Graphml
{
    /// <summary>
    /// Helper class for converting from yEd graphml to something usable by DGML.
    /// </summary>
    public static class Graphml
    {
        /// <summary>
        /// Namespace "y" in yEd graphml.
        /// </summary>
        public const string Namespace_y = "http://www.yworks.com/xml/graphml";
        /// <summary>
        /// Default namespace in yEd graphml.
        /// </summary>
        public const string NamespaceDefault = "http://graphml.graphdrawing.org/xmlns";

        /// <summary>
        /// Converts yEd graphml to a <see cref="DrawableElementCollection"/> usable by DGML.
        /// </summary>
        /// <param name="graphml">A string containing the contents of a graphml file.</param>
        /// <param name="offsetX">Amount of x-pixels to nudge the entire model by.</param>
        /// <param name="offsetY">Amount of y-pixels to nudge the entire model by.</param>
        /// <returns></returns>
        public static DrawableElementCollection ToDrawableElementCollection(string graphml, int offsetX = 0, int offsetY = 0)
        {
            // convert graphml (xml from yEd) to DrawableElementCollection
            var serializer = new XmlSerializer(typeof(GraphmlRoot), NamespaceDefault);
            GraphmlRoot root;
            using (var reader = new StringReader(graphml))
            {
                root = (GraphmlRoot)serializer.Deserialize(reader);
            }

            var collection = new DrawableElementCollection();

            foreach(var node in root.Graph.Nodes)
            {
                foreach(var data in node.Data)
                {
                    if (AreAllNull(data?.GenericNode, data?.PolyLineEdge, data?.ShapeNode)) continue;

                    if (data.GenericNode != null)
                        collection.Add(GetGenericNode(node, data.GenericNode));
                    else if (data.ShapeNode != null)
                        collection.Add(GetShapeNode(node, data.ShapeNode));
                    else
                        throw new GraphmlElementFormatException($"Node with id '{node.Id}' is of an unknown type. Known types: ShapeNode, GenericNode");
                }
            }

            foreach(var edge in root.Graph.Edges)
            {
                foreach (var data in edge.Data)
                {
                    if (AreAllNull(data?.GenericNode, data?.PolyLineEdge, data?.ShapeNode)) continue;

                    if (data.PolyLineEdge != null)
                        collection.Add(GetPolyLineEdge(edge, data.PolyLineEdge));
                    else
                        throw new GraphmlElementFormatException($"Edge with id '{edge.Id}' is of an unknown type. Known types: PolyLineEdge");
                }
            }

            var lowestX = -collection.Nodes.Min(e => e.Value.X); // let's try to pull everything to the left border
            var lowestY = -collection.Nodes.Min(e => e.Value.Y);

            // by adding the negative of lowest x and y to every node, we move everything closer to the left border
            // and onto the screen
            // x1 = -20 -> lowestX = 20, -20 + 20 = 0
            // x2 = 0 -> lowestX = 20, 0 + 20 = 20
            // so everything should move proportionally, right?

            foreach (var node in collection.Nodes)
                node.Value.SetPosition(node.Value.X + lowestX + offsetX, node.Value.Y + lowestY + offsetY);

            foreach (var edge in collection.Edges)
            {
                edge.Value.SetPosition(edge.Value.X + lowestX + offsetX, edge.Value.Y + lowestY + offsetY);
                foreach(var point in edge.Value.Points)
                {
                    point.SetPosition(point.X + lowestX + offsetX, point.Y + lowestY + offsetY);
                }
            }

            return collection;
        }

        private static DrawableEdge GetPolyLineEdge(GraphmlEdge edge, GraphmlPolyLineEdge polyLineEdge)
        {
            var text = polyLineEdge.EdgeLabel?.Text ?? "";
            if(string.IsNullOrWhiteSpace(text))
                return new DrawableEdge(edge.Id, "", edge.SourceId, edge.TargetId, polyLineEdge.Path.GetPathPoints(), true);

            var label = new EdgeLabel(polyLineEdge.EdgeLabel.X, polyLineEdge.EdgeLabel.Y);

            return new DrawableEdge(edge.Id, polyLineEdge.EdgeLabel?.Text ?? "", label, edge.SourceId, edge.TargetId, polyLineEdge.Path.GetPathPoints(), true);
        }

        private static DrawableNode GetShapeNode(GraphmlNode node, GraphmlShapeNode shapeNode)
        {
            return new DrawableNode(node.Id, shapeNode.NodeLabel?.Text ?? "", shapeNode.Geometry.X, shapeNode.Geometry.Y, shapeNode.Geometry.Width, shapeNode.Geometry.Height, true);
        }

        private static DrawableNode GetGenericNode(GraphmlNode node, GraphmlGenericNode genericNode)
        {
            return new DrawableNode(node.Id, genericNode.NodeLabel?.Text ?? "", genericNode.Geometry.X, genericNode.Geometry.Y, genericNode.Geometry.Width, genericNode.Geometry.Height, true);
        }

        private static bool AreAllNull(params object[] objects)
        {
            foreach (var obj in objects)
                if (obj != null) return false;
            return true;
        }
    }
}
