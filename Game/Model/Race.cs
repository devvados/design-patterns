using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Race
    {
        private string name;
        private List<Territory> territories = new List<Territory>();
        private List<Unit> units = new List<Unit>();

        public string Name { get => name; set => name = value; }
        internal List<Unit> Units { get => units; set => units = value; }
        internal List<Territory> Territories { get => territories; set => territories = value; }


        public Race(string name)
        {
            Name = name;
        }
    }
}
