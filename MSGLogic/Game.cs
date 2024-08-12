using System.Configuration;

namespace MSGLogic
{
    public class Game
    {
        public List<Player> Players { get; set; }
        public Map Map { get; set; }
        double attackDisadv = 0.8;

        public Game(Map map, List<Player> players)
        {
            this.Players = players;
            this.Map = map;
        }

        public static Game CreateGame()
        {
            int rows = int.Parse(GetValueFromConfig("Rows"));
            int columns = int.Parse(GetValueFromConfig("Columns"));

            var players = new List<Player>
            {
                new Player(PlayerColor.Blue, "Alice"),
                new Player(PlayerColor.Red, "Bob")
            };

            return new Game(new Map(rows, columns, players),
                            players);
        }

        private static string GetValueFromConfig(string key) => ConfigurationManager.AppSettings[key];

        /// <summary>
        /// Elemental step of game progression
        /// </summary>
        public void NextTurn()
        {
            Players.ForEach(p => p.Produce());
        }

        /// <summary>
        /// Player attacking his opponent
        /// </summary>
        /// <param name="sender">Who initiated attack</param>
        /// <returns>Indicate if attack was successfull</returns>
        public bool Attack(string attacker)
        {
            var p1 = Players.First();
            var p2 = Players.Last();
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
