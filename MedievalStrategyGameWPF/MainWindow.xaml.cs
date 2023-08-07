using MedievalStrategyGameWPF.Classes;
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

namespace MedievalStrategyGameWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double attackDisadv = 0.8;
        SolidColorBrush blueFill = Brushes.Blue;
        SolidColorBrush redFill = Brushes.Red;
        Player p1;
        Player p2;

        public MainWindow()
        {
            InitializeComponent();

            p1 = new Player(Brushes.Blue, "Alice");
            p2 = new Player(Brushes.Red, "Bob");

            updateTextBlocks();

            P1Attack.Content = p1.name;
            P2Attack.Content = p2.name;
        }

        private void NextTurn_Click(object sender, RoutedEventArgs e)
        {
            // Production
            p1.Produce();
            p2.Produce();

            updateTextBlocks();
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            var but = (Button)sender;
            string attacker = (string)but.Content;
            string defender = attacker == p1.name ? p2.name : p1.name;
            int attackerArmy = attacker == p1.name ? p1.armySize : p2.armySize;
            int defenderArmy = attacker == p1.name ? p2.armySize : p1.armySize;

            attackerArmy = (int)Math.Round(attackerArmy * attackDisadv);

            // Attacker won the gamme
            if (attackerArmy > defenderArmy)
            {
                attackerArmy -= defenderArmy;
                defenderArmy = 0;

                if (attacker == p1.name)
                    P2StartSquare.Fill = blueFill;
                else
                    P1StartSquare.Fill = redFill;
            }
            // Attack failed
            else 
            {
                defenderArmy -= attackerArmy;
                attackerArmy = 0;
            }

            if (attacker == p1.name)
            {
                p1.armySize = attackerArmy;
                p2.armySize = defenderArmy;
            }
            else
            {
                p2.armySize = attackerArmy;
                p1.armySize = defenderArmy;
            }

            updateTextBlocks();

            if (attackerArmy > defenderArmy)
                MessageBox.Show($"{attacker} won the gamme!");
            else
                MessageBox.Show($"{attacker}'s attack has failed!");
        }

        private void updateTextBlocks()
        {
            P1StartSquareCount.Text = p1.armySize.ToString();
            P2StartSquareCount.Text = p2.armySize.ToString();
        }
    }
}
