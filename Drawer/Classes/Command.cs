using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawer
{
    abstract class Command
    {
        protected Shape Fig;
        protected Point Point;
        public static Canvas Canvas;

        public abstract void Execute();
        public abstract void UnExecute();
    }

    class EllipseCommand : Command
    {
        private readonly Shape _ellipse;

        public EllipseCommand(Shape s, Point p)
        {
            _ellipse = new Ellipse
            {
                Width = s.Width,
                Height = s.Height,
                StrokeThickness = s.StrokeThickness,
                Stroke = s.Stroke
            };
            Canvas.SetLeft(_ellipse, p.X - s.Width / 2);
            Canvas.SetTop(_ellipse, p.Y - s.Height / 2);
        }

        public override void Execute()
        {
            Canvas.Children.Add(_ellipse);
        }

        public override void UnExecute()
        {
            Canvas.Children.Remove(_ellipse);
        }
    }

    class TriangleCommand : Command
    {
        private readonly Polygon _poly;

        public TriangleCommand(Shape s, Point p)
        {
            var p1 = new Point(p.X, p.Y - s.Height / 2);
            var p2 = new Point(p.X - s.Width / 2, p.Y + s.Height / 2);
            var p3 = new Point(p.X + s.Width / 2, p.Y + s.Height / 2);

            var pc = new PointCollection(3) {p1, p2, p3};

            _poly = new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Points = pc
            };
        }

        public override void Execute()
        {
            Canvas.Children.Add(_poly);
        }

        public override void UnExecute()
        {
            Canvas.Children.Remove(_poly);
        }
    }

    class RectangleCommand : Command
    {
        private readonly Shape _rect;

        public RectangleCommand(Shape s, Point p)
        {
            _rect = new Rectangle
            {
                Width = s.Width,
                Height = s.Height,
                StrokeThickness = s.StrokeThickness,
                Stroke = s.Stroke
            };
            Canvas.SetLeft(_rect, p.X - s.Width / 2);
            Canvas.SetTop(_rect, p.Y - s.Height / 2);
        }

        public override void Execute()
        {
            Canvas.Children.Add(_rect);
        }

        public override void UnExecute()
        {
            Canvas.Children.Remove(_rect);
        }
    }

    class ThicknessCommand : Command
    {
        private readonly double _newThickness;
        private readonly double _oldThickness;

        public ThicknessCommand(double t1, double t2)
        {
            _newThickness = t1;
            _oldThickness = t2;
        }

        public override void Execute()
        {
            foreach (var item in Canvas.Children.OfType<Shape>())
            {
                item.StrokeThickness = _newThickness;
            }
            Canvas.InvalidateVisual();
        }

        public override void UnExecute()
        {
            foreach (var item in Canvas.Children.OfType<Shape>())
            {
                item.StrokeThickness = _oldThickness;
            }
            Canvas.InvalidateVisual();
        }
    }

    class ColorCommand : Command
    {
        private readonly Brush _newBrush;
        private readonly Brush _oldBrush;

        public ColorCommand(Brush b1, Brush b2)
        {
            _newBrush = b1;
            _oldBrush = b2;
        }

        public override void Execute()
        {
            foreach (var item in Canvas.Children.OfType<Shape>())
            {
                item.Stroke = _newBrush;
            }
            Canvas.InvalidateVisual();
        }

        public override void UnExecute()
        {
            foreach (var item in Canvas.Children.OfType<Shape>())
            {
                item.Stroke = _oldBrush;
            }
            Canvas.InvalidateVisual();
        }
    }

    class ClearCanvasCommand : Command
    {
        private readonly List<Shape> _shapesOnCanvas = new List<Shape>();

        public override void Execute()
        {
            _shapesOnCanvas.AddRange(Canvas.Children.OfType<Shape>());
            Canvas.Children.Clear();
        }

        public override void UnExecute()
        {
            foreach (var item in _shapesOnCanvas)
            {
                Canvas.Children.Add(item);
            }
        }
    }

    class MacroCommand : Command
    {
        private readonly List<Command> _commands;

        public MacroCommand(List<Command> coms)
        {
            _commands = coms;
        }

        public override void Execute()
        {
            foreach (var com in _commands)
            {
                com.Execute();
            }
        }

        public override void UnExecute()
        {
            foreach (var com in _commands)
            {
                com.UnExecute();
            }
        }
    }
}
