using M4Graphs.Core.ModelElements;
using System;
using System.Collections.Concurrent;

namespace M4Graphs.Core
{
    /// <summary>
    /// An object pool for model elements.
    /// </summary>
    public static class ModelElementFactory
    {
        private static ConcurrentBag<ModelNode> NodePool { get; }
        private static Func<ModelNode> NodeGenerator { get; }
        private static ConcurrentBag<ModelEdge> EdgePool { get; }
        private static Func<ModelEdge> EdgeGenerator { get; }
        
        static ModelElementFactory()
        {
            NodePool = new ConcurrentBag<ModelNode>();
            EdgePool = new ConcurrentBag<ModelEdge>();
            NodeGenerator = () => new ModelNode();
            EdgeGenerator = () => new ModelEdge();
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
        /// Returns a <see cref="ModelNode"/> with the specified identifier and text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ModelNode CreateNode(string id, string text)
        {
            ModelNode item;
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
        /// Returns a <see cref="ModelEdge"/> with the specified identifier and text.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static ModelEdge CreateEdge(string id, string text)
        {
            ModelEdge item;
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
        /// Returns a <see cref="ModelEdge"/> with the specified identifier, text and parent node.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static ModelEdge CreateEdge(string id, string text, ModelNode parentNode)
        {
            var edge = CreateEdge(id, text);
            edge.SetParentNode(parentNode);
            return edge;
        }

        /// <summary>
        /// Puts the <see cref="ModelNode"/> back into the pool.
        /// </summary>
        /// <param name="node"></param>
        public static void Put(ModelNode node)
        {
            NodePool.Add(node);
        }

        /// <summary>
        /// Puts the <see cref="ModelEdge"/> back into the pool.
        /// </summary>
        /// <param name="edge"></param>
        public static void Put(ModelEdge edge)
        {
            EdgePool.Add(edge);
        }
    }
}
