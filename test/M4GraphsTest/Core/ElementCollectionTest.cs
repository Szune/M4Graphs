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
        private readonly ElementCollection<DefaultNodeElement, DefaultEdgeElement> _sut =
            new ElementCollection<DefaultNodeElement, DefaultEdgeElement>();

        private int _nodeCount;
        private int _edgeCount;

        const string elementText = "text_Verify";
        const string elementId = "id0";

        private void PrepareCollection()
        {
            for(int i = 0; i < _nodeCount; i++)
                _sut.Add(new DefaultNodeElement(i.ToString(), i.ToString()));
            for(int i = 0; i < _edgeCount; i++)
                _sut.Add(new DefaultEdgeElement(i.ToString(), i.ToString()));
        }

        private void AddNode()
        {
            _sut.Add(new DefaultNodeElement(elementId, elementText));
        }

        private void AddEdge()
        {
            _sut.Add(new DefaultEdgeElement(elementId, elementText));
        }

        [TestInitialize]
        public void Initialize()
        {
            _nodeCount = 3;
            _edgeCount = 4;
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
        public void Add_node_should_add_node()
        {
            AddNode();
            _sut.Nodes.First().Id.Should().Be(elementId);
        }

        [TestMethod]
        public void Add_node_should_not_add_node_if_node_with_same_id_exists()
        {
            AddNode();
            var node2 = new DefaultNodeElement(elementId, "node2");
            Action act = () => _sut.Add(node2);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [TestMethod]
        public void Add_edge_should_not_add_edge_if_edge_with_same_id_exists()
        {
            AddEdge();
            var edge2 = new DefaultEdgeElement(elementId, "edge2");
            Action act = () => _sut.Add(edge2);
            act.Should().ThrowExactly<ArgumentException>();
        }

        [TestMethod]
        public void Add_edge_should_add_edge()
        {
            AddEdge();
            _sut.Edges.First().Id.Should().Be(elementId);
        }

        [TestMethod]
        public void Count_should_return_total_count_of_nodes_and_edges()
        {
            PrepareCollection();
            _sut.Count.Should().Be(_nodeCount + _edgeCount);
        }

        [TestMethod]
        public void Node_enumeration_should_enumerate_nodes()
        {
            PrepareCollection();
            var c = 0;
            foreach (var node in _sut.Nodes)
            {
                node.Id.Should().Be(c.ToString());
                c++;
            }
        }

        [TestMethod]
        public void Edge_enumeration_should_enumerate_edges()
        {
            PrepareCollection();
            var c = 0;
            foreach (var edge in _sut.Edges)
            {
                edge.Id.Should().Be(c.ToString());
                c++;
            }
        }

        [TestMethod]
        public void Nodes_should_be_immutable()
        {
            var node = new DefaultNodeElement("mutable", "mutable");
            Action act = () => (_sut.Nodes as ICollection<DefaultNodeElement>).Add(node);
            act.Should().ThrowExactly<NotSupportedException>();
        }

        [TestMethod]
        public void Edges_should_be_immutable()
        {
            var node = new DefaultNodeElement("mutable", "mutable");
            Action act = () => (_sut.Nodes as ICollection<DefaultNodeElement>).Add(node);
            act.Should().ThrowExactly<NotSupportedException>();
        }

        [TestMethod]
        public void GetNode_should_return_node_when_node_with_specified_id_exists()
        {
            AddNode();
            _sut.GetNode(elementId).Text.Should().Be(elementText);
        }

        [TestMethod]
        public void GetEdge_should_return_edge_when_edge_with_specified_id_exists()
        {
            AddEdge();
            _sut.GetEdge(elementId).Text.Should().Be(elementText);
        }

        [TestMethod]
        public void GetNode_should_throw_KeyNotFoundException_if_node_is_not_present_in_collection()
        {
            Action act = () => _sut.GetNode(elementId);
            act.Should().ThrowExactly<KeyNotFoundException>();
        }

        [TestMethod]
        public void GetEdge_should_throw_KeyNotFoundException_if_edge_is_not_present_in_collection()
        {
            Action act = () => _sut.GetEdge(elementId);
            act.Should().ThrowExactly<KeyNotFoundException>();
        }

        [TestMethod]
        public void RemoveNode_should_remove_node_from_collection()
        {
            AddNode();
            _sut.RemoveNode(elementId).Should().BeTrue();
            _sut.Nodes.Should().HaveCount(0);
        }

        [TestMethod]
        public void RemoveEdge_should_remove_edge_from_collection()
        {
            AddEdge();
            _sut.RemoveEdge(elementId);
            _sut.Edges.Should().HaveCount(0);
        }

        [TestMethod]
        public void RemoveEdge_should_return_true_if_edge_was_removed()
        {
            AddEdge();
            _sut.RemoveEdge(elementId).Should().BeTrue();
        }

        [TestMethod]
        public void RemoveNode_should_return_true_if_node_was_removed()
        {
            AddNode();
            _sut.RemoveNode(elementId).Should().BeTrue();
        }

        [TestMethod]
        public void RemoveNode_should_return_false_if_node_is_already_not_present()
        {
            _sut.RemoveNode(elementId).Should().BeFalse();
        }

        [TestMethod]
        public void RemoveEdge_should_return_false_if_edge_is_already_not_present()
        {
            _sut.RemoveEdge(elementId).Should().BeFalse();
        }

        [TestMethod]
        public void TryGetNode_should_return_false_if_node_is_not_present()
        {
            _sut.TryGetNode(elementId, out _).Should().BeFalse();
        }

        [TestMethod]
        public void TryGetEdge_should_return_false_if_edge_is_not_present()
        {
            _sut.TryGetEdge(elementId, out _).Should().BeFalse();
        }

        [TestMethod]
        public void TryGetNode_should_return_true_if_node_is_present()
        {
            AddNode();
            _sut.TryGetNode(elementId, out _).Should().BeTrue();
        }

        [TestMethod]
        public void TryGetEdge_should_return_true_if_edge_is_present()
        {
            AddEdge();
            _sut.TryGetEdge(elementId, out _).Should().BeTrue();
        }

        [TestMethod]
        public void Constructor_Dictionary_assigns_values()
        {
            var nodes = new Dictionary<string, DefaultNodeElement>{ {elementId, new DefaultNodeElement(elementId, elementText)} };
            var edges = new Dictionary<string, DefaultEdgeElement> { {elementId, new DefaultEdgeElement(elementId, elementText)}, {elementId + "1", new DefaultEdgeElement(elementId + "1", elementText)} };
            var sut = new ElementCollection<DefaultNodeElement, DefaultEdgeElement>(nodes, edges);
            sut.Nodes.Should().HaveCount(1);
            sut.Edges.Should().HaveCount(2);
        }

        [TestMethod]
        public void Constructor_IEnumerable_assigns_values()
        {
            var nodes = new List<DefaultNodeElement> { new DefaultNodeElement(elementId, elementText) };
            var edges = new List<DefaultEdgeElement> { new DefaultEdgeElement(elementId, elementText), new DefaultEdgeElement(elementId + "1", elementText) };
            var sut = new ElementCollection<DefaultNodeElement, DefaultEdgeElement>(nodes, edges);
            sut.Nodes.Should().HaveCount(1);
            sut.Edges.Should().HaveCount(2);
        }
    }
}
