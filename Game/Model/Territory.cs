using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Territory
    {
        public Race Race;
        public uint MaxStructuresCount;
        public List<Structure> Structures = new List<Structure>();
        public string Name;
        static Random r = new Random();
        public Territory(string Name)
        {
            this.Name = Name;
            
            switch(r.Next(1,4))
            {
                case 1:
                    Structures.Add(new ArcherFactory() { Race = this.Race, Territory = this });
                    break;
                case 2:
                    Structures.Add(new PikemanFactory() { Race = this.Race, Territory = this });
                    break;
                case 3:
                    Structures.Add(new SwordsmanFactory() { Race = this.Race, Territory = this });
                    break;
            }
            MaxStructuresCount = (uint)r.Next(1, 5);
        }
    }
}
