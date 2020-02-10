using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SV_final.Model
{
    public class _Object
    {
        [XmlAttribute("Type")]
        public int Type { get; set; }

        [XmlAttribute("ShapeType")]
        public int ShapeType { get; set; }

        [XmlElement("Transform")]
        public _Transform Transform { get; set; }

        [XmlElement("Position")]
        public _Position Position { get; set; }

        [XmlElement("Points")]
        public _Points Points { get; set; }

        [XmlElement("Label")]
        public _Label Label { get; set; }

        public _Object() {
            this.Transform = new _Transform();
            this.Position = new _Position();
            this.Points = new _Points();
            this.Label = new _Label();
        }

        public _Object(int ShapeType, int Type,
                      double Height, double Width, int ShowRoof, int ExtendedPoint, int Side, int Direction,
                      int Bottom, int Right, int Top, int Left,
                      int y1, int x1, int y2, int x2,
                      String Text)
        {
            this.ShapeType = ShapeType;
            this.Type = Type;
            this.Transform = new _Transform(Height, Width, ShowRoof, ExtendedPoint, Side, Direction);
            this.Position = new _Position(Bottom, Right, Top, Left);
            this.Points = new _Points(y1, x1, y2, x2);
            this.Label = new _Label(Text);

        }

        public class _Transform
        {
            [XmlAttribute("Direction")]
            public int Direction { get; set; }
            [XmlAttribute("Side")]
            public int Side { get; set; }
            [XmlAttribute("ExtendedPoint")]
            public int ExtendedPoint { get; set; }
            [XmlAttribute("ShowRoof")]
            public int ShowRoof { get; set; }
            [XmlAttribute("Width")]
            public double Width { get; set; }
            [XmlAttribute("Height")]
            public double Height { get; set; }

            public _Transform() { }
            public _Transform(double Height, double Width, int ShowRoof, int ExtendedPoint, int Side, int Direction)
            {
                this.Height = Height;
                this.Width = Width;
                this.ShowRoof = ShowRoof;
                this.ExtendedPoint = ExtendedPoint;
                this.Side = Side;
                this.Direction = Direction;
            }
        }
        public class _Position
        {
            [XmlAttribute("Left")]
            public double Left { get; set; }
            [XmlAttribute("Top")]
            public double Top { get; set; }
            [XmlAttribute("Right")]
            public double Right { get; set; }
            [XmlAttribute("Bottom")]
            public double Bottom { get; set; }

            public _Position() { }
            public _Position(int Bottom, int Right, int Top, int Left)
            {
                this.Bottom = Bottom;
                this.Right = Right;
                this.Top = Top;
                this.Left = Left;
            }
        }
        public class _Points
        {
            [XmlElement("Point")]
            public List<_Point> Point { get; set; }
            public _Points() { }
            public _Points(int y1, int x1, int y2, int x2)
            {
                this.Point = new List<_Point>();
                this.Point.Add(new _Point(y1, x1));
                this.Point.Add(new _Point(y2, x2));
            }
        }
        public class _Point
        {
            [XmlAttribute("y")]
            public double y { get; set; }
            [XmlAttribute("x")]
            public double x { get; set; }

            public _Point() { }

            public _Point(int y, int x)
            {
                this.y = y;
                this.x = x;
            }
        }
        public class _Label
        {
            [XmlAttribute("Text")]
            public string Text { get; set; }
            public _Label() { }
            public _Label(string Text)
            {
                this.Text = Text;
            }
        }
    }
}