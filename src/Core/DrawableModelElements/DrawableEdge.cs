using M4Graphs.Core.General;
using System;
using System.Collections.Generic;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// An edge prepared for drawing.
    /// </summary>
    public class DrawableEdge : IDrawableEdge, IEquatable<DrawableEdge>
    {
        /// <summary>
        /// Returns the x-coordinate of the edge.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of the edge.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Returns the edge's source node, used for determining the start of the edge.
        /// </summary>
        public IDrawableNode SourceNode { get; private set; }

        /// <summary>
        /// Returns the edge's target, used for determining the end of the edge.
        /// </summary>
        public IDrawableNode TargetNode { get; private set; }

        /// <summary>
        /// Returns the edge's identifier.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Returns the edge's text.
        /// </summary>
        public string Text { get; }
        /// <summary>
        /// Returns the points in between the source and target node.
        /// </summary>
        public List<PathPoint> Points { get; set; } = new List<PathPoint>();
        /// <summary>
        /// Returns the location of the edge's label, where its text is drawn.
        /// </summary>
        public PathPoint LabelPoint { get; set; }

        /// <summary>
        /// Returns a value indicating whether this edge was loaded or generated.
        /// </summary>
        public bool IsLoaded { get; }

        /// <summary>
        /// Returns the edge's target's identifier.
        /// </summary>
        public string TargetId { get; }
        /// <summary>
        /// Returns the edge's source's identifier
        /// </summary>
        public string SourceId { get; }

        public IEdgeLabel Label { get; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableEdge(string id, string text, double x, double y)
        {
            Id = id;
            Text = text;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableEdge(string id, string text, double x, double y, IDrawableNode parent, IDrawableNode child) : this(id, text, x, y)
        {
            SourceNode = parent;
            TargetNode = child;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableEdge(string id, string text, IDrawableNode parent, IDrawableNode child)
        {
            Id = id;
            Text = text;
            SourceNode = parent;
            TargetNode = child;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableEdge(string id, string text, string sourceId, string targetId, List<PathPoint> points, bool isLoaded)
        {
            Id = id;
            Text = text;
            SourceId = sourceId;
            TargetId = targetId;
            Points = points;
            IsLoaded = isLoaded;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableEdge(string id, string text, IEdgeLabel label, string sourceId, string targetId, List<PathPoint> points, bool isLoaded)
        {
            Id = id;
            Text = text;
            SourceId = sourceId;
            TargetId = targetId;
            Points = points;
            IsLoaded = isLoaded;
            Label = label;
        }

        /// <summary>
        /// Sets the edge's source node.
        /// </summary>
        public void SetSourceNode(IDrawableNode start)
        {
            SourceNode = start;
        }

        /// <summary>
        /// Set's the edge's target node.
        /// </summary>
        public void SetTargetNode(IDrawableNode end)
        {
            TargetNode = end;
        }

        /// <summary>
        /// Sets the edge's position.
        /// </summary>
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DrawableEdge);
        }

        public virtual bool Equals(DrawableEdge other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y &&
                   EqualityComparer<IDrawableNode>.Default.Equals(SourceNode, other.SourceNode) &&
                   EqualityComparer<IDrawableNode>.Default.Equals(TargetNode, other.TargetNode) &&
                   Id == other.Id &&
                   Text == other.Text &&
                   EqualityComparer<List<PathPoint>>.Default.Equals(Points, other.Points) &&
                   EqualityComparer<PathPoint>.Default.Equals(LabelPoint, other.LabelPoint) &&
                   IsLoaded == other.IsLoaded &&
                   TargetId == other.TargetId &&
                   SourceId == other.SourceId &&
                   EqualityComparer<IEdgeLabel>.Default.Equals(Label, other.Label);
        }

        public override int GetHashCode()
        {
            var hashCode = -1842038537;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<IDrawableNode>.Default.GetHashCode(SourceNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<IDrawableNode>.Default.GetHashCode(TargetNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<PathPoint>>.Default.GetHashCode(Points);
            hashCode = hashCode * -1521134295 + EqualityComparer<PathPoint>.Default.GetHashCode(LabelPoint);
            hashCode = hashCode * -1521134295 + IsLoaded.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TargetId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SourceId);
            hashCode = hashCode * -1521134295 + EqualityComparer<IEdgeLabel>.Default.GetHashCode(Label);
            return hashCode;
        }

        public static bool operator ==(DrawableEdge edge1, DrawableEdge edge2)
        {
            return EqualityComparer<DrawableEdge>.Default.Equals(edge1, edge2);
        }

        public static bool operator !=(DrawableEdge edge1, DrawableEdge edge2)
        {
            return !(edge1 == edge2);
        }
    }
}
