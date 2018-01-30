using M4Graphs.Core;
using System;
using System.Collections.Generic;

namespace M4Graphs.Generators
{
    /// <summary>
    /// Builder for <see cref="IModelGenerator"/>s.
    /// </summary>
    public sealed class ModelGenerator
    {
        public static ModelGenerator<DefaultModelGenerator> Default => new ModelGenerator<DefaultModelGenerator>();
    }

    /// <summary>
    /// Builder for <see cref="IModelGenerator"/>s.
    /// </summary>
    public class ModelGenerator<TModelGenerator> where TModelGenerator : IModelGenerator
    {
        private Stack<Action<TModelGenerator>> _buildJobs = new Stack<Action<TModelGenerator>>();
        private TModelGenerator _generator;

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
            Action<TModelGenerator> setMargins = (g) => g.SetMargins(xMargin, yMargin);
            _buildJobs.Push(setMargins);
            return this;
        }

        /// <summary>
        /// Sets the start node.
        /// </summary>
        public ModelGenerator<TModelGenerator> StartNode(string id, string text)
        {
            Action<TModelGenerator> setStartNode = (g) => g.SetStartNode(ModelElementFactory.CreateNode(id, text));
            _buildJobs.Push(setStartNode);
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