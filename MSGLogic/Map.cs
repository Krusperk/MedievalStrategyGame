namespace MSGLogic
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }

        public Map(int rows, int columns) 
        {
            Tiles = new Tile[rows, columns];
        }
    }
}
