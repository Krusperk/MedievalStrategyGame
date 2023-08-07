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
        SolidColorBrush blueFill = Brushes.Blue;
        SolidColorBrush redFill = Brushes.Red;
        Game game;

        // Temporary
        Player p1;
        Player p2;

        public MainWindow()
        {
            InitializeComponent();

            game = new Game(new Player(Brushes.Blue, "Alice"),
                            new Player(Brushes.Red, "Bob"));
            p1 = game.players.First();
            p2 = game.players.Last();

            updateTextBlocks();

            P1Attack.Content = p1.name;
            P2Attack.Content = p2.name;
        }

        private void NextTurn_Click(object sender, RoutedEventArgs e)
        {
            game.NextTurn();

            updateTextBlocks();
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {
            var but = (Button)sender;
            string attacker = (string)but.Content;

            var successAttackP = game.Attack(attacker);

            updateTextBlocks();

            if (successAttackP)
            {
                if (attacker == p1.name)
                    P2StartSquare.Fill = blueFill;
                else
                    P1StartSquare.Fill = redFill;

                MessageBox.Show($"{attacker} won the gamme!");
            }
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
