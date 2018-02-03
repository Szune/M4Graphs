using M4Graphs.Core.General;
using System;
using M4Graphs.Core.Geometry;
using System.Collections.Generic;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// A node prepared for drawing.
    /// </summary>
    public class DrawableNode : IDrawableNode, IEquatable<DrawableNode>
    {
        /// <summary>
        /// Returns the x-coordinate of the node.
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of the node.
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Returns the node's width.
        /// </summary>
        public double Width { get; private set; }

        /// <summary>
        /// Returns the node's height.
        /// </summary>
        public double Height { get; private set; }

        /// <summary>
        /// Returns the node's parent node's identifier.
        /// </summary>
        public string ParentNodeId { get; internal set; }

        /// <summary>
        /// Returns a value indicating whether this node was loaded or generated.
        /// </summary>
        public bool IsLoaded { get; }

        /// <summary>
        /// Returns the node's identifier.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Returns the node's text.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Returns the x-coordinate of the center of the node.
        /// </summary>
        public double CenterX => (X + (Width / 2));
        /// <summary>
        /// Returns the y-coordinate of the center of the node.
        /// </summary>
        public double CenterY => (Y + (Height / 2));

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableNode(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableNode(string id, string text, double x, double y) : this(id)
        {
            Text = text;
            X = x;
            Y = y;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DrawableNode(string id, string text, double x, double y, double width, double height, bool isLoaded) : this(id, text, x, y)
        {
            Width = width;
            Height = height;
            IsLoaded = isLoaded;
        }

        /// <summary>
        /// Sets the node's position.
        /// </summary>
        public void SetPosition(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Sets the size of the node.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the point where the specified edge collides with the node.
        /// </summary>
        public PathPoint GetPointOfEdgeCollision(PathPoint nextLastPoint)
        {
            return Collision.GetPointOfEdgeCollision(this, nextLastPoint);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DrawableNode);
        }

        public virtual bool Equals(DrawableNode other)
        {
            return other != null &&
                   X == other.X &&
                   Y == other.Y &&
                   Width == other.Width &&
                   Height == other.Height &&
                   ParentNodeId == other.ParentNodeId &&
                   IsLoaded == other.IsLoaded &&
                   Id == other.Id &&
                   Text == other.Text &&
                   CenterX == other.CenterX &&
                   CenterY == other.CenterY;
        }

        public override int GetHashCode()
        {
            var hashCode = -610834779;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Width.GetHashCode();
            hashCode = hashCode * -1521134295 + Height.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ParentNodeId);
            hashCode = hashCode * -1521134295 + IsLoaded.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + CenterX.GetHashCode();
            hashCode = hashCode * -1521134295 + CenterY.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(DrawableNode node1, DrawableNode node2)
        {
            return EqualityComparer<DrawableNode>.Default.Equals(node1, node2);
        }

        public static bool operator !=(DrawableNode node1, DrawableNode node2)
        {
            return !(node1 == node2);
        }
    }
}
