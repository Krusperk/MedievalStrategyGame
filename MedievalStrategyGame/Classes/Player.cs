using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MedievalStrategyGameWPF.Classes
{
    internal class Player
    {
        public string name;
        SolidColorBrush color;
        public int armySize;
        int armyGrow;

        public Player(SolidColorBrush color, string name)
        {
            this.name = name;
            this.color = color;
            armySize = 0;
            armyGrow = 1;
        }

        public void Produce() => armySize += armyGrow;
    }
}
