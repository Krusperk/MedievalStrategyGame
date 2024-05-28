namespace MSGLogic
{
    public class Game
    {
        public List<Player> players;
        double attackDisadv = 0.8;

        public Game(params Player[] playersArray)
        {
            this.players = playersArray.ToList();
        }

        public static Game CreateGame()
        {
            return new Game(new Player(PlayerColor.Blue, "Alice"),
                            new Player(PlayerColor.Red, "Bob"));
        }

        /// <summary>
        /// Elemental step of game progression
        /// </summary>
        public void NextTurn()
        {
            players.ForEach(p => p.Produce());
        }

        /// <summary>
        /// Player attacking his opponent
        /// </summary>
        /// <param name="sender">Who initiated attack</param>
        /// <returns>Indicate if attack was successfull</returns>
        public bool Attack(string attacker)
        {
            var p1 = players.First();
            var p2 = players.Last();
            bool success;

            int attackerArmy = attacker == p1.name ? p1.armySize : p2.armySize;
            int defenderArmy = attacker == p1.name ? p2.armySize : p1.armySize;

            attackerArmy = (int)Math.Round(attackerArmy * attackDisadv);

            // Attacker won the gamme
            if (attackerArmy > defenderArmy)
            {
                attackerArmy -= defenderArmy;
                defenderArmy = 0;

                success = true;
            }
            // Attack failed
            else
            {
                defenderArmy -= attackerArmy;
                attackerArmy = 0;

                success = false;
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

            return success;
        }
    }
}
