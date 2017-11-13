using System;
using System.Collections.Generic;
using M4Graphs.Core.General;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// An edge prepared for drawing.
    /// </summary>
    public class DrawableEdge : IDrawableEdge, IEquatable<IDrawableEdge>
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
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
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
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="parent"></param>
        /// <param name="child"></param>
        public DrawableEdge(string id, string text, double x, double y, IDrawableNode parent, IDrawableNode child) : this(id, text, x, y)
        {
            SourceNode = parent;
            TargetNode = child;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="parent"></param>
        /// <param name="child"></param>
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
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="sourceId"></param>
        /// <param name="targetId"></param>
        /// <param name="points"></param>
        /// <param name="isLoaded"></param>
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
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="label"></param>
        /// <param name="sourceId"></param>
        /// <param name="targetId"></param>
        /// <param name="points"></param>
        /// <param name="isLoaded"></param>
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
        /// <param name="start"></param>
        public void SetSourceNode(IDrawableNode start)
        {
            SourceNode = start;
        }

        /// <summary>
        /// Set's the edge's target node.
        /// </summary>
        /// <param name="end"></param>
        public void SetTargetNode(IDrawableNode end)
        {
            TargetNode = end;
        }

        /// <summary>
        /// Sets the edge's position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Determines whether the specified <see cref="IDrawableEdge"/> is equal to the current edge.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(IDrawableEdge other)
        {
            if (other == null) return false;
            if (!(other is IDrawableEdge)) return false;
            if (other.Id != Id) return false;
            return true;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is IDrawableEdge drawable)
                return Equals(drawable);
            return false;
        }

        /// <summary>
        /// Returns the edge's hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
