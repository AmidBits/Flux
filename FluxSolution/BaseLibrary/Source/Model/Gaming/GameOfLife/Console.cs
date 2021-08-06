namespace Flux.Model.Gaming.GameOfLife
{
  public class Console
  {
    private Game m_game;

    public Console(Game game)
      => m_game = game;

    public void Run()
    {
      Setup();

      while (!System.Console.KeyAvailable || System.Console.ReadKey(true).Key != System.ConsoleKey.Escape) // Run the game until the Escape key is pressed.
      {
        Draw();

        System.Threading.Thread.Sleep(250);

        m_game.Update();
      }
    }

    /// <summary>Setup the Console.</summary>
    private void Setup()
    {
      System.Console.CursorVisible = false;

      // Pad +1 to prevent scrolling when drawing the board.

      var width = 2 * System.Math.Max(m_game.GridSize.Width, 8) + 1; // Multiply by 2 for a symmetrical double-width for each cell.
      var height = System.Math.Max(m_game.GridSize.Height, 8) + 1;

      if (System.OperatingSystem.IsWindows())
      {
        System.Console.SetWindowSize(width, height);
        System.Console.SetBufferSize(width, height);
      }
    }

    /// <summary>Creates a 3x3 board with a blinker.</summary>
    //private void InitializeDemoBoard()
    //{
    //  m_size = new Geometry.Size2(3, 3);

    //  m_canLoopEdges = false;

    //  m_board = new CellState[m_size.Height, m_size.Width];
    //  m_board[1, 0] = CellState.Alive;
    //  m_board[1, 1] = CellState.Alive;
    //  m_board[1, 2] = CellState.Alive;
    //}

    // Draws the board to the console.
    private void Draw()
    {
      System.Console.SetCursorPosition(0, 0);

      System.Console.Write(m_game.ToConsoleBlock());
    }
  }
}
