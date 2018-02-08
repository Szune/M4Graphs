using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using M4Graphs.Core;
using M4Graphs.Core.Geometry;
using M4Graphs.Core.Interfaces;
using M4Graphs.Parsers.Graphml.Elements;
using M4Graphs.Wpf.Components;

namespace M4Graphs.Wpf.Rendering
{
    public class GraphmlRenderer : IRenderer<GraphModel>
    {
        private readonly ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> _unmodifiedElements;
        private ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> _elements;
        private Coordinate _offset;
        private readonly Coordinate _lowestCoordinate;

        public GraphmlRenderer(ElementCollection<GraphmlNodeElement, GraphmlEdgeElement> unmodifiedElements)
        {
            _unmodifiedElements = unmodifiedElements;
            _offset = Coordinate.Zero;
            var lowX = -unmodifiedElements.Nodes.Min(n => n.X);
            var lowY = -unmodifiedElements.Nodes.Min(n => n.Y);
            _lowestCoordinate = new Coordinate(lowX, lowY);
            UpdateElements(_offset, _lowestCoordinate);
        }

        private void UpdateElements(Coordinate offset, Coordinate lowestCoordinate)
        {
            var nodes = _unmodifiedElements.Nodes.Select(node =>
                node.WithOffset(offset.X + lowestCoordinate.X, offset.Y + lowestCoordinate.Y)).ToDictionary(node => node.Id);
            var edges = _unmodifiedElements.Edges
                .Select(edge => edge.WithOffset(offset.X + lowestCoordinate.X, offset.Y + lowestCoordinate.Y))
                .ToDictionary(edge => edge.Id);
            _elements = new ElementCollection<GraphmlNodeElement, GraphmlEdgeElement>(nodes, edges);
        }

        public void SetOffset(Coordinate offset)
        {
            _offset = offset;
            UpdateElements(_offset, _lowestCoordinate);
        }

        public void RenderElements(GraphModel model)
        {
            foreach (var edge in _elements.Edges)
                DrawEdge(model, edge);
            foreach (var node in _elements.Nodes)
                DrawNode(model, node);
        }

        private void DrawEdge(GraphModel model, GraphmlEdgeElement edge)
        {
            var loadedPoints = CoordinatesToPointCollection(edge.Points);
            var source = _elements.GetNode(edge.SourceId);
            var target = _elements.GetNode(edge.TargetId);
            loadedPoints.Insert(0, new Point(source.CenterX, source.CenterY));
            Coordinate outside;
            if (loadedPoints.Count > 1)
            {
                // if there's more than 1 point, to be able to get the correct angle for the last point,
                // we need to use the current last point
                var nextLast = loadedPoints.Last();
                outside = target.GetPointOfEdgeCollision(new Coordinate(nextLast.X, nextLast.Y));
            }
            else
            {
                outside = target.GetPointOfEdgeCollision(new Coordinate(source.CenterX, source.CenterY));
            }
            loadedPoints.Add(outside.ToPoint());

            Edge newEdge;
            if (edge.Label != null)
            {
                var secondPoint = loadedPoints[1];
                var firstPoint = source.GetPointOfEdgeCollision(new Coordinate(secondPoint.X, secondPoint.Y));
                // get first x and y coordinates outside of source node
                var labelPoint = edge.Label.GetViewPosition(firstPoint.X, firstPoint.Y);
                newEdge = new Edge(edge.Id, edge.Text, loadedPoints, labelPoint.ToPoint());
            }
            else
                newEdge = new Edge(edge.Id, edge.Text, loadedPoints);

            model.AddElement(newEdge);
        }

        private void DrawNode(GraphModel model, GraphmlNodeElement node)
        {
            var newNode = new Node(node.Id, node.Text, node.X, node.Y, node.Width, node.Height);
            model.AddElement(newNode);
        }

        private PointCollection CoordinatesToPointCollection(List<Coordinate> points)
        {
            return new PointCollection(points.Select(point => new Point(point.X, point.Y)));
        }
    }
}
