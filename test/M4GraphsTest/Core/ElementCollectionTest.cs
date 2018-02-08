using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using M4Graphs.Core;
using M4Graphs.Core.Elements;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace M4GraphsTest.Core
{
    [TestClass]
    public class ElementCollectionTest
    {
        private ElementCollection<DefaultNodeElement, DefaultEdgeElement> _sut =
            new ElementCollection<DefaultNodeElement, DefaultEdgeElement>();

        private int prepCount = 4;

        private void PrepareCollection()
        {
            for(int i = 0; i < prepCount; i++)
            _sut.Add(new DefaultNodeElement(i.ToString(), i.ToString()));
        }

        [TestInitialize]
        public void Initialize()
        {
            _sut.Clear();
        }

        [TestMethod]
        public void Clear_should_clear_all_elements()
        {
            PrepareCollection();
            _sut.Clear();
            _sut.Nodes.Should().HaveCount(0);
            _sut.Edges.Should().HaveCount(0);
        }

        [TestMethod]
        public void Node_Enumeration_Enumerates_Nodes()
        {
            PrepareCollection();
            _sut.Nodes.Should().HaveCount(prepCount);
        }

        [TestMethod]
        public void Nodes_should_be_immutable()
        {
            PrepareCollection();
            var node = new DefaultNodeElement("mutable", "mutable");
            Action act = () => (_sut.Nodes as ICollection<DefaultNodeElement>).Add(node);
            act.Should().ThrowExactly<NotSupportedException>();
        }

        [TestMethod]
        public void Edges_should_be_immutable()
        {
            PrepareCollection();
            var node = new DefaultNodeElement("mutable", "mutable");
            Action act = () => (_sut.Nodes as ICollection<DefaultNodeElement>).Add(node);
            act.Should().ThrowExactly<NotSupportedException>();
        }
    }
}
