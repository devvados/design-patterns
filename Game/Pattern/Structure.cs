using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    abstract class Structure
    {
        public Race Race;
        public Territory Territory;
        public abstract string GetName();
    }

    abstract class ResourceFactory: Structure
    {

    }

    abstract class UnitFactory: Structure
    {
        public abstract Unit FactoryMethod();
    }

    class ArcherFactory : UnitFactory
    {
        public Builder b;
        public override string GetName() { return "Казарма Лучников"; }

        public override Unit FactoryMethod()
        {
            //Console.WriteLine(this.GetName() + " создает лучника");
            if (b != null)
            {
                b.Build();
                return new Archer() { Race = this.Race, Territory = this.Territory, Name = b.Result() + " лучник" };
            }
            else
                return new Archer() { Race = this.Race, Territory = this.Territory };
        }
    }

    class PikemanFactory : UnitFactory
    {
        public Builder b;
        public override string GetName() { return "Казарма Копейщиков"; }

        public override Unit FactoryMethod()
        {
            //Console.WriteLine(this.GetName() + " cоздает копейщика");
            if(b!=null)
            {
                b.Build();
                return new Pikeman() { Race = this.Race, Territory = this.Territory, Name = b.Result() + " копейщик" };
            }
            else
                return new Pikeman() { Race = this.Race, Territory = this.Territory }; 
        }
    }

    class SwordsmanFactory : UnitFactory
    {
        public Builder b;
        public override string GetName() { return "Казарма Мечников"; }

        public override Unit FactoryMethod()
        {
            //Console.WriteLine(this.GetName() + " создает мечника");
            if(b!=null)
            {
                b.Build();
                return new Swordsman() { Race = this.Race, Territory = this.Territory, Name = b.Result() + "мечник" };
            }
            return new Swordsman() { Race = this.Race, Territory = this.Territory };
        }
    }




}
