using System;
using System.Collections.Concurrent;
using M4Graphs.Core.Elements;

namespace M4Graphs.Core
{
    /// <summary>
    /// An object pool for model elements.
    /// </summary>
    public static class ModelElementFactory
    {
        private static ConcurrentBag<DefaultNodeElement> NodePool { get; }
        private static Func<DefaultNodeElement> NodeGenerator { get; }
        private static ConcurrentBag<DefaultEdgeElement> EdgePool { get; }
        private static Func<DefaultEdgeElement> EdgeGenerator { get; }
        
        static ModelElementFactory()
        {
            NodePool = new ConcurrentBag<DefaultNodeElement>();
            EdgePool = new ConcurrentBag<DefaultEdgeElement>();
            NodeGenerator = () => new DefaultNodeElement();
            EdgeGenerator = () => new DefaultEdgeElement();
        }

        /// <summary>
        /// Fills the object pool with the specified amount of node and edge objects.
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="edges"></param>
        public static void Fill(int nodes, int edges)
        {
            for (int i = 0; i < nodes; i++)
                NodePool.Add(NodeGenerator());

            for (int i = 0; i < edges; i++)
                EdgePool.Add(EdgeGenerator());
        }

        /// <summary>
        /// Returns a <see cref="DefaultNodeElement"/> with the specified identifier and text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DefaultNodeElement CreateNode(string id, string text)
        {
            DefaultNodeElement item;
            if (NodePool.TryTake(out item))
            {
                item.Id = id;
                item.Text = text;
                return item;
            }
            NodePool.Add(NodeGenerator());

            var newNode = NodeGenerator();
            newNode.Id = id;
            newNode.Text = text;
            return newNode;
        }

        /// <summary>
        /// Returns a <see cref="DefaultEdgeElement"/> with the specified identifier and text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static DefaultEdgeElement CreateEdge(string id, string text)
        {
            DefaultEdgeElement item;
            if (EdgePool.TryTake(out item))
            {
                item.Id = id;
                item.Text = text;
                return item;
            }
            EdgePool.Add(EdgeGenerator());

            var newEdge = EdgeGenerator();
            newEdge.Id = id;
            newEdge.Text = text;
            return newEdge;
        }

        /// <summary>
        /// Returns a <see cref="DefaultEdgeElement"/> with the specified identifier, text and parent node.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static DefaultEdgeElement CreateEdge(string id, string text, DefaultNodeElement parentNode)
        {
            var edge = CreateEdge(id, text);
            edge.SetParentNode(parentNode);
            return edge;
        }

        /// <summary>
        /// Puts the <see cref="DefaultNodeElement"/> back into the pool.
        /// </summary>
        /// <param name="node"></param>
        public static void Put(DefaultNodeElement node)
        {
            NodePool.Add(node);
        }

        /// <summary>
        /// Puts the <see cref="DefaultEdgeElement"/> back into the pool.
        /// </summary>
        /// <param name="edgeElement"></param>
        public static void Put(DefaultEdgeElement edgeElement)
        {
            EdgePool.Add(edgeElement);
        }
    }
}
