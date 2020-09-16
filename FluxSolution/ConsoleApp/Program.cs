using Flux;
using Flux.Model;
using Flux.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {

      //var ttt = new Flux.Model.TicTacToe.Board();
      //System.Console.WriteLine(ttt.ToString());

      //for (var index = 0; index < 9; index++)
      //{
      //  {
      //    var myAllMoves = ttt.GetOptionsForPlayer2().ToList();
      //    if (myAllMoves.Count > 0)
      //      System.Console.WriteLine(string.Join(System.Environment.NewLine, myAllMoves));
      //    var myTopMoves = myAllMoves.Where(m => m.Score == myAllMoves.Max(m => m.Score)).ToList();
      //    if (myTopMoves.Count > 0)
      //    {
      //      var myTopMove = myTopMoves.RandomElement();
      //      if (myTopMove.Score == -10) break;
      //      System.Console.WriteLine($"Your top move: {myTopMove}");
      //    }

      //    System.Console.Write("Enter row: ");
      //    var rowChar = System.Console.ReadKey().KeyChar;
      //    System.Console.Write("\r\nEnter column: ");
      //    var columnChar = System.Console.ReadKey().KeyChar;

      //    if (rowChar == '\u001b' || columnChar == '\u001b')
      //    {
      //      ttt.Clear();
      //      System.Console.Clear();
      //      continue;
      //    }
      //    else System.Console.Clear();

      //    var row = int.Parse(rowChar.ToString());
      //    var column = int.Parse(columnChar.ToString());

      //    System.Console.WriteLine($"\r\nYour move: {row}, {column}");

      //    ttt[row, column] = Flux.Model.TicTacToe.State.Player2;

      //    System.Console.WriteLine(ttt.ToString());
      //  }

      //  {
      //    var allMoves = ttt.GetOptionsForPlayer1().ToList();

      //    if (allMoves.Count == 0) break;

      //    foreach (var m in allMoves)
      //      System.Console.WriteLine(m);

      //    var topMoves = allMoves.Where(m => m.Score == allMoves.Max(m => m.Score)).ToList();

      //    if (topMoves.Count == 0) break;

      //    var topMove = topMoves.RandomElement();
      //    if (topMove.Score == 10) break;

      //    if (!(topMove.Row == -1 || topMove.Column == -1))
      //      ttt[topMove.Row, topMove.Column] = Flux.Model.TicTacToe.State.Player1;//turn ? player : opponent;

      //    System.Console.WriteLine(ttt.ToString());
      //  }
      //}
      //if (move.Score == +10 || move.Score == -10) break;

      //var tmp = player;
      //player = opponent;
      //opponent = tmp;
      //turn = !turn;

      //foreach (var dr in Flux.Resources.Ucd.UnicodeData.GetDataReader(Flux.Resources.Ucd.UnicodeData.LocalUri))
      //  System.Console.WriteLine(string.Join(", ", dr.GetValues()));

      //var quad = new Flux.Collections.Generic.Quadtree<Test>(new Rectangle(0, 0, 600, 600));

      //quad.Insert(new Test() { Bounds = new Rectangle(450, 150, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(150, 350, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(400, 250, 10, 10) });
      //quad.Insert(new Test() { Bounds = new Rectangle(425, 125, 10, 10) });

      //var q = quad.Retrieve(new Rectangle(301,301, 298, 298));

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMean()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMedian()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => aad.AverageAbsoluteDeviationFromMode()));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => felix.Variance()));
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
