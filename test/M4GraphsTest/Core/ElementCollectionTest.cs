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

        [TestMethod]
        public void Node_Enumeration_Enumerates_Nodes()
        {
            PrepareCollection();
            _sut.Nodes.Should().HaveCount(prepCount);
        }
    }
}
