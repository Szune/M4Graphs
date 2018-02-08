using M4Graphs.Core.Elements;
using M4Graphs.Core.Geometry;

namespace M4Graphs.Parsers.Graphml.Elements
{
    public class GraphmlNodeElement : INodeElement
    {

        public GraphmlNodeElement(string id, string text, double x, double y, double width, double height)
        {
            Id = id;
            Text = text;
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public string Id { get; }
        public string Text { get; }
        public double X { get; }
        public double Y { get; }
        public double Width { get; }
        public double Height { get; }
        public double CenterX => X + (Width / 2);
        public double CenterY => Y + (Height / 2);

        public Coordinate GetPointOfEdgeCollision(Coordinate nextLastPoint)
        {
            return Collision.GetPointOfEdgeCollision((int)X, (int)Y, (int)Width, (int)Height, nextLastPoint);
        }

        public GraphmlNodeElement WithOffset(double x, double y)
        {
            return new GraphmlNodeElement(Id, Text, X +  x, Y + y, Width, Height);
        }
    }
}
