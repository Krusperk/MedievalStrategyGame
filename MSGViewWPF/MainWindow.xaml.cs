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
            
            game = Game.CreateGame();
            p1 = game.Players.First();
            p2 = game.Players.Last();

            updateTextBlocks();

            P1Attack.Content = p1.name;
            P2Attack.Content = p2.name;
        }

        private void InitializeMap()
        {
            Map.Rows = int.Parse(GetValueFromConfig("Rows"));
            Map.Columns = int.Parse(GetValueFromConfig("Columns"));

            CreateMapTiles();
        }

        //tileSize * r, tileSize * c, tileSize, tileSize
        private void CreateMapTiles()
        {
            var tileSize = int.Parse(GetValueFromConfig("TileSize"));
            for (int r = 0; r < Map.Rows; r++)
            {
                for (int c = 0; c < Map.Columns; c++)
                {
                    var grid = new Grid();
                    var rectangle = new Rectangle();
                    var textBlock = new TextBlock();
                    grid.Children.Add(rectangle);
                    grid.Children.Add(textBlock);

                    textBlock.Text = "0";
                    textBlock.VerticalAlignment = VerticalAlignment.Center;
                    textBlock.HorizontalAlignment= HorizontalAlignment.Center;
                    textBlock.Foreground = Brushes.White;
                    textBlock.FontWeight = FontWeights.Bold;
                    textBlock.FontSize = 20;

                    rectangle.Fill = (r, c) switch
                    {
                        _ when r == 0 && c == Map.Columns - 1 => Brushes.Red,   // Top right corner spawn
                        _ when r == Map.Rows - 1 && c == 0 => Brushes.Blue,     // Bottom left corner spawn
                        _ => rectangle.Fill = Brushes.DarkGray                  // Undiscovered
                    };

                    rectangle.Stroke = Brushes.Black;

                    Map.Children.Add(grid);
                }
            }
        }

        private string GetValueFromConfig(string key) => ConfigurationManager.AppSettings[key];

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
                    color = blueFill;
                else
                    color = redFill;

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
            foreach (Grid tile in Map.Children)
            {
                var color = ((Rectangle)tile.Children[0]).Fill;

                if (color == Brushes.Red)
                    ((TextBlock)tile.Children[1]).Text = p1.armySize.ToString();
                else if (color == Brushes.Blue)
                    ((TextBlock)tile.Children[1]).Text = p2.armySize.ToString();
            }
        }
    }
}
