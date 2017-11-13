using M4Graphs.Core;
using M4Graphs.Core.General;
using M4Graphs.Core.ModelElements;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace M4Graphs.CoreTest
{
    [TestClass]
    public class ModelGeneratorTest
    {
        private ModelGenerator model;
        private ModelNode node;
        private ModelEdge edge;
        [TestInitialize]
        public void Initialize()
        {
            model = new ModelGenerator();
            node = ModelElementFactory.CreateNode("n1", "Start");
            edge = ModelElementFactory.CreateEdge("e1", "DoThing");
        }

        [TestMethod]
        public void Model_SetStartNode_Should_Set_StartNode()
        {
            model.SetStartNode(node);
            model.StartNode.Should().Be(node);
        }

        [TestMethod]
        public void Model_SetStartNode_Should_Set_Levels_To_Zero()
        {
            model.SetStartNode(node);
            model.StartNode.Position.X.Should().Be(0);
            model.StartNode.Position.Y.Should().Be(0);
        }

        [TestMethod]
        public void Model_SetStartNode_Should_Assume_StartNode_If_There_Is_No_StartNode_And_A_Node_With_No_Parent_Edge_Was_Added()
        {
            model.SetStartNode(node);
            model.StartNode.Should().Be(node);
        }

        [TestMethod]
        public void Model_AddStartNode_Should_Set_Level_X_To_Zero_If_It_Has_No_Parent_Edge_And_There_Are_No_Other_Nodes_At_That_Level()
        {
            model.AddStartNode(node);
            model.Nodes[node.Id].Position.Should().Be(new GeneratedPosition(0, 0));
        }

        [TestMethod]
        public void Model_AddStartNode_Should_Set_Level_X_To_One_If_It_Has_No_Parent_Edge_And_There_Is_One_Other_Node_At_That_Level()
        {
            model.SetStartNode(node);
            var secondNode = ModelElementFactory.CreateNode("n2", "SecondStart");
            model.AddStartNode(secondNode);
            model.Nodes[secondNode.Id].Position.Should().Be(new GeneratedPosition(1, 0));
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Add_Edge()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            model.Edges[edge.Id].Should().Be(edge);
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Only_Add_One_Edge()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            model.Edges.Should().HaveCount(1);
        }

        [TestMethod]
        public void Model_AddElement_Node_Should_Add_Node()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "SecondNode");
            model.AddElement(edge.Id, secondNode);
            model.Nodes[secondNode.Id].Should().Be(secondNode);
        }

        [TestMethod]
        public void Model_AddElement_Node_Should_Only_Add_One_Node()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "SecondNode");
            model.AddElement(edge.Id, secondNode);
            model.Nodes.Should().HaveCount(2);
        }


        [TestMethod]
        public void Model_AddElement_Node_With_Parent_Edge_Set_ParentNode_Of_Added_Node_To_Parent_Edges_ParentNode()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "DoneThing");
            model.AddElement(edge.Id, secondNode);
            model.Nodes[secondNode.Id].ParentNode.Should().Be(node);
        }

        [TestMethod]
        public void Model_AddElement_Should_Increment_Level_Y_By_One_Plus_Parent_Nodes_Level()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "DoneThing");
            model.AddElement(edge.Id, secondNode);
            model.Nodes[secondNode.Id].Position.Should().Be(new GeneratedPosition(node.Position.X, node.Position.Y + 1));
        }

        [TestMethod]
        public void Model_GetElements_First_Node_Should_Be_At_Zero_Zero()
        {
            model.SetStartNode(node);
            var xDist = 20;
            var yDist = 10;
            model.SetMargins(xDist, yDist);
            var elements = model.GetElements();
            elements.Nodes[node.Id].X.Should().Be(0);
            elements.Nodes[node.Id].Y.Should().Be(0);
        }

        [TestMethod]
        public void Model_GetElements_Nodes_Should_Multiply_Levels_After_The_First_Level_By_Their_Respective_Distance()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "DoneThing");
            model.AddElement(edge.Id, secondNode);
            var xDist = 20;
            var yDist = 10;
            model.SetMargins(xDist, yDist);
            var elements = model.GetElements();
            elements.Nodes[secondNode.Id].X.Should().Be(secondNode.Position.X * xDist);
            elements.Nodes[secondNode.Id].Y.Should().Be(secondNode.Position.Y * yDist);
        }

        [TestMethod]
        public void Model_AddElement_Edge_Added_Edge_Should_Set_ParentLevel_To_Parents_Level()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "DoneThing");
            model.AddElement(edge.Id, secondNode);
            var secondEdge = ModelElementFactory.CreateEdge("e2", "DoAnotherThing");
            model.AddElement(secondNode.Id, secondEdge);
            model.Edges[secondEdge.Id].SourceNodePositon.Should().Be(model.Nodes[secondNode.Id].Position);
        }

        [TestMethod]
        public void Model_AddElement_Node_If_Added_Node_Has_ParentEdge_Should_Set_ParentEdges_ChildLevel_To_Added_Nodes_Level()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "DoneThing");
            model.AddElement(edge.Id, secondNode);
            model.Edges[edge.Id].TargetNodePosition.Should().Be(model.Nodes[secondNode.Id].Position);
        }

        [TestMethod]
        public void Model_AddStartNode_Should_Add_To_NodesAtY()
        {
            model.SetStartNode(node);
            model.AddStartNode(ModelElementFactory.CreateNode("n2", "SecondStart"));
            model.NodesAtY[0].Should().Be(2);
        }

        [TestMethod]
        public void Model_SetStartNode_Should_Add_To_NodesAtY()
        {
            model.SetStartNode(node);
            model.NodesAtY[0].Should().Be(1);
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Add_To_EdgesFromNode()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            model.EdgesFromNode[node.Id].Should().Be(1);
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Add_To_EdgesFromNode_Every_Time_Please()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            model.AddElement(node.Id, ModelElementFactory.CreateEdge("e2", "SecondEdge"));
            model.EdgesFromNode[node.Id].Should().Be(2);
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Set_Level_XY_To_Zero_If_Adding_From_StartNode()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            model.Edges[edge.Id].Position.Should().Be(new GeneratedPosition(0, 0));
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Set_Level_Y_Incrementally()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondNode = ModelElementFactory.CreateNode("n2", "SecondNode");
            model.AddElement(edge.Id, secondNode);
            var secondEdge = ModelElementFactory.CreateEdge("e2", "SecondEdge");
            model.AddElement(secondNode.Id, secondEdge);
            model.Edges[secondEdge.Id].Position.Should().Be(new GeneratedPosition(0, 1));
        }

        [TestMethod]
        public void Model_AddElement_Edge_Should_Set_Level_X_Incrementally()
        {
            model.SetStartNode(node);
            model.AddElement(node.Id, edge);
            var secondEdge = ModelElementFactory.CreateEdge("e2", "SecondEdge");
            model.AddElement(node.Id, secondEdge);
            model.Edges[secondEdge.Id].Position.Should().Be(new GeneratedPosition(1, 0));
        }

    }
}
