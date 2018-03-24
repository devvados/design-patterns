using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class Unit
    {
        private Race race;
        private Territory territory;
        private string name;

        public string Name { get => name; set => name = value; }
        internal Territory Territory { get => territory; set => territory = value; }
        internal Race Race { get => race; set => race = value; }
    }

    class Archer : Unit
    {
        public Archer() { Name = "Лучник"; } 
    }
    class Pikeman : Unit
    {
        public Pikeman() { Name = "Копейщик"; }
    }
    class Swordsman : Unit
    {
        public Swordsman() { Name = "Мечник"; }
    }


}
