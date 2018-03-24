using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Drawer
{
    /// <summary>
    /// Interaction logic for Figure.xaml
    /// </summary>
    public partial class Figure : System.Windows.Window
    {
        public Shape F;
        private readonly Helper _h1 = new BadShapeDimensionsHanadler();

        private readonly Helper _h2 = new UnSelectedShapeHandler();

        public Figure()
        {
            InitializeComponent();
        }

        private void BSettings_Click(object sender, RoutedEventArgs e)
        {
            //h1.Successor = h2;
            var success = _h2.HandleRequest(F);
            if (success)
            {
                var wnd = new Settings()
                {
                    Owner = this
                };
                wnd.ShowDialog();
            }
        }

        private void BOk_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWnd = this.Owner as MainWindow;
            if (mainWnd != null)
            {
                _h1.Successor = _h2;
                var success = _h1.HandleRequest(F);

                if (success)
                {
                    mainWnd.Figure = F;
                    this.Close();
                }
            }
        }

        private void CBFigure_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBFigure.SelectedIndex == 0)
                F = new Ellipse();
            if (CBFigure.SelectedIndex == 1)
                F = new Rectangle();
            if (CBFigure.SelectedIndex == 2)
                F = new Triangle();

            F.Stroke = Brushes.Black;
            F.StrokeThickness = 1;
        }
    }
}
