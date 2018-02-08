using System.Collections.Generic;
using System.Linq;
using M4Graphs.Core.Elements;
using M4Graphs.Core.Elements.Labels;
using M4Graphs.Core.Geometry;

namespace M4Graphs.Parsers.Graphml.Elements
{
    public class GraphmlEdgeElement : IEdgeElement
    {
        public GraphmlEdgeElement(string id, string text, string sourceId, string targetId, List<Coordinate> points)
        {
            Id = id;
            Text = text;
            SourceId = sourceId;
            TargetId = targetId;
            Points = points;
        }

        public GraphmlEdgeElement(string id, string text, IEdgeLabel label, string sourceId, string targetId, List<Coordinate> points) : this(id, text, sourceId, targetId, points)
        {
            Label = label;
        }

        public string Id { get; }
        public string Text { get; }
        public List<Coordinate> Points { get; }
        public string SourceId { get; }
        public string TargetId { get; }
        public IEdgeLabel Label { get; }

        public GraphmlEdgeElement WithOffset(double x, double y)
        {
            return new GraphmlEdgeElement(Id, Text, Label, SourceId, TargetId, Points.Select(point => new Coordinate(point.X + x, point.Y + y)).ToList());
        }
    }
}
