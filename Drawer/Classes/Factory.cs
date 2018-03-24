using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    abstract class Creator
    {
        public Command CommandToExecute;
        public abstract void Draw();
    }

    class RectangleCreator : Creator
    {
        public RectangleCreator(Command com)
        {
            CommandToExecute = com;
        }
        public override void Draw()
        {
            CommandToExecute.Execute();
        }
    }

    class EllipseCreator : Creator
    {
        public EllipseCreator(Command com)
        {
            CommandToExecute = com;
        }
        public override void Draw()
        {
            CommandToExecute.Execute();
        }
    }

    class TriangleCreator : Creator
    {
        public TriangleCreator(Command com)
        {
            CommandToExecute = com;
        }
        public override void Draw()
        {
            CommandToExecute.Execute();
        }
    }
}
