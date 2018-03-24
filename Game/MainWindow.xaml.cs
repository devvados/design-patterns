using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<List<int>> a = new List<List<int>>() { new List<int>() { 1, 2, 3 }, new List<int>() { 1, 3, 2 }, new List<int>() { 1, 4, 3 } };
            World aaaa = World.GetWorld();
            //Console.ReadKey();

            var races = new List<RaceInfo>(World.racesInfo);

            for(var i = 0; i < World.Races.Count; i++)
            {
                var rd = new RowDefinition
                {
                    Height = new GridLength(120)
                };
                GridRaces.RowDefinitions.Add(rd);


                var dp = new DockPanel
                {
                    Tag = i
                };
                for (var j = 0; j < races.Count; j++)
                {
                    string raceName = races[j].LRaceName.Content.ToString();
                    int raceNum = Convert.ToInt32(raceName.Replace("Раса ", ""));
                    if (raceNum == (int)dp.Tag)
                        dp.Children.Add(races[j]);
                }
                Grid.SetRow(dp, i);
                GridRaces.Children.Add(dp);
            }
        }
    }
}
