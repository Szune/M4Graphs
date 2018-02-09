using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace M4Graphs.Wpf.Shapes
{
    public sealed class PolylineArrow : Shape
    {
        public static readonly DependencyProperty PointsProperty = DependencyProperty.Register("Points", typeof(PointCollection), typeof(PolylineArrow), new FrameworkPropertyMetadata(new PointCollection(), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty FillRuleProperty = DependencyProperty.Register("FillRule", typeof(FillRule), typeof(PolylineArrow), new FrameworkPropertyMetadata(FillRule.EvenOdd, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty HeadHeightProperty = DependencyProperty.Register("HeadHeight", typeof(double), typeof(PolylineArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));
        public static readonly DependencyProperty HeadWidthProperty = DependencyProperty.Register("HeadWidth", typeof(double), typeof(PolylineArrow), new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.AffectsMeasure));

        public PolylineArrow()
        {

        }

        public double HeadHeight
        {
            get
            {
                return (double)GetValue(HeadHeightProperty);
            }
            set
            {
                SetValue(HeadHeightProperty, value);
            }
        }
        public double HeadWidth
        {
            get
            {
                return (double)GetValue(HeadWidthProperty);
            }
            set
            {
                SetValue(HeadWidthProperty, value);
            }
        }

        public PointCollection Points
        {
            get
            {
                return (PointCollection)GetValue(PointsProperty);
            }
            set
            {
                SetValue(PointsProperty, value);
            }
        }

        public FillRule FillRule
        {
            get
            {
                return (FillRule)GetValue(FillRuleProperty);
            }
            set
            {
                SetValue(FillRuleProperty, value);
            }
        }

        protected override Geometry DefiningGeometry { get
            {
                StreamGeometry geometry = new StreamGeometry();
                geometry.FillRule = FillRule;

                using (var context = geometry.Open())
                {
                    GetGeometry(context);
                }
                // Freeze the geometry for performance benefits

                geometry.Freeze();

                return geometry;
            }
        }

        private void GetGeometry(StreamGeometryContext context)
        {
            if (Points.Count < 2)
            {
                if (!DesignerProperties.GetIsInDesignMode(this))
                    throw new InvalidOperationException("a polyline arrow requires at least 2 points");
                return;
            }

            context.BeginFigure(Points[0], true, false);
            foreach(var pt in Points)
            {
                context.LineTo(pt, true, true);
            }

            var nextLast = Points[Points.Count - 2];
            var last = Points.Last();
            var theta = Math.Atan2(nextLast.Y - last.Y, nextLast.X - last.X);
            var sint = Math.Sin(theta);
            var cost = Math.Cos(theta);
            var arrowLineLeft = new Point(last.X + (HeadWidth * cost - HeadHeight * sint),
                last.Y + (HeadWidth * sint + HeadHeight * cost));
            var arrowLineRight = new Point(last.X + (HeadWidth * cost + HeadHeight * sint),
                last.Y - (HeadHeight * cost - HeadWidth * sint));
            context.LineTo(arrowLineRight, true, true);
            context.LineTo(last, true, true);
            context.LineTo(arrowLineLeft, true, true);
        }
    }
}
