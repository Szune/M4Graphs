using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using M4Graphs.Core;
using FluentAssertions;

namespace M4Graphs.CoreTest
{
    [TestClass]
    public class BuilderTest
    {
        [TestMethod]
        public void ModelGeneratorBuilder_Order_Of_Jobs_Should_Not_Matter()
        {
            var generator = Model.Generator.Margins(10, 11).StartNode("n0", "Start").Build();
            generator._xMargin.Should().Be(10);
            generator._yMargin.Should().Be(11);
            generator.StartNode.Id.Should().Be("n0");
        }

        [TestMethod]
        public void ModelReaderBuilder_Order_Of_Jobs_Should_Not_Matter()
        {
            var reader = Model.Reader.Offset(5, 6).NoCache().Build();
        }
    }
}
