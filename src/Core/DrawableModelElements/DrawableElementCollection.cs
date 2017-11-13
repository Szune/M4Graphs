using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M4Graphs.Core.DrawableModelElements
{
    /// <summary>
    /// A collection of drawable elements.
    /// </summary>
    public class DrawableElementCollection : IEnumerable<IDrawableElement>
    {
        /// <summary>
        /// Returns a dictionary of <see cref="IDrawableEdge"/>s, where the key is the edge's identifier.
        /// </summary>
        public Dictionary<string, IDrawableEdge> Edges = new Dictionary<string, IDrawableEdge>();
        /// <summary>
        /// Returns a dictionary of <see cref="IDrawableNode"/>s, where the key is the node's identifier.
        /// </summary>
        public Dictionary<string, IDrawableNode> Nodes = new Dictionary<string, IDrawableNode>();
        /// <summary>
        /// Returns all <see cref="IDrawableElement"/>s contained in this collection.
        /// </summary>
        public Dictionary<string, IDrawableElement> Elements =>
            Edges.Values.Cast<IDrawableElement>().Concat(Nodes.Values.Cast<IDrawableElement>()).ToDictionary(element => element.Id);
        /// <summary>
        /// Returns the number of elements in this collection.
        /// </summary>
        public int Count => Edges.Count + Nodes.Count;

        /// <summary>
        /// Adds an <see cref="IDrawableElement"/> to the collection.
        /// </summary>
        /// <param name="item"></param>
        public void Add(IDrawableElement item)
        {
            if (item is IDrawableEdge edge)
            {
                Edges.Add(edge.Id, edge);
                return;
            }
            if (item is IDrawableNode node)
            {
                Nodes.Add(node.Id, node);
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
            Edges.Add(edge.Id, edge);
        }

        /// <summary>
        /// Adds a <see cref="DrawableNode"/> to the collection.
        /// </summary>
        /// <param name="node"></param>
        public void Add(IDrawableNode node)
        {
            Nodes.Add(node.Id, node);
        }

        /// <summary>
        /// Removes all the elements in the collection.
        /// </summary>
        public void Clear()
        {
            Edges.Clear();
            Nodes.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains the specified <see cref="DrawableEdge"/>.
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool Contains(IDrawableEdge edge)
        {
            return Edges.Values.Contains(edge);
        }

        /// <summary>
        /// Determines whether the collection contains the specified <see cref="DrawableNode"/>.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Contains(IDrawableNode node)
        {
            return Nodes.Values.Contains(node);
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
                return Edges.Values.Contains(edge);
            }
            if (item is DrawableNode node)
            {
                return Nodes.Values.Contains(node);
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
            return Edges.Remove(edge.Id);
        }

        /// <summary>
        /// Removes the specified <see cref="DrawableNode"/> from the collection.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Remove(IDrawableNode node)
        {
            return Nodes.Remove(node.Id);
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
                return Edges.Remove(edge.Id);
            }
            if (item is IDrawableNode node)
            {
                return Nodes.Remove(node.Id);
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Elements.Values.GetEnumerator();
        }
    }
}
