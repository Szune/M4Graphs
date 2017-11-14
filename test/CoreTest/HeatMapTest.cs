using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using M4Graphs.Core;
using FluentAssertions;

namespace M4Graphs.CoreTest
{
    [TestClass]
    public class HeatMapTest
    {
        HeatMap map;
        [TestInitialize]
        public void Initialize()
        {
            map = new HeatMap();
        }

        [TestMethod]
        public void GetHeat_Throws_If_Element_Does_Not_Exist_In_HeatMap()
        {
            Action act = () => map.GetHeat("e1");
            act.ShouldThrowExactly<KeyNotFoundException>();
        }

        [TestMethod]
        public void GetHeat_Returns_100_Percent_If_There_Is_Only_One_Element_In_HeatMap()
        {
            // setup
            map.AddHeat("n1");
            map.AddHeat("n1");
            // assert
            map.GetHeat("n1").Should().Be(1);
        }
        
        [TestMethod]
        public void GetHeat_Returns_50_Percent_If_There_Are_Only_Two_Elements_With_Same_Amount_Of_Hits()
        {
            // setup
            map.AddHeat("n1");
            map.AddHeat("e1");
            // assert
            map.GetHeat("n1").Should().Be(0.5);
        }

        [TestMethod]
        public void GetHeat_Return_40_Percent_When_4_Out_Of_10_Hits_On_One_Element()
        {
            // setup
            for (int i = 0; i < 4; i++)
            {
                map.AddHeat("n1");
                map.AddHeat("e1");
            }
            map.AddHeat("n2");
            map.AddHeat("n2");
            // assert
            map.GetHeat("n1").Should().Be(0.4);
            map.GetHeat("e1").Should().Be(0.4);
        }

        [TestMethod]
        public void GetHeat_Return_20_Percent_When_2_Out_Of_10_Hits_On_One_Element()
        {
            // setup
            for (int i = 0; i < 4; i++)
            {
                map.AddHeat("n1");
                map.AddHeat("e1");
            }
            map.AddHeat("n2");
            map.AddHeat("n2");
            // assert
            map.GetHeat("n2").Should().Be(0.2);
        }

        [TestMethod]
        public void AddHeat_Does_Not_Throw()
        {
            Action act = () => map.AddHeat("n1");
            act.ShouldNotThrow();
        }
    }
}
