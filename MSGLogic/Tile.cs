namespace MSGLogic
{
    public class Tile
    {
        public Player Owner { get; set; }

        public Tile(Player owner)
        {
            Owner = owner;
        }
    }
}
