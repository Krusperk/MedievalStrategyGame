using MSGLogic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Configuration;
using System;

namespace MSGViewWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush blueFill = Brushes.Blue;
        SolidColorBrush redFill = Brushes.Red;
        int rows;
        int columns;
        Game game;

        // Temporary
        Player p1;
        Player p2;

        public MainWindow()
        {
            InitializeComponent();
            ProcessConfig();
            game = Game.CreateGame(rows, columns);
            
            p1 = game.Players.First();
            p2 = game.Players.Last();

            updateTextBlocks();

            P1Attack.Content = p1.name;
            P2Attack.Content = p2.name;
        }

        private void ProcessConfig()
        {
            rows = int.Parse(ConfigurationManager.AppSettings["rows"]);
            columns = int.Parse(ConfigurationManager.AppSettings["columns"]);
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
