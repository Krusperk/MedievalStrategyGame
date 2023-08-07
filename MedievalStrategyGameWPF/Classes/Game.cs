using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedievalStrategyGameWPF.Classes
{
    internal class Game
    {
        List<Player> players;

        public Game()
        {
            players= new List<Player>();
        }

        /// <summary>
        /// Elemental step of game progression
        /// </summary>
        public void NextTurn()
        {

        }

        /// <summary>
        /// Player attacking his opponent
        /// </summary>
        /// <param name="sender">Who initiated attack</param>
        /// <returns>Indicate if attack was successfull</returns>
        public bool Attack(object sender)
        {


            throw new NotImplementedException();
        }
    }
}
