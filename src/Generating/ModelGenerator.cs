using M4Graphs.Core.Elements;
using System;
using System.Collections.Generic;

namespace M4Graphs.Generators
{
    /// <summary>
    /// Builder for <see cref="IModelGenerator"/>s.
    /// </summary>
    public static class ModelGenerator
    {
        public static ModelGenerator<DefaultModelGenerator> Default => new ModelGenerator<DefaultModelGenerator>();
    }

    /// <summary>
    /// Builder for <see cref="IModelGenerator"/>s.
    /// </summary>
    public class ModelGenerator<TModelGenerator> where TModelGenerator : IModelGenerator
    {
        private readonly Stack<Action<TModelGenerator>> _buildJobs = new Stack<Action<TModelGenerator>>();
        private readonly TModelGenerator _generator;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        public ModelGenerator()
        {
            _generator = Activator.CreateInstance<TModelGenerator>();
        }

        /// <summary>
        /// Sets the margins used when drawing.
        /// </summary>
        public ModelGenerator<TModelGenerator> Margins(int xMargin, int yMargin)
        {
            void SetMargins(TModelGenerator g) => g.SetMargins(xMargin, yMargin);
            _buildJobs.Push(SetMargins);
            return this;
        }

        /// <summary>
        /// Sets the start node.
        /// </summary>
        public ModelGenerator<TModelGenerator> StartNode(string id, string text)
        {
            void SetStartNode(TModelGenerator g) => g.SetStartNode(new DefaultNodeElement(id, text));
            _buildJobs.Push(SetStartNode);
            return this;
        }

        /// <summary>
        /// Builds the <see cref="DefaultModelGenerator"/>.
        /// </summary>
        public TModelGenerator Build()
        {
            while(_buildJobs.Count > 0)
                _buildJobs.Pop().Invoke(_generator); // perform build job
            return _generator;
        }
    }
}