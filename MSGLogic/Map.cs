namespace MSGLogic
{
    public class Map
    {
        public Tile[,] Tiles { get; set; }

        public Map(int rows, int columns, List<Player> players) 
        {
            Tiles = new Tile[rows, columns];

            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                {
                    Player player = (r, c) switch
                    {
                        _ when r == 0 && c == columns - 1 => players.First(),
                        _ when r == rows - 1 && c == 0 => players.Last(),
                        _ => null
                    };

                    Tiles[r, c] = new Tile(player);
                }
            }
        }
    }
}
