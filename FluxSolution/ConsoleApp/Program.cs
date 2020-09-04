using Flux;
using Flux.Model;
using Flux.Text;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
  class Program
  {
    static double[] felix = new double[] { 0.003027523, 0.002012256, -0.001369238, -0.001737660, -0.001647287,
        0.000275154, 0.002017238, 0.001372621, 0.000274148, -0.000913576, 0.001920263, 0.001186456, -0.000364631,
        0.000638337, 0.000182266, -0.001275626, -0.000821093, 0.001186998, -0.000455996, -0.000547445, -0.000182582,
        -0.000547845, 0.001279006, 0.000456204, 0.000000000, -0.001550388, 0.001552795, 0.000729594, -0.000455664,
        -0.002188184, 0.000639620, 0.000091316, 0.001552228, -0.001002826, 0.000182515, -0.000091241, -0.000821243,
        -0.002009132, 0.000000000, 0.000823572, 0.001920088, -0.001368863, 0.000000000, 0.002101800, 0.001094291,
        0.001639643, 0.002637323, 0.000000000, -0.000172336, -0.000462665, -0.000136141 };

    static double[] kahn1 = new double[] { 2, 2, 4, 4 };
    static double[] kahn2 = new double[] { 1, 1, 6, 4 };

    static double[] thoughtco = new double[] { 1, 2, 2, 3, 5, 7, 7, 7, 7, 9 };

    static double[] aad = new double[] { 2, 2, 3, 4, 14 };

    //public class Test
    //  : Flux.Collections.Generic.IQuadtree
    //{
    //  public Rectangle Bounds { get; set; }
    //}

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



      var a = new Flux.Numerics.BigFraction(5.ToBigInteger(), 6.ToBigInteger());
      a = Flux.Numerics.BigFraction.Negate(a);
      System.Console.WriteLine(a);
      var b = new Flux.Numerics.BigFraction(3.ToBigInteger(), 8.ToBigInteger());
      b = Flux.Numerics.BigFraction.Negate(b);
      System.Console.WriteLine(b);

      var gcd = Flux.Numerics.BigFraction.GreatestCommonDivisor(a, b);
      System.Console.WriteLine(gcd);
      var lcm = Flux.Numerics.BigFraction.LeastCommonMultiple(a, b);
      System.Console.WriteLine(lcm);

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
