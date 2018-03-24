using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using System.Windows.Forms;

namespace Drawer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Shape Figure;

        private readonly List<Command> _commands = new List<Command>();
        private Command _currentCommand;
        private int _commandCounter = -1;

        Creator _creator;

        public MainWindow()
        {
            InitializeComponent();

            Command.Canvas = canvas;
        }

        private void MIDraw_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new Figure()
            {
                Owner = this
            };
            wnd.ShowDialog();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var start = e.GetPosition(canvas);

            if (Figure is Ellipse)
            {
                _currentCommand = new EllipseCommand(Figure, start);
                _creator = new EllipseCreator(_currentCommand);
            }
            else if (Figure is Rectangle)
            {
                _currentCommand = new EllipseCommand(Figure, start);
                _creator = new RectangleCreator(_currentCommand);
            }
            else //(figure is Triangle)
            {
                _currentCommand = new TriangleCommand(Figure, start);
                _creator = new TriangleCreator(_currentCommand);
            }

            while (_commands.Count - _commandCounter > 1)
                _commands.RemoveAt(_commandCounter + 1);

            _creator.Draw();

            _commands.Add(_currentCommand);
            _commandCounter++;

            NUDShapeThickness.IsEnabled = true;
            BSelectColor.IsEnabled = true;
            BSelectThickness.IsEnabled = true;
        }

        private void BClsCanvas_Click(object sender, RoutedEventArgs e)
        {
            _currentCommand = new ClearCanvasCommand();

            while (_commands.Count - _commandCounter > 1)
                _commands.RemoveAt(_commandCounter + 1);

            _currentCommand.Execute();
            _commands.Add(_currentCommand);
            _commandCounter++;

            NUDShapeThickness.IsEnabled = false;
            BSelectColor.IsEnabled = false;
            BSelectThickness.IsEnabled = false;
        }

        private void MIAbout_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new About();
            wnd.ShowDialog();
        }

        private void BUndo_Click(object sender, RoutedEventArgs e)
        {
            if (_commandCounter > -1 && (_commands.Count - _commandCounter) < 4)
            {
                _commands[_commandCounter].UnExecute();
                _commandCounter--;
            }

            if (!canvas.Children.OfType<Shape>().Any())
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
            else
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
        }

        private void BRedo_Click(object sender, RoutedEventArgs e)
        {
            if ((_commands.Count - _commandCounter < 5) && (_commands.Count - _commandCounter > 1))
            {
                _commandCounter++;
                _commands[_commandCounter].Execute();
            }
            if (canvas.Children.OfType<Shape>().Any())
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
            else
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
        }

        private void BSelectThickness_Click(object sender, RoutedEventArgs e)
        {
            var oldThickness = canvas.Children.OfType<Shape>().First().StrokeThickness;
            var newThickness = NUDShapeThickness.Value ?? 0;

            _currentCommand = new ThicknessCommand(newThickness, oldThickness);

            while (_commands.Count - _commandCounter > 1)
                _commands.RemoveAt(_commandCounter + 1);
            _currentCommand.Execute();
            _commands.Add(_currentCommand);
            _commandCounter++;

        }

        private void BSelectColor_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ColorDialog();
            var color = new Color();

            wnd.ShowDialog();

            if (wnd.Color != null)
            {
                var color1 = wnd.Color;
                color.A = color1.A;
                color.R = color1.R;
                color.G = color1.G;
                color.B = color1.B;

                Brush newBrush = new SolidColorBrush(color);
                var oldBrush = canvas.Children.OfType<Shape>().First().Stroke;

                CanvasLinesColor.Background = newBrush;

                _currentCommand = new ColorCommand(newBrush, oldBrush);

                while (_commands.Count - _commandCounter > 1)
                    _commands.RemoveAt(_commandCounter + 1);
                _currentCommand.Execute();
                _commands.Add(_currentCommand);
                _commandCounter++;
            }
        }

        private void BMacroCommand_Click(object sender, RoutedEventArgs e)
        {
            double t = 1;
            Brush brush = Brushes.Black;

            var coms = new List<Command>
            {
                new RectangleCommand(
                    new Rectangle { Height = 40, Width = 60, StrokeThickness = t, Stroke = brush },
                    new Point(150, 100)
                    ),
                new EllipseCommand(
                    new Ellipse { Height = 20, Width = 100, StrokeThickness = t, Stroke = brush },
                    new Point(150, 300)
                    ),
                new TriangleCommand(
                    new Triangle { Height = 20, Width = 40, StrokeThickness = t, Stroke = brush },
                    new Point(250, 250)
                    ),
                new ThicknessCommand(4, t),
                new ColorCommand(Brushes.Green, brush)
            };
            Command macroCommand = new MacroCommand(coms);

            while (_commands.Count - _commandCounter > 1)
                _commands.RemoveAt(_commandCounter + 1);

            macroCommand.Execute();
            _commands.Add(macroCommand);
            _commandCounter++;

            if (canvas.Children.OfType<Shape>().Any())
            {
                NUDShapeThickness.IsEnabled = true;
                BSelectColor.IsEnabled = true;
                BSelectThickness.IsEnabled = true;
            }
            else
            {
                NUDShapeThickness.IsEnabled = false;
                BSelectColor.IsEnabled = false;
                BSelectThickness.IsEnabled = false;
            }
        }
    }
}
