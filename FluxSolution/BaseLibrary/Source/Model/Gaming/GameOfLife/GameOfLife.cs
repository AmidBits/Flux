using System;

namespace Flux.Model.Gaming.GameOfLife
{
  // Plays Conway's Game of Life on the console with a random initial state.
  public class Game
  {
    // The delay in milliseconds between board updates.
    private const int DELAY = 50;

    // The cell colors.
    private const ConsoleColor DEAD_COLOR = ConsoleColor.White;
    private const ConsoleColor LIVE_COLOR = ConsoleColor.Black;

    // The color of the cells that are off of the board.
    private const ConsoleColor EXTRA_COLOR = ConsoleColor.Gray;

    private const char EMPTY_BLOCK_CHAR = ' ';
    private const char FULL_BLOCK_CHAR = '\u2588';

    // Holds the current state of the board.
    private static bool[,] board;

    // The dimensions of the board in cells.
    private static int width = 32;
    private static int height = 32;

    // True if cell rules can loop around edges.
    private static bool loopEdges = true;

    public void Run()
    {
      // Use initializeRandomBoard for a larger, random board.
      InitializeRandomBoard();

      InitializeConsole();

      // Run the game until the Escape key is pressed.
      while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Escape)
      {
        Game.DrawBoard();
        Game.UpdateBoard();

        // Wait for a bit between updates.
        System.Threading.Thread.Sleep(DELAY);
      }
    }

    // Sets up the Console.
    private static void InitializeConsole()
    {
      Console.BackgroundColor = EXTRA_COLOR;
      Console.Clear();

      Console.CursorVisible = false;

      // Each cell is two characters wide.
      // Using an extra row on the bottom to prevent scrolling when drawing the board.
      int width = Math.Max(Game.width, 8) * 2 + 1;
      int height = Math.Max(Game.height, 8) + 1;
      System.Console.SetWindowSize(width, height);
      System.Console.SetBufferSize(width, height);

      Console.BackgroundColor = DEAD_COLOR;
      Console.ForegroundColor = LIVE_COLOR;
    }

    // Creates the initial board with a random state.
    private static void InitializeRandomBoard()
    {
      var random =new  Flux.Randomization.Xoshiro256SS();

      Game.board = new bool[Game.width, Game.height];
      for (var y = 0; y < Game.height; y++)
      {
        for (var x = 0; x < Game.width; x++)
        {
          // Equal probability of being true or false.
          Game.board[x, y] = random.Next(2) == 0;
        }
      }
    }

    // Creates a 3x3 board with a blinker.
    private static void InitializeDemoBoard()
    {
      Game.width = 3;
      Game.height = 3;

      Game.loopEdges = false;

      Game.board = new bool[3, 3];
      Game.board[1, 0] = true;
      Game.board[1, 1] = true;
      Game.board[1, 2] = true;
    }

    // Draws the board to the console.
    private static void DrawBoard()
    {
      var sb = new System.Text.StringBuilder();

      for (var y = 0; y < Game.height; y++)
      {
        for (var x = 0; x < Game.width; x++)
        {
          char c = Game.board[x, y] ? FULL_BLOCK_CHAR : EMPTY_BLOCK_CHAR;

          // Each cell is two characters wide.
          sb.Append(c);
          sb.Append(c);
        }

        sb.Append('\n');
      }

      // Write the string to the console.
      Console.SetCursorPosition(0, 0);
      Console.Write(sb.ToString());
    }

    // Moves the board to the next state based on Conway's rules.
    private static void UpdateBoard()
    {
      // A temp variable to hold the next state while it's being calculated.
      bool[,] newBoard = new bool[Game.width, Game.height];

      for (var y = 0; y < Game.height; y++)
      {
        for (var x = 0; x < Game.width; x++)
        {
          var n = CountLiveNeighbors(x, y);
          var c = Game.board[x, y];

          // A live cell dies unless it has exactly 2 or 3 live neighbors.
          // A dead cell remains dead unless it has exactly 3 live neighbors.
          newBoard[x, y] = c && (n == 2 || n == 3) || !c && n == 3;
        }
      }

      // Set the board to its new state.
      Game.board = newBoard;
    }

    // Returns the number of live neighbors around the cell at position (x,y).
    private static int CountLiveNeighbors(int x, int y)
    {
      // The number of live neighbors.
      int value = 0;

      // This nested loop enumerates the 9 cells in the specified cells neighborhood.
      for (var j = -1; j <= 1; j++)
      {
        // If loopEdges is set to false and y+j is off the board, continue.
        if (!Game.loopEdges && y + j < 0 || y + j >= Game.height)
        {
          continue;
        }

        // Loop around the edges if y+j is off the board.
        int k = (y + j + Game.height) % Game.height;

        for (var i = -1; i <= 1; i++)
        {
          // If loopEdges is set to false and x+i is off the board, continue.
          if (!Game.loopEdges && x + i < 0 || x + i >= Game.width)
          {
            continue;
          }

          // Loop around the edges if x+i is off the board.
          int h = (x + i + Game.width) % Game.width;

          // Count the neighbor cell at (h,k) if it is alive.
          value += Game.board[h, k] ? 1 : 0;
        }
      }

      // Subtract 1 if (x,y) is alive since we counted it as a neighbor.
      return value - (Game.board[x, y] ? 1 : 0);
    }
  }
}
