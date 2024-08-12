namespace MSGLogic
{
    public class Player
    {
        public string name;
        public PlayerColor color;
        public int armySize;
        int armyGrow;

        public Player(PlayerColor color, string name)
        {
            this.name = name;
            this.color = color;
            armySize = 0;
            armyGrow = 1;
        }

        public void Produce() => armySize += armyGrow;
    }
}
