using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// A collection of drawable elements.
    /// </summary>
    public class DrawableElementCollection : IEnumerable<IDrawableElement>
    {
        private readonly Dictionary<string, IDrawableEdge> _edges = new Dictionary<string, IDrawableEdge>();
        private readonly Dictionary<string, IDrawableNode> _nodes = new Dictionary<string, IDrawableNode>();

        /// <summary>
        /// Returns a dictionary of <see cref="IDrawableEdge"/>s, where the key is the edge's identifier.
        /// </summary>
        public Dictionary<string, IDrawableEdge> Edges => new Dictionary<string, IDrawableEdge>(_edges);
        /// <summary>
        /// Returns a dictionary of <see cref="IDrawableNode"/>s, where the key is the node's identifier.
        /// </summary>
        public Dictionary<string, IDrawableNode> Nodes => new Dictionary<string, IDrawableNode>(_nodes);

        /// <summary>
        /// Returns all <see cref="IDrawableElement"/>s contained in this collection.
        /// </summary>
        public Dictionary<string, IDrawableElement> Elements =>
            _edges.Values.Cast<IDrawableElement>().Concat(_nodes.Values).ToDictionary(element => element.Id);
        /// <summary>
        /// Returns the number of elements in this collection.
        /// </summary>
        public int Count => _edges.Count + _nodes.Count;

        public DrawableElementCollection() { }

        public DrawableElementCollection(Dictionary<string, IDrawableNode> nodes, Dictionary<string, IDrawableEdge> edges)
        {
            _nodes = nodes;
            _edges = edges;
        }

        /// <summary>
        /// Adds an <see cref="IDrawableElement"/> to the collection.
        /// </summary>
        /// <param name="item"></param>
        public void Add(IDrawableElement item)
        {
            if (item is IDrawableEdge edge)
            {
                _edges.Add(edge.Id, edge);
                return;
            }
            if (item is IDrawableNode node)
            {
                _nodes.Add(node.Id, node);
                return;
            }
            throw new ArgumentException($"argument is not convertable to neither {nameof(DrawableEdge)} nor {nameof(DrawableNode)}.", "item");
        }

        /// <summary>
        /// Adds a <see cref="DrawableEdge"/> to the collection.
        /// </summary>
        /// <param name="edge"></param>
        public void Add(IDrawableEdge edge)
        {
            _edges.Add(edge.Id, edge);
        }

        /// <summary>
        /// Adds a <see cref="DrawableNode"/> to the collection.
        /// </summary>
        /// <param name="node"></param>
        public void Add(IDrawableNode node)
        {
            _nodes.Add(node.Id, node);
        }

        /// <summary>
        /// Removes all the elements in the collection.
        /// </summary>
        public void Clear()
        {
            _edges.Clear();
            _nodes.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains the specified <see cref="DrawableEdge"/>.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool Contains(IDrawableEdge edge)
        {
            return _edges.Values.Contains(edge);
        }

        /// <summary>
        /// Determines whether the collection contains the specified <see cref="DrawableNode"/>.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(IDrawableNode node)
        {
            return _nodes.Values.Contains(node);
        }

        /// <summary>
        /// Determines whether the collection contains the specified <see cref="IDrawableElement"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(IDrawableElement item)
        {
            if (item is DrawableEdge edge)
            {
                return _edges.Values.Contains(edge);
            }
            if (item is DrawableNode node)
            {
                return _nodes.Values.Contains(node);
            }
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through all the elements.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IDrawableElement> GetEnumerator()
        {
            return Elements.Values.GetEnumerator();
        }

        /// <summary>
        /// Removes the specified <see cref="DrawableEdge"/> from the collection.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool Remove(IDrawableEdge edge)
        {
            return _edges.Remove(edge.Id);
        }

        /// <summary>
        /// Removes the specified <see cref="DrawableNode"/> from the collection.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Remove(IDrawableNode node)
        {
            return _nodes.Remove(node.Id);
        }

        /// <summary>
        /// Removes the specified <see cref="IDrawableElement"/> from the collection.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(IDrawableElement item)
        {
            if (item is IDrawableEdge edge)
            {
                return _edges.Remove(edge.Id);
            }
            if (item is IDrawableNode node)
            {
                return _nodes.Remove(node.Id);
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.Values.GetEnumerator();
        }
    }
}
