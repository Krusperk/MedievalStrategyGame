using MSGLogic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Configuration;
using System;
using System.Windows.Shapes;
using System.Xml.Schema;

namespace MSGViewWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game game;
        TileGUI[,] tiles;

        // Temporary
        Player p1;
        Player p2;

        public MainWindow()
        {
            InitializeComponent();

            game = Game.CreateGame();
            p1 = game.Players.First();
            p2 = game.Players.Last();

            InitializeMap();
            
            updateTextBlocks();

            P1Attack.Content = p1.name;
            P2Attack.Content = p2.name;
        }

        private void InitializeMap()
        {
            Map.Rows = game.Map.Tiles.GetLength(0);
            Map.Columns = game.Map.Tiles.GetLength(1);

            CreateMapTiles();
        }

        private void CreateMapTiles()
        {
            tiles = new TileGUI[Map.Rows, Map.Columns];
            for (int r = 0; r < Map.Rows; r++)
            {
                for (int c = 0; c < Map.Columns; c++)
                {
                    var player = (r, c) switch
                    {
                        _ when r == 0 && c == Map.Columns - 1 => p1,    // Top right corner spawn
                        _ when r == Map.Rows - 1 && c == 0 => p2,       // Bottom left corner spawn
                        _ => null                                       // Undiscovered
                    };
                    tiles[r, c] = new TileGUI(player, Map);
                }
            }
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
                SolidColorBrush color;
                if (attacker == p1.name)
                    color = Brushes.Blue;
                else
                    color = Brushes.Red;

                foreach (Grid grid in Map.Children)
                {
                    var rect = (Rectangle)grid.Children[0];
                    rect.Fill = color;
                }

                MessageBox.Show($"{attacker} won the gamme!");
            }
            else
                MessageBox.Show($"{attacker}'s attack has failed!");
        }

        private void updateTextBlocks()
        {
            foreach (TileGUI tile in tiles)
            {
                var color = tile.rectangle.Fill;

                tile.textBlock.Text = color == Brushes.Red
                                        ? p1.armySize.ToString()
                                    : color == Brushes.Blue
                                        ? p2.armySize.ToString()
                                        : "";
            }
        }
    }
}
