using M4Graphs.Core.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace M4Graphs.Core.Converters.Graphml
{
    /// <summary>
    /// Graphml root.
    /// </summary>
    [XmlRoot("graphml")]
    public class GraphmlRoot
    {
        /// <summary>
        /// Graph element.
        /// </summary>
        [XmlElement("graph")]
        public GraphmlGraph Graph { get; set; }
    }

    /// <summary>
    /// Graphml graph element.
    /// </summary>
    public class GraphmlGraph
    {
        /// <summary>
        /// Node elements.
        /// </summary>
        [XmlElement("node")]
        public List<GraphmlNode> Nodes { get; set; }
        /// <summary>
        /// Edge elements.
        /// </summary>
        [XmlElement("edge")]
        public List<GraphmlEdge> Edges { get; set; }
    }

    /// <summary>
    /// Graphml node element.
    /// </summary>
    public class GraphmlNode
    {
        /// <summary>
        /// Id attribute.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// Data element.
        /// </summary>
        [XmlElement("data")]
        public List<GraphmlData> Data { get; set; }
    }

    /// <summary>
    /// Graphml edge element.
    /// </summary>
    public class GraphmlEdge
    {
        /// <summary>
        /// Id attribute.
        /// </summary>
        [XmlAttribute("id")]
        public string Id { get; set; }
        /// <summary>
        /// Source id attribute.
        /// </summary>
        [XmlAttribute("source")]
        public string SourceId { get; set; }
        /// <summary>
        /// Target id attribute.
        /// </summary>
        [XmlAttribute("target")]
        public string TargetId { get; set; }
        /// <summary>
        /// Data element.
        /// </summary>
        [XmlElement("data")]
        public List<GraphmlData> Data { get; set; }
    }

    /// <summary>
    /// Graphml data element.
    /// </summary>
    public class GraphmlData
    {
        /// <summary>
        /// ShapeNode element.
        /// </summary>
        [XmlElement("ShapeNode", Namespace = Graphml.Namespace_y)]
        public GraphmlShapeNode ShapeNode { get; set; }

        /// <summary>
        /// PolyLineEdge element.
        /// </summary>
        [XmlElement("PolyLineEdge", Namespace = Graphml.Namespace_y)]
        public GraphmlPolyLineEdge PolyLineEdge { get; set; }

        /// <summary>
        /// GenericNode element.
        /// </summary>
        [XmlElement("GenericNode", Namespace = Graphml.Namespace_y)]
        public GraphmlGenericNode GenericNode { get; set; }
    }

    /// <summary>
    /// Graphml ShapeNode element.
    /// </summary>
    public class GraphmlShapeNode
    {
        /// <summary>
        /// Geometry element.
        /// </summary>
        [XmlElement("Geometry", Namespace = Graphml.Namespace_y)]
        public GraphmlGeometry Geometry { get; set; }
        /// <summary>
        /// Fill element.
        /// </summary>
        [XmlElement("Fill", Namespace = Graphml.Namespace_y)]
        public GraphmlFill Fill { get; set; }
        /// <summary>
        /// BorderStyle element.
        /// </summary>
        [XmlElement("BorderStyle", Namespace = Graphml.Namespace_y)]
        public GraphmlBorderStyle BorderStyle { get; set; }
        /// <summary>
        /// NodeLabel element.
        /// </summary>
        [XmlElement("NodeLabel", Namespace = Graphml.Namespace_y)]
        public GraphmlNodeLabel NodeLabel { get; set; }
        /// <summary>
        /// Shape element.
        /// </summary>
        [XmlElement("Shape", Namespace = Graphml.Namespace_y)]
        public GraphmlShape Shape { get; set; }
    }

    /// <summary>
    /// Graphml GenericNode element.
    /// </summary>
    public class GraphmlGenericNode
    {
        /// <summary>
        /// Configuration attribute.
        /// </summary>
        [XmlAttribute("configuration")]
        public string Configuration { get; set; }

        /// <summary>
        /// Geometry element.
        /// </summary>
        [XmlElement("Geometry", Namespace = Graphml.Namespace_y)]
        public GraphmlGeometry Geometry { get; set; }
        /// <summary>
        /// Fill element.
        /// </summary>
        [XmlElement("Fill", Namespace = Graphml.Namespace_y)]
        public GraphmlFill Fill { get; set; }
        /// <summary>
        /// BorderStyle element.
        /// </summary>
        [XmlElement("BorderStyle", Namespace = Graphml.Namespace_y)]
        public GraphmlBorderStyle BorderStyle { get; set; }
        /// <summary>
        /// NodeLabel element.
        /// </summary>
        [XmlElement("NodeLabel", Namespace = Graphml.Namespace_y)]
        public GraphmlNodeLabel NodeLabel { get; set; }
    }

    /// <summary>
    /// Graphml geometry element.
    /// </summary>
    public class GraphmlGeometry
    {
        /// <summary>
        /// Height attribute.
        /// </summary>
        [XmlAttribute("height")]
        public double Height { get; set; }
        /// <summary>
        /// Width attribute.
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
        /// <summary>
        /// X attribute.
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; }
        /// <summary>
        /// Y attribute.
        /// </summary>
        [XmlAttribute("y")]
        public double Y { get; set; }
    }

    /// <summary>
    /// Graphml fill element.
    /// </summary>
    public class GraphmlFill
    {
        /// <summary>
        /// Color attribute.
        /// </summary>
        [XmlAttribute("color")]
        public string Color { get; set; }
        /// <summary>
        /// Transparent attribute.
        /// </summary>
        [XmlAttribute("transparent")]
        public bool Transparent { get; set; }
    }

    /// <summary>
    /// Graphml BorderStyle element.
    /// </summary>
    public class GraphmlBorderStyle
    {
        /// <summary>
        /// HasColor attribute.
        /// </summary>
        [XmlAttribute("hasColor")]
        public bool HasColor { get; set; }
        /// <summary>
        /// Color attribute.
        /// </summary>
        [XmlAttribute("color")]
        public string Color { get; set; }
        /// <summary>
        /// Type attribute.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
        /// <summary>
        /// Width attribute.
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
    }

    /// <summary>
    /// Graphml NodeLabel element.
    /// </summary>
    public class GraphmlNodeLabel
    {
        /// <summary>
        /// Alignment attribute.
        /// </summary>
        [XmlAttribute("alignment")]
        public string Alignment { get; set; }
        /// <summary>
        /// AutoSizePolicy attribute.
        /// </summary>
        [XmlAttribute("autoSizePolicy")]
        public string AutoSizePolicy { get; set; }
        /// <summary>
        /// FontFamily attribute.
        /// </summary>
        [XmlAttribute("fontFamily")]
        public string FontFamily { get; set; }
        /// <summary>
        /// FontSize attribute.
        /// </summary>
        [XmlAttribute("fontSize")]
        public int FontSize { get; set; }
        /// <summary>
        /// FontStyle attribute.
        /// </summary>
        [XmlAttribute("fontStyle")]
        public string FontStyle { get; set; }
        /// <summary>
        /// HasBackgroundColor attribute.
        /// </summary>
        [XmlAttribute("hasBackgroundColor")]
        public bool HasBackgroundColor { get; set; }
        /// <summary>
        /// HasLineColor attribute.
        /// </summary>
        [XmlAttribute("hasLineColor")]
        public bool HasLineColor { get; set; }
        /// <summary>
        /// Height attribute.
        /// </summary>
        [XmlAttribute("height")]
        public double Height { get; set; }
        /// <summary>
        /// HorizontalTextPosition attribute.
        /// </summary>
        [XmlAttribute("horizontalTextPosition")]
        public string HorizontalTextPosition { get; set; }
        /// <summary>
        /// IconTextGap attribute.
        /// </summary>
        [XmlAttribute("iconTextGap")]
        public int IconTextGap { get; set; }
        /// <summary>
        /// ModelName attribute.
        /// </summary>
        [XmlAttribute("modelName")]
        public string ModelName { get; set; }
        /// <summary>
        /// TextColor attribute.
        /// </summary>
        [XmlAttribute("textColor")]
        public string TextColor { get; set; }
        /// <summary>
        /// VerticalTextPosition attribute.
        /// </summary>
        [XmlAttribute("verticalTextPosition")]
        public string VerticalTextPosition { get; set; }
        /// <summary>
        /// Visible attribute.
        /// </summary>
        [XmlAttribute("visible")]
        public bool Visible { get; set; }
        /// <summary>
        /// Width attribute.
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
        /// <summary>
        /// X attribute.
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; }
        /// <summary>
        /// Y attribute.
        /// </summary>
        [XmlAttribute("y")]
        public double Y { get; set; }

        /// <summary>
        /// Text content of NodeLabel.
        /// </summary>
        [XmlText]
        public string Text;

        /// <summary>
        /// LabelModel element.
        /// </summary>
        [XmlElement("LabelModel", Namespace = Graphml.Namespace_y)]
        public GraphmlNodeLabelModel LabelModel { get; set; }

        /// <summary>
        /// ModelParameter element.
        /// </summary>
        [XmlElement("ModelParameter", Namespace = Graphml.Namespace_y)]
        public GraphmlNodeModelParameter ModelParameter { get; set; }
    }

    /// <summary>
    /// Graphml LabelModel element (node).
    /// </summary>
    public class GraphmlNodeLabelModel
    {
        /// <summary>
        /// SmartNodeLabelModel element.
        /// </summary>
        [XmlElement("SmartNodeLabelModel", Namespace = Graphml.Namespace_y)]
        public GraphmlSmartNodeLabelModel SmartNodeLabelModel { get; set; }
    }

    /// <summary>
    /// Graphml SmartNodeLabelModel element.
    /// </summary>
    public class GraphmlSmartNodeLabelModel
    {
        /// <summary>
        /// Distance attribute.
        /// </summary>
        [XmlAttribute("distance")]
        public double Distance { get; set; }
    }

    /// <summary>
    /// Graphml ModelParameter element (node).
    /// </summary>
    public class GraphmlNodeModelParameter
    {
        /// <summary>
        /// SmartNodeLabelModelParameter element.
        /// </summary>
        [XmlElement("SmartNodeLabelModelParameter", Namespace = Graphml.Namespace_y)]
        public GraphmlSmartNodeLabelModelParameter SmartNodeLabelModelParameter { get; set; }
    }

    /// <summary>
    /// Graphml SmartNodeLabelModelParameter element.
    /// </summary>
    public class GraphmlSmartNodeLabelModelParameter
    {
        /// <summary>
        /// LabelRatioX attribute.
        /// </summary>
        [XmlAttribute("labelRatioX")]
        public double LabelRatioX { get; set; }
        /// <summary>
        /// LabelRatioY attribute.
        /// </summary>
        [XmlAttribute("labelRatioY")]
        public double LabelRatioY { get; set; }
        /// <summary>
        /// NodeRatioX attribute.
        /// </summary>
        [XmlAttribute("nodeRatioX")]
        public double NodeRatioX { get; set; }
        /// <summary>
        /// NodeRatioY attribute.
        /// </summary>
        [XmlAttribute("nodeRatioY")]
        public double NodeRatioY { get; set; }
        /// <summary>
        /// OffsetX attribute.
        /// </summary>
        [XmlAttribute("offsetX")]
        public double OffsetX { get; set; }
        /// <summary>
        /// OffsetY attribute.
        /// </summary>
        [XmlAttribute("offsetY")]
        public double OffsetY { get; set; }
        /// <summary>
        /// UpX attribute.
        /// </summary>
        [XmlAttribute("upX")]
        public double UpX { get; set; }
        /// <summary>
        /// UpY attribute.
        /// </summary>
        [XmlAttribute("upY")]
        public double UpY { get; set; }
    }

    /// <summary>
    /// Graphml shape element.
    /// </summary>
    public class GraphmlShape
    {
        /// <summary>
        /// Type attribute.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
    }

    /// <summary>
    /// Graphml PolyLineEdge element.
    /// </summary>
    public class GraphmlPolyLineEdge
    {
        /// <summary>
        /// Path element.
        /// </summary>
        [XmlElement("Path", Namespace = Graphml.Namespace_y)]
        public GraphmlPath Path { get; set; }
        /// <summary>
        /// LineStyle element.
        /// </summary>
        [XmlElement("LineStyle", Namespace = Graphml.Namespace_y)]
        public GraphmlLineStyle LineStyle { get; set; }
        /// <summary>
        /// Arrows element.
        /// </summary>
        [XmlElement("Arrows", Namespace = Graphml.Namespace_y)]
        public GraphmlArrows Arrows { get; set; }
        /// <summary>
        /// EdgeLabel element.
        /// </summary>
        [XmlElement("EdgeLabel", Namespace = Graphml.Namespace_y)]
        public GraphmlEdgeLabel EdgeLabel { get; set; }
        /// <summary>
        /// BendStyle element.
        /// </summary>
        [XmlElement("BendStyle", Namespace = Graphml.Namespace_y)]
        public GraphmlBendStyle BendStyle { get; set; }
    }

    /// <summary>
    /// Graphml EdgeLabel element.
    /// </summary>
    public class GraphmlEdgeLabel
    {
        /// <summary>
        /// Alignment attribute.
        /// </summary>
        [XmlAttribute("alignment")]
        public string Alignment { get; set; }
        /// <summary>
        /// Configuration attribute.
        /// </summary>
        [XmlAttribute("configuration")]
        public string Configuration { get; set; }
        /// <summary>
        /// Distance attribute.
        /// </summary>
        [XmlAttribute("distance")]
        public double Distance { get; set; }
        /// <summary>
        /// FontFamily attribute.
        /// </summary>
        [XmlAttribute("fontFamily")]
        public string FontFamily { get; set; }
        /// <summary>
        /// FontSize attribute.
        /// </summary>
        [XmlAttribute("fontSize")]
        public int FontSize { get; set; }
        /// <summary>
        /// FontStyle attribute.
        /// </summary>
        [XmlAttribute("fontStyle")]
        public string FontStyle { get; set; }
        /// <summary>
        /// HasBackgroundColor attribute.
        /// </summary>
        [XmlAttribute("hasBackgroundColor")]
        public bool HasBackgroundColor { get; set; }
        /// <summary>
        /// HasLineColor attribute.
        /// </summary>
        [XmlAttribute("hasLineColor")]
        public bool HasLineColor { get; set; }
        /// <summary>
        /// Height attribute.
        /// </summary>
        [XmlAttribute("height")]
        public double Height { get; set; }
        /// <summary>
        /// HorizontalTextPosition attribute.
        /// </summary>
        [XmlAttribute("horizontalTextPosition")]
        public string HorizontalTextPosition { get; set; }
        /// <summary>
        /// IconTextGap attribute.
        /// </summary>
        [XmlAttribute("iconTextGap")]
        public int IconTextGap { get; set; }
        /// <summary>
        /// ModelName attribute.
        /// </summary>
        [XmlAttribute("modelName")]
        public string ModelName { get; set; }
        /// <summary>
        /// PreferredPlacement attribute.
        /// </summary>
        [XmlAttribute("preferredPlacement")]
        public string PreferredPlacement { get; set; }
        /// <summary>
        /// Ratio attribute.
        /// </summary>
        [XmlAttribute("ratio")]
        public double Ratio { get; set; }
        /// <summary>
        /// TextColor attribute.
        /// </summary>
        [XmlAttribute("textColor")]
        public string TextColor { get; set; }
        /// <summary>
        /// VerticalTextPosition attribute.
        /// </summary>
        [XmlAttribute("verticalTextPosition")]
        public string VerticalTextPosition { get; set; }
        /// <summary>
        /// Visible attribute.
        /// </summary>
        [XmlAttribute("visible")]
        public bool Visible { get; set; }
        /// <summary>
        /// Width attribute.
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
        /// <summary>
        /// X attribute.
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; }
        /// <summary>
        /// Y attribute.
        /// </summary>
        [XmlAttribute("y")]
        public double Y { get; set; }

        /// <summary>
        /// Text content of EdgeLabel.
        /// </summary>
        [XmlText]
        public string Text;

        /// <summary>
        /// LabelModel element.
        /// </summary>
        [XmlElement("LabelModel", Namespace = Graphml.Namespace_y)]
        public GraphmlEdgeLabelModel LabelModel { get; set; }

        /// <summary>
        /// ModelParameter element.
        /// </summary>
        [XmlElement("ModelParameter", Namespace = Graphml.Namespace_y)]
        public GraphmlEdgeModelParameter ModelParameter { get; set; }

        /// <summary>
        /// PreferredPlacementDescriptor element.
        /// </summary>
        [XmlElement("PreferredPlacementDescriptor", Namespace = Graphml.Namespace_y)]
        public GraphmlPreferredPlacementDescriptor PreferredPlacementDescriptor { get; set; }
    }

    /// <summary>
    /// Graphml PreferredPlacementDescriptor element.
    /// </summary>
    public class GraphmlPreferredPlacementDescriptor
    {
        /// <summary>
        /// Angle attribute.
        /// </summary>
        [XmlAttribute("angle")]
        public double Angle { get; set; }
        /// <summary>
        /// AngleOffsetOnRightSide attribute.
        /// </summary>
        [XmlAttribute("angleOffsetOnRightSide")]
        public int AngleOffsetOnRightSide { get; set; }
        /// <summary>
        /// AngleReference attribute.
        /// </summary>
        [XmlAttribute("angleReference")]
        public string AngleReference { get; set; }
        /// <summary>
        /// AngleRotationOnRightSide attribute.
        /// </summary>
        [XmlAttribute("angleRotationOnRightSide")]
        public string AngleRotationOnRightSide { get; set; }
        /// <summary>
        /// Distance attribute.
        /// </summary>
        [XmlAttribute("distance")]
        public double Distance { get; set; }
        /// <summary>
        /// Frozen attribute.
        /// </summary>
        [XmlAttribute("frozen")]
        public bool Frozen { get; set; }
        /// <summary>
        /// Placement attribute.
        /// </summary>
        [XmlAttribute("placement")]
        public string Placement { get; set; }
        /// <summary>
        /// Side attribute.
        /// </summary>
        [XmlAttribute("side")]
        public string Side { get; set; }
        /// <summary>
        /// SideReference attribute.
        /// </summary>
        [XmlAttribute("sideReference")]
        public string SideReference { get; set; }
    }

    /// <summary>
    /// Graphml LabelModel element (edge).
    /// </summary>
    public class GraphmlEdgeLabelModel
    {
        /// <summary>
        /// SmartEdgeLabelModel element.
        /// </summary>
        [XmlElement("SmartEdgeLabelModel", Namespace = Graphml.Namespace_y)]
        public GraphmlSmartEdgeLabelModel SmartEdgeLabelModel { get; set; }
    }

    /// <summary>
    /// Graphml SmartEdgeLabelModel element.
    /// </summary>
    public class GraphmlSmartEdgeLabelModel
    {
        /// <summary>
        /// AutoRotationEnabled attribute.
        /// </summary>
        [XmlAttribute("autoRotationEnabled")]
        public bool AutoRotationEnabled { get; set; }
        /// <summary>
        /// DefaultAngle attribute.
        /// </summary>
        [XmlAttribute("defaultAngle")]
        public double DefaultAngle { get; set; }
        /// <summary>
        /// DefaultDistance attribute.
        /// </summary>
        [XmlAttribute("defaultDistance")]
        public double DefaultDistance { get; set; }

    }

    /// <summary>
    /// Graphml ModelParameter element (edge).
    /// </summary>
    public class GraphmlEdgeModelParameter
    {
        /// <summary>
        /// SmartEdgeLabelModelParameter element.
        /// </summary>
        [XmlElement("SmartEdgeLabelModelParameter", Namespace = Graphml.Namespace_y)]
        public GraphmlSmartEdgeLabelModelParameter SmartEdgeLabelModelParameter { get; set; }
    }

    /// <summary>
    /// Graphml SmartEdgeLabelModelParameter element.
    /// </summary>
    public class GraphmlSmartEdgeLabelModelParameter
    {
        /// <summary>
        /// Angle attribute.
        /// </summary>
        [XmlAttribute("angle")]
        public double Angle { get; set; }
        /// <summary>
        /// Distance attribute.
        /// </summary>
        [XmlAttribute("distance")]
        public double Distance { get; set; }
        /// <summary>
        /// DistanceToCenter attribute.
        /// </summary>
        [XmlAttribute("distanceToCenter")]
        public bool DistanceToCenter { get; set; }
        /// <summary>
        /// Position attribute.
        /// </summary>
        [XmlAttribute("position")]
        public string Position { get; set; }
        /// <summary>
        /// Ratio attribute.
        /// </summary>
        [XmlAttribute("ratio")]
        public double Ratio { get; set; }
        /// <summary>
        /// Segment attribute.
        /// </summary>
        [XmlAttribute("segment")]
        public int Segment { get; set; }
    }

    /// <summary>
    /// Graphml BendStyle element.
    /// </summary>
    public class GraphmlBendStyle
    {
        /// <summary>
        /// Smoothed attribute.
        /// </summary>
        [XmlAttribute("smoothed")]
        public bool Smoothed { get; set; }
    }

    /// <summary>
    /// Graphml Arrows element.
    /// </summary>
    public class GraphmlArrows
    {
        /// <summary>
        /// Source attribute.
        /// </summary>
        [XmlAttribute("source")]
        public string Source { get; set; }
        /// <summary>
        /// Target attribute.
        /// </summary>
        [XmlAttribute("target")]
        public string Target { get; set; }
    }

    /// <summary>
    /// Graphml LineStyle element.
    /// </summary>
    public class GraphmlLineStyle
    {
        /// <summary>
        /// Color attribute.
        /// </summary>
        [XmlAttribute("color")]
        public string Color { get; set; }
        /// <summary>
        /// Type attribute.
        /// </summary>
        [XmlAttribute("type")]
        public string Type { get; set; }
        /// <summary>
        /// Width attribute.
        /// </summary>
        [XmlAttribute("width")]
        public double Width { get; set; }
    }

    /// <summary>
    /// Graphml Path element.
    /// </summary>
    public class GraphmlPath
    {
        /// <summary>
        /// SourceX attribute.
        /// </summary>
        [XmlAttribute("sx")]
        public double SourceX { get; set; }
        /// <summary>
        /// SourceY attribute.
        /// </summary>
        [XmlAttribute("sy")]
        public double SourceY { get; set; }
        /// <summary>
        /// TargetX attribute.
        /// </summary>
        [XmlAttribute("tx")]
        public double TargetX { get; set; }
        /// <summary>
        /// TargetY attribute.
        /// </summary>
        [XmlAttribute("ty")]
        public double TargetY { get; set; }

        /// <summary>
        /// Point elements.
        /// </summary>
        [XmlElement("Point", Namespace = Graphml.Namespace_y)]
        public List<GraphmlPoint> Points { get; set; }

        /// <summary>
        /// Returns all points, or an empty list if no points exist.
        /// </summary>
        /// <returns></returns>
        public List<PathPoint> GetPathPoints()
        {
            if (!(Points?.Any() ?? false))
                return new List<PathPoint>();
            {
                return Points.Select(point => new PathPoint(point.X, point.Y)).ToList();
            }
        }
    }

    /// <summary>
    /// Graphml Point element.
    /// </summary>
    public class GraphmlPoint
    {
        /// <summary>
        /// X attribute.
        /// </summary>
        [XmlAttribute("x")]
        public double X { get; set; }
        /// <summary>
        /// Y attribute.
        /// </summary>
        [XmlAttribute("y")]
        public double Y { get; set; }
    }
}
