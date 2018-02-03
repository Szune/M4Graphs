﻿using M4Graphs.Core.DrawableModelElements;
using M4Graphs.Core.General;
using System;
using System.Collections.Generic;

namespace M4Graphs.Core.ModelElements
{
    /// <summary>
    /// A node generated by the associated model.
    /// </summary>
    public class ModelNode : IModelElement, IEquatable<ModelNode>
    {
        /// <summary>
        /// Returns the node's identifier.
        /// </summary>
        public string Id { get; internal set; }
        /// <summary>
        /// Returns the node's text.
        /// </summary>
        public string Text { get; internal set; }

        /// <summary>
        /// Returns the node's parent node.
        /// </summary>
        public ModelNode ParentNode { get; set; }

        /// <summary>
        /// Returns all edges that end at this node.
        /// </summary>
        public List<ModelEdge> ParentEdges { get; private set; }
        /// <summary>
        /// Returns all edges that start at this node.
        /// </summary>
        public List<ModelEdge> ChildEdges { get; private set; }

        /// <summary>
        /// Returns the node's generated position.
        /// </summary>
        public GeneratedPosition Position { get; private set; } = new GeneratedPosition();

        /// <summary>
        /// Returns a value indicating whether the node has any child edges associated with it.
        /// </summary>
        public bool HasChild => ChildEdges.Count > 0;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelNode()
        {
            ParentEdges = new List<ModelEdge>();
            ChildEdges = new List<ModelEdge>();
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public ModelNode(string id, string text) : this()
        {
            Id = id;
            Text = text;
        }

        /// <summary>
        /// Adds an edge that starts at this node.
        /// </summary>
        /// <param name="edge">The edge that starts at this node.</param>
        public void AddChildEdge(ModelEdge edge)
        {
            edge.SetParentNode(this);
            ChildEdges.Add(edge);
        }

        /// <summary>
        /// Adds an edge that ends at this node.
        /// </summary>
        /// <param name="edge">The edge that ends at this node.</param>
        public void AddParentEdge(ModelEdge edge)
        {
            ParentEdges.Add(edge);
        }

        /// <summary>
        /// Sets this node's parent node.
        /// </summary>
        /// <param name="node"></param>
        public void SetParentNode(ModelNode node)
        {
            ParentNode = node;
        }

        /// <summary>
        /// Sets the node's generated position.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void SetPosition(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }

        /// <summary>
        /// Returns a string that represents the current edge.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Node ({Id}): {Text}";
        }

        /// <summary>
        /// Determines whether the specified <see cref="ModelNode"/> is equal to the current node.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(ModelNode other)
        {
            if (other == null) return false;
            if (other.Id != Id) return false;
            if (other.Text != Text) return false;
            if (other.Position.X != Position.X) return false;
            if (other.Position.Y != Position.Y) return false;
            return true;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is ModelNode node)
                return Equals(node);
            return false;
        }

        /// <summary>
        /// Returns the node's hash code.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return new { Id, Text, Position.X, Position.Y }.GetHashCode();
        }

        /// <summary>
        /// Converts the <see cref="ModelNode"/> to a new <see cref="DrawableNode"/>.
        /// </summary>
        /// <param name="xDistance"></param>
        /// <param name="yDistance"></param>
        /// <returns></returns>
        public IDrawableNode ToGeneratedDrawable(int xDistance, int yDistance)
        {
            return new DrawableNode(Id, Text, Position.X * xDistance, Position.Y * yDistance);
        }
    }
}
