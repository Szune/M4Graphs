using M4Graphs.Core;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using M4Graphs.Core.Elements;
using M4Graphs.Core.Geometry;
using M4Graphs.Generators;
using M4Graphs.Parsers;
using M4Graphs.Wpf.Rendering;

namespace M4Graphs.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>> doStuff = new List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>>();
        List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>> doStuffCopy = new List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>>();
        Queue<Action> addHeat = new Queue<Action>();

        private Timer simulationTimer;

        public MainWindow()
        {
            InitializeComponent();
            simulationTimer = new Timer(500);
            simulationTimer.Enabled = false;
            simulationTimer.Elapsed += SimulationTimer_Elapsed;
        }

        private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Simulate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reset();

            var reader = ModelParser.Graphml.FromFile(@"D:\exempel.graphml").Build();
            var renderer = WpfRenderer.Graphml(reader.GetElements());
            renderer.SetOffset(new Coordinate(20, 20));
            dgm.Draw(renderer);
            CreatePreconditionsForSimulatingTheModel();
            //CreatePreconditionsForBuildingTheModel();
        }

        private void CreatePreconditionsForSimulatingTheModel()
        {
            for (int i = 0; i < 10; i++)
            {
                addHeat.Enqueue(() => dgm.ActivateElement("n0"));
                addHeat.Enqueue(() => dgm.ActivateElement("e0"));
                addHeat.Enqueue(() => dgm.ActivateElement("n2"));
                addHeat.Enqueue(() => dgm.ActivateElement("n0"));
                addHeat.Enqueue(() => dgm.ActivateElement("e1"));
                addHeat.Enqueue(() => dgm.AddElementError("e1"));
                addHeat.Enqueue(() => dgm.ActivateElement("n1"));
                addHeat.Enqueue(() => dgm.ActivateElement("e3"));
                addHeat.Enqueue(() => dgm.ActivateElement("n3"));
                addHeat.Enqueue(() => dgm.AddElementError("n1"));
            }
        }

        private void CreatePreconditionsForBuildingTheModel()
        {
            for (int i = 1; i < 6; i += 2)
            {
                doStuff.Add(Tuple.Create("n" + i, ModelElementFactory.CreateEdge("e" + (i + 1), "Edge" + (i + 1)), null as DefaultNodeElement, false));
                doStuff.Add(Tuple.Create("e" + (i + 1), null as DefaultEdgeElement, ModelElementFactory.CreateNode("n" + (i + 2), "Node" + (i + 2)), false));
            }

            for (int i = 1; i < 6; i += 2)
            {
                doStuff.Add(Tuple.Create("n" + i, ModelElementFactory.CreateEdge("e" + (i + 10), "Edge" + (i + 10)), null as DefaultNodeElement, false));
                doStuff.Add(Tuple.Create("e" + (i + 10), null as DefaultEdgeElement, ModelElementFactory.CreateNode("n" + (i + 11), "Nooooooooooooode" + (i + 11)), false));
            }

            for (int i = 1; i < 6; i += 2)
            {
                doStuff.Add(Tuple.Create("n" + i, ModelElementFactory.CreateEdge("e" + (i + 30), "Edge" + (i + 30)), null as DefaultNodeElement, false));
                doStuff.Add(Tuple.Create("e" + (i + 30), null as DefaultEdgeElement, ModelElementFactory.CreateNode("n" + (i + 31), "Node" + (i + 31)), false));
            }

            for (int i = 1; i < 6; i += 2)
            {
                doStuff.Add(Tuple.Create("n" + (i + 11), ModelElementFactory.CreateEdge("e" + (i + 50), "Edge" + (i + 50)), ModelElementFactory.CreateNode("n" + i, "w/e"), true));
            }
            doStuffCopy = new List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>>(doStuff);
        }

        private void Reset()
        {
            var model = ModelGenerator.Default.Margins(80, 40).StartNode("n1","Start").Build();
            // TODO: make a renderer for DefaultModelGenerator
            //dgm.Set(model);
            //dgm.Draw();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //BuildTree();
            //System.Threading.Thread.Sleep(3000);
            simulationTimer.Enabled = true;
        }

        private void Simulate()
        {
            if (addHeat.Count < 1) return;
            //dgm.Dispatcher.Invoke(() =>
            //{
                addHeat.Dequeue()();
            //});
        }

        private void BuildTree()
        {
            //if (!doStuffCopy.Any()) return;
            //var act = doStuffCopy.First();
            //if (!act.Item4)
            //{
            //    if(act.Item2 != null)
            //        (dgm.Model as DefaultModelGenerator).AddElement(act.Item1, act.Item2);
            //    else
            //    {
            //        (dgm.Model as DefaultModelGenerator).AddElement(act.Item1, act.Item3);
            //    }
            //}
            //else
            //{
            //    (dgm.Model as DefaultModelGenerator).AddElement(act.Item1, act.Item2);
            //    (dgm.Model as DefaultModelGenerator).Connect(act.Item2.Id, act.Item3.Id);
            //}
            //doStuffCopy.Remove(doStuffCopy.First());
            //dgm.Draw();
            // TODO: rewrite DefaultModelGenerator and create a renderer for it
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            doStuffCopy = new List<Tuple<string, DefaultEdgeElement, DefaultNodeElement, bool>>(doStuff);
            Reset();
        }
    }
}
