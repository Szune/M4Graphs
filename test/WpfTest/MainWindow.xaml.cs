using M4Graphs.Core.Geometry;
using M4Graphs.Parsers;
using M4Graphs.Wpf.Rendering;
using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;

namespace M4Graphs.WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly Queue<Action> _addHeat = new Queue<Action>();

        private readonly Timer _simulationTimer;

        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            _simulationTimer = new Timer(500) {Enabled = false};
            _simulationTimer.Elapsed += SimulationTimer_Elapsed;
        }

        private void SimulationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Simulate();
            _simulationTimer.Stop();
            System.Threading.Thread.Sleep(_random.Next(300, 1000));
            _simulationTimer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void CreatePreconditionsForSimulatingTheModel()
        {
            _addHeat.Clear();
            _addHeat.Enqueue(() => dgm.ActivateElement("n1")); // Start
            _addHeat.Enqueue(() => dgm.ActivateElement("e0")); // e_Home
            _addHeat.Enqueue(() => dgm.ActivateElement("n0")); // v_Home
            _addHeat.Enqueue(() => dgm.ActivateElement("e3")); // e_ResetPassword
            _addHeat.Enqueue(() => dgm.ActivateElement("n4")); // v_PasswordIsReset
            _addHeat.Enqueue(() => dgm.ActivateElement("e4")); // e_BackToHome
            _addHeat.Enqueue(() => dgm.ActivateElement("n0")); // v_Home
            _addHeat.Enqueue(() => dgm.ActivateElement("e1")); // e_Login
            _addHeat.Enqueue(() => dgm.ActivateElement("n2")); // v_Storefront
            _addHeat.Enqueue(() => dgm.ActivateElement("e2")); // e_AddItemToCart
            _addHeat.Enqueue(() => dgm.ActivateElement("n3")); // v_ItemIsAddedToCart
            _addHeat.Enqueue(() => dgm.ActivateElement("e8")); // e_ToStorefront
            _addHeat.Enqueue(() => dgm.ActivateElement("n2")); // v_Storefront
            _addHeat.Enqueue(() => dgm.ActivateElement("e6")); // e_DirectlyToCheckout
            _addHeat.Enqueue(() => dgm.ActivateElement("n5")); // v_VerifyCheckout
            _addHeat.Enqueue(() => dgm.ActivateElement("e7")); // e_BackToStorefront
            _addHeat.Enqueue(() => dgm.ActivateElement("n2")); // v_Storefront
            _addHeat.Enqueue(() => dgm.ActivateElement("e6")); // e_DirectlyToCheckout
            _addHeat.Enqueue(() => dgm.ActivateElement("n5")); // v_VerifyCheckout
            _addHeat.Enqueue(() => dgm.AddElementError("n5")); // error @ v_VerifyCheckout
            _addHeat.Enqueue(() => dgm.ActivateElement("e7")); // e_BackToStorefront
            _addHeat.Enqueue(() => dgm.ActivateElement("n2")); // v_Storefront
            _addHeat.Enqueue(() => dgm.ActivateElement("e2")); // e_AddItemToCart
            _addHeat.Enqueue(() => dgm.ActivateElement("n3")); // v_ItemIsAddedToCart
            _addHeat.Enqueue(() => dgm.ActivateElement("e5")); // e_Checkout
            _addHeat.Enqueue(() => dgm.ActivateElement("n5")); // v_VerifyCheckout
            _addHeat.Enqueue(() => dgm.ActivateElement("e7")); // e_BackToStorefront
            _addHeat.Enqueue(() => dgm.ActivateElement("n2")); // v_Storefront
            _addHeat.Enqueue(() => dgm.ActivateElement("n1")); // Start
        }

        private void Reset()
        {
            //dgm = new GraphModel();
            var reader = ModelParser.Graphml.FromFile(@"Models\demo.graphml").Build();
            var renderer = WpfRenderer.Graphml(reader.GetElements());
            renderer.SetOffset(new Coordinate(20, 20));
            dgm.Draw(renderer);
            CreatePreconditionsForSimulatingTheModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _simulationTimer.Enabled = true;
        }

        private void Simulate()
        {
            if (_addHeat.Count < 1) return;
            _addHeat.Dequeue()();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _simulationTimer.Enabled = false;
            Reset();
        }
    }
}
