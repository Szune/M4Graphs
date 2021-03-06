﻿using M4Graphs.Core.Geometry;
using System;
using System.Collections.Generic;

namespace M4Graphs.Core.Elements
{
    /// <summary>
    /// A node generated by the associated model.
    /// </summary>
    public class DefaultNodeElement : INodeElement, IEquatable<DefaultNodeElement>
    {
        /// <summary>
        /// Returns the node's identifier.
        /// </summary>
        public string Id { get; internal set; }
        /// <summary>
        /// Returns the node's text.
        /// </summary>
        public string Text { get; internal set; }

        public int Width { get; }
        public int Height { get; }

        /// <summary>
        /// Returns the node's parent node.
        /// </summary>
        public DefaultNodeElement ParentNode { get; private set; }

        /// <summary>
        /// Returns all edges that end at this node.
        /// </summary>
        public List<DefaultEdgeElement> ParentEdges { get; private set; }
        /// <summary>
        /// Returns all edges that start at this node.
        /// </summary>
        public List<DefaultEdgeElement> ChildEdges { get; private set; }

        /// <summary>
        /// Returns the node's position.
        /// </summary>
        public Coordinate Position { get; }

        /// <summary>
        /// Returns a value indicating whether the node has any child edges associated with it.
        /// </summary>
        public bool HasChild => ChildEdges.Count > 0;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public DefaultNodeElement()
        {
            ParentEdges = new List<DefaultEdgeElement>();
            ChildEdges = new List<DefaultEdgeElement>();
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        public DefaultNodeElement(string id, string text) : this()
        {
            Id = id;
            Text = text;
        }

        /// <summary>
        /// Adds an edge that starts at this node.
        /// </summary>
        /// <param name="edgeElement">The edge that starts at this node.</param>
        public void AddChildEdge(DefaultEdgeElement edgeElement)
        {
            edgeElement.SetParentNode(this);
            ChildEdges.Add(edgeElement);
        }

        /// <summary>
        /// Adds an edge that ends at this node.
        /// </summary>
        /// <param name="edgeElement">The edge that ends at this node.</param>
        public void AddParentEdge(DefaultEdgeElement edgeElement)
        {
            ParentEdges.Add(edgeElement);
        }

        /// <summary>
        /// Sets this node's parent node.
        /// </summary>
        /// <param name="node"></param>
        public void SetParentNode(DefaultNodeElement node)
        {
            ParentNode = node;
        }

        /// <summary>
        /// Returns a string that represents the current edge.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Node ({Id}): {Text}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DefaultNodeElement);
        }

        public virtual bool Equals(DefaultNodeElement other)
        {
            return other != null &&
                   Id == other.Id &&
                   Text == other.Text &&
                   EqualityComparer<DefaultNodeElement>.Default.Equals(ParentNode, other.ParentNode) &&
                   EqualityComparer<List<DefaultEdgeElement>>.Default.Equals(ParentEdges, other.ParentEdges) &&
                   EqualityComparer<List<DefaultEdgeElement>>.Default.Equals(ChildEdges, other.ChildEdges) &&
                   EqualityComparer<Coordinate>.Default.Equals(Position, other.Position) &&
                   HasChild == other.HasChild;
        }

        public override int GetHashCode()
        {
            var hashCode = 1086983834;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + EqualityComparer<DefaultNodeElement>.Default.GetHashCode(ParentNode);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<DefaultEdgeElement>>.Default.GetHashCode(ParentEdges);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<DefaultEdgeElement>>.Default.GetHashCode(ChildEdges);
            hashCode = hashCode * -1521134295 + EqualityComparer<Coordinate>.Default.GetHashCode(Position);
            hashCode = hashCode * -1521134295 + HasChild.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(DefaultNodeElement node1, DefaultNodeElement node2)
        {
            return EqualityComparer<DefaultNodeElement>.Default.Equals(node1, node2);
        }

        public static bool operator !=(DefaultNodeElement node1, DefaultNodeElement node2)
        {
            return !(node1 == node2);
        }
    }
}
