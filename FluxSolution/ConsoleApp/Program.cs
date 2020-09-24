using Flux;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {

      /*
      var ttt = new Flux.Model.TicTacToe.Board();

      for (var index = 0; index < 9; index++)
      {
        System.Console.Clear();

        {
          System.Console.WriteLine(ttt.ToString());

          var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
          if (allMoves1.Count > 0) System.Console.WriteLine("a1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
          var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
          if (allMoves2.Count > 0) System.Console.WriteLine("a2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

          var allMyMoves = allMoves1;

          var myTopMoves = allMyMoves.Where(m => m.Score == allMyMoves.Max(m => m.Score)).ToList();
          if (myTopMoves.Count > 0)
          {
            myTopMoves.RandomElement(out var myTopMove);
            if (myTopMove.Score == -10) break;
            System.Console.WriteLine($"Your top move: {myTopMove}"); 
          }

          System.Console.Write("Enter row: ");
          var rowChar = System.Console.ReadKey().KeyChar;
          System.Console.Write("\r\nEnter column: ");
          var columnChar = System.Console.ReadKey().KeyChar;

          if (rowChar == '\u001b' || columnChar == '\u001b')
          {
            ttt.Clear();
            System.Console.Clear();
            continue;
          }

          var row = int.Parse(rowChar.ToString());
          var column = int.Parse(columnChar.ToString());

          System.Console.WriteLine($"\r\nYour move: {row}, {column}");

          ttt[row, column] = Flux.Model.TicTacToe.State.Player2;
        }

        {
          var allMoves1 = ttt.GetOptionsForPlayer1().ToList();
          if (allMoves1.Count > 0) System.Console.WriteLine("b1" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves1));
          var allMoves2 = ttt.GetOptionsForPlayer2().ToList();
          if (allMoves2.Count > 0) System.Console.WriteLine("b2" + System.Environment.NewLine + string.Join(System.Environment.NewLine, allMoves2));

          var allMoves = allMoves2;

          if (allMoves.Count == 0) break;

          foreach (var m in allMoves)
            System.Console.WriteLine(m);

          var topMoves = allMoves.Where(m => m.Score == allMoves.Min(m => m.Score)).ToList();

          if (topMoves.Count == 0) break;

          topMoves.RandomElement(out var topMove);
          if (topMove.Score == 10) break;

          if (!(topMove.Row == -1 || topMove.Column == -1))
            ttt[topMove.Row, topMove.Column] = Flux.Model.TicTacToe.State.Player1;//turn ? player : opponent;

          System.Console.WriteLine(ttt.ToString());
        }
      }
      */
    }

    static void Main(string[] args)
    {
      System.Console.InputEncoding = System.Text.Encoding.UTF8;
      System.Console.OutputEncoding = System.Text.Encoding.UTF8;

      System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => TimedMain(args), 1));

      System.Console.WriteLine(System.Environment.NewLine + @"Press any key to exit...");
      System.Console.ReadKey();
    }
  }
}
