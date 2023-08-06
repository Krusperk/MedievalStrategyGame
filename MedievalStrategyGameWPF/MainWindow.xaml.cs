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
        int p1Army = 0;
        int p2Army = 0;
        int p1ArmyGrow = 1;
        int p2ArmyGrow = 1;
        SolidColorBrush blueFill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        SolidColorBrush redFill = new SolidColorBrush(Color.FromRgb(255, 0, 0));

        public MainWindow()
        {
            InitializeComponent();

            P1StartSquareCount.Text = p1Army.ToString();
            P1StartSquare.Fill = blueFill;

            P2StartSquareCount.Text = p2Army.ToString();
            P2StartSquare.Fill = redFill;
        }

        private void NextTurn_Click(object sender, RoutedEventArgs e)
        {
            // Production
            p1Army += p1ArmyGrow;
            p2Army += p2ArmyGrow;

            P1StartSquareCount.Text = p1Army.ToString();
            P2StartSquareCount.Text = p2Army.ToString();
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            var but = (Button)sender;
            string attacker = but.Name;
            string defender = attacker == "P1Attack" ? "P2Attack" : "P1Attack";
            int attackerArmy = attacker == "P1Attack" ? p1Army : p2Army;
            int defenderArmy = attacker == "P1Attack" ? p2Army : p1Army;

            attackerArmy = (int)Math.Round(attackerArmy * 0.8);

            // Attacker won the gamme
            if (attackerArmy > defenderArmy)
            {
                attackerArmy -= defenderArmy;
                defenderArmy = 0;

                if (attacker == "P1Attack")
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

            if (attacker == "P1Attack")
            {
                p1Army = attackerArmy;
                p2Army = defenderArmy;
            }
            else
            {
                p2Army = attackerArmy;
                p1Army = defenderArmy;
            }

            P1StartSquareCount.Text = p1Army.ToString();
            P2StartSquareCount.Text = p2Army.ToString();

            if (attackerArmy > defenderArmy)
                MessageBox.Show($"{attacker} won the gamme!");
            else
                MessageBox.Show($"{attacker}'s attack has failed!");
        }
    }
}
