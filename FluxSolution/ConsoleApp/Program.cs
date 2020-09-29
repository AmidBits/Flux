using Flux;
using System;
using System.Data.Common;
using System.Linq;

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      var dataTable = new Flux.Resources.Scrape.ZipCodes().GetDataTable(Flux.Resources.Scrape.ZipCodes.LocalUri);

      foreach (var dataRow in dataTable.Select())
        System.Console.WriteLine(string.Join('|', dataRow.ItemArray));

      //var source = new System.Collections.Generic.List<(string, int)>
      //{
      //  ("Milk", 2),
      //  ("Cream", 6),
      //  ("Milk", 1),
      //  ("Chicken", 1),
      //  ("Chicken", 1),
      //  ("OJ", 3),
      //  ("OJ", 3),
      //  ("Cheese", 5),
      //};

      //System.Console.WriteLine(string.Join('|', source));
      //var sd = source.Histogram((e, i) => e.Item1, (e, i) => e.Item2);
      //System.Console.WriteLine(string.Join('|', sd.Select(kvp => $"{kvp}")));

      //var target = new System.Collections.Generic.List<(string, int)>
      //{
      //  ("Milk", 4),
      //  ("Cream", 4),
      //  ("Chicken", 4),
      //  ("OJ", 4),
      //  ("Cheese", 4),
      //};

      //var st = source.Inventory(target, vt => vt.Item1, vt => vt.Item2);

      //foreach (var kvp in st)
      //  System.Console.WriteLine($"{kvp}");

      //for (var i = 0; i < 10; i++)
      //  System.Console.WriteLine($"{i} = ({Flux.Bitwise.Log2(i)}) = {System.Math.Ceiling(System.Math.Log(i, 2))} = {System.Math.Pow(2, System.Math.Ceiling(System.Math.Log(i, 2)))}");

      //var rsA = new Flux.RunningStatistics();
      //rsA.Add(1, 3, 5, 7, 9);
      //var rsB = new Flux.RunningStatistics();
      //rsB.Add(2, 4, 6, 8);
      //var rsC = new Flux.RunningStatistics();
      //rsC.Add(0.85, 1.9, 2.8, 3.7, 4.6, 5.5, 6.4, 7.3, 8.2, 9.1, 10.15);
      //var rsT = rsA + rsB + rsC;


      //var bta = Flux.Collections.Immutable.AvlTree<char, string>.Empty;
      //bta = bta.Add('F', "10");
      //bta = bta.Add('B', "7");
      //bta = bta.Add('G', "14");
      //bta = bta.Add('A', "20");
      //bta = bta.Add('D', "1");
      //bta = bta.Add('D', "2");
      //bta = bta.Add('I', "5");
      //bta = bta.Add('C', "8");
      //bta = bta.Add('E', "31");
      //bta = bta.Add('H', "23");

      //System.Console.WriteLine(bta.IsBST(0, 'A', 'Z'));
      //System.Console.WriteLine(bta.IsBT(0));
      //System.Console.WriteLine(bta.Search(6));
      //System.Console.WriteLine(bta.Delete(7));
      //System.Console.WriteLine(bta.Delete(7));
      //      Flux.Text.
      ////http://www.gutenberg.org/ebooks/45849.txt.utf-8
      //var dt = new Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings().GetDataTable(Flux.Resources.ProjectGutenberg.TenThousandWonderfulThings.LocalUri);
      //var index = 0;
      //foreach (var b in dt.Rows.Cast<System.Data.DataRow>())
      //{
      //  System.Console.WriteLine();
      //  System.Console.WriteLine(++index);
      //  System.Console.WriteLine($"{string.Join('|', b.ItemArray)}");
      //  System.Console.ReadKey();
      //}

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
