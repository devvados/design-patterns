using System;
using System.Windows;
using System.Windows.Shapes;

namespace Drawer
{
    public abstract class Helper
    {
        public Helper Successor { get; set; }
        public abstract bool HandleRequest(Shape s);
    }

    public class UnSelectedShapeHandler : Helper
    {
        public override bool HandleRequest(Shape s)
        {
            var success = false;
            try
            {
                if (s == null)
                {
                    throw new Exception("Не выбрана фигура для отрисовки!");
                }
                else if (Successor != null)
                {
                    success = Successor.HandleRequest(s);
                    //success = true;
                }
                else
                {
                    success = true;
                }
                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return success;
        }
    }

    public class BadShapeDimensionsHanadler : Helper
    {
        public override bool HandleRequest(Shape s)
        {
            var success = false;
            try
            {
                if (s != null)
                {
                    if (double.IsNaN(s.Height) || double.IsNaN(s.Width))
                    {
                        throw new Exception("Не указаны размеры фигуры!");
                    }
                    else if (Math.Abs(s.Height) < 0.0001 || Math.Abs(s.Width) < 0.0001)
                    {
                        throw new Exception("Размеры фигуры не могут быть нулевыми!");
                    }
                    else
                        success = true;
                }
                else if (Successor != null)
                {
                    success = Successor.HandleRequest(s);
                    //success = true;
                }
                return success;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return success;
        }
    }
}
