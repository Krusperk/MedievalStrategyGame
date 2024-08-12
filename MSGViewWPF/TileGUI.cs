using MSGLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MSGViewWPF
{
    internal class TileGUI
    {
        internal Grid grid;
        internal Rectangle rectangle;
        internal TextBlock textBlock;

        internal TileGUI(Player player, UniformGrid map)
        {
            grid = new Grid();
            rectangle = new Rectangle();
            textBlock = new TextBlock();

            grid.Children.Add(rectangle);
            grid.Children.Add(textBlock);

            textBlock.Text = "0";
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.Foreground = Brushes.White;
            textBlock.FontWeight = FontWeights.Bold;
            textBlock.FontSize = 20;

            rectangle.Fill = transformColor(player?.color);
            rectangle.Stroke = Brushes.Black;

            map.Children.Add(grid);
        }

        private static SolidColorBrush transformColor(PlayerColor? color)
        {
            return color switch
            {
                PlayerColor.Red => Brushes.Red,
                PlayerColor.Blue => Brushes.Blue,
                _ => Brushes.DarkGray
            };
        }
    }
}
