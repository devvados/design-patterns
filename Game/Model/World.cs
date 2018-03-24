using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class World
    {
        public static List<RaceInfo> racesInfo = new List<RaceInfo>();
        static World W = null;
        public static List<Race> Races = new List<Race>();
        public List<Territory> Territories = new List<Territory>();

        public static World GetWorld()
        {
            W = new World(3);

            return W;
        }


        public World(int NumRaces)
        {
            int TerNum = 0;
            for (int i = 0; i < NumRaces; i++)
            {
                RaceInfo control = new RaceInfo();
                Races.Add(new Race("Раса " + i.ToString()));
                control.LRaceName.Content = Races.Last().Name.ToString();

                for (int j = 0; j < 10; j++)
                {
                    Races[i].Territories.Add(new Territory("Территория " + TerNum.ToString()) { Race = Races[i] });
                    control.CBTerritories.Items.Add(Races[i].Territories.Last().Name);
                    control.LFactories.Content = Races[i].Territories[j].Structures.Last().GetName();
                    //Console.WriteLine(Races[i].Name + "\t" + Races[i].Territories[j].Name + "\t" + Races[i].Territories[j].Structures.Last().GetName());             
                    TerNum++;
                }
                (Races[i].Territories[2].Structures[0] as UnitFactory).FactoryMethod();
                //Console.WriteLine("\n" + new string('-', 100) + "\n");
                racesInfo.Add(control);
            }
            
            this.MoveTerritoryToRace(Races[1], Races[0].Territories[2]);

            Console.WriteLine("\n" + new string('-', 100) + "\n");

            ArcherFactory taf = new ArcherFactory()
            {
                b = new HeavyBuilder()
            };
            Console.WriteLine(taf.FactoryMethod().Name);
        }

        public void MoveTerritoryToRace(Race To, Territory Terr)
        {
            Console.WriteLine(Terr.Name +" переходит к "+ To.Name);
            Terr.Race = To;
        }       
    }
}
