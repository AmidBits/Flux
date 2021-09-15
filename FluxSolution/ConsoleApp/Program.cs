using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

using Flux;

// C# Interactive commands:
// #r "System.Runtime"
// #r "System.Runtime.Numerics"
// #r "C:\Users\Rob\source\repos\Flux\FluxSolution\BaseLibrary\bin\Debug\net5.0\BaseLibrary.dll"

namespace ConsoleApp
{
  class Program
  {
    private static void TimedMain(string[] args)
    {
      int[,] board = {
        { 1, 2, 1 },
        { 2, 0, 0 },
        { 0, 0, 0 }
      };

      System.Console.WriteLine(board.ToConsoleBlock());

      var bpms = Flux.Model.TicTacToe2.Game.GetMovesForPlayer1(board);
      var bpmsmax = bpms.Values.Max();
      while (bpms.Any(kvp => kvp.Value < bpmsmax))
        bpms.Remove(bpms.First(kvp => kvp.Value < bpmsmax).Key);

      foreach (var move in bpms)
        System.Console.WriteLine(move);
      System.Console.WriteLine();

      var boms = Flux.Model.TicTacToe2.Game.GetMovesForPlayer2(board);
      while (boms.Any(kvp => !bpms.Keys.Contains(kvp.Key)))
        boms.Remove(boms.First(kvp => !bpms.Keys.Contains(kvp.Key)).Key);

      foreach (var move in boms)
        System.Console.WriteLine(move);
      System.Console.WriteLine();

      //if(boms.Any())

      var bm = bpms.OrderByDescending(pm => pm.Value).FirstOrValue(pm => boms.OrderBy(om => om.Value).Any(om => om.Key == pm.Key), bpms.OrderByDescending(pm => pm.Value).First());

      System.Console.WriteLine(bm);

      //      var scores = new int[] { 3, 5, 6, 9, 1, 2, 0, -1 };

      //      var mm = new Flux.Model.MinMax();

      //System.Console.WriteLine(      mm.Minimax(true, scores));

      //      var liss = new Flux.Metrical.LongestIncreasingSubsequence<int>();
      //      var seq = new int[] { 0, 8, 4, 12, 2, 10, 6, 14, 1, 9, 5, 13, 3, 11, 7, 15 };

      //      var glis = liss.GetLongestIncreasingSubsequence(seq);

      //var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      //var cv = new Flux.Model.GameOfLife.Console(game);
      //cv.Run(100);
      //return;

      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => RegularForLoop(10, 0.1), 1));
      //System.Console.WriteLine(Flux.Diagnostics.Performance.Measure(() => ParallelForLoop(10, 0.1), 1));
    }

    #region Serial vs. Parallel loops
    //static void RegularForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} started at {startDateTime}.");
    //  for (int i = 0; i < taskCount; i++)
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  }
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(RegularForLoop)} executed in {span.TotalSeconds} seconds.");
    //  //System.Console.WriteLine();
    //}
    //static void ParallelForLoop(int taskCount = 10, double taskLoad = 1)
    //{
    //  //var startDateTime = DateTime.Now;
    //  System.Threading.Tasks.Parallel.For(0, taskCount, i =>
    //  {
    //    ExpensiveTask(taskLoad);
    //    //var total = ExpensiveTask(taskLoad);
    //    //System.Console.WriteLine($"{nameof(ExpensiveTask)} {i} - {total}.");
    //  });
    //  //var endDateTime = DateTime.Now;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} ended at {endDateTime}.");
    //  //var span = endDateTime - startDateTime;
    //  //System.Console.WriteLine($"{nameof(ParallelForLoop)} executed in {span.TotalSeconds} seconds");
    //  //System.Console.WriteLine();
    //}
    //static long ExpensiveTask(double taskLoad = 1)
    //{
    //  var total = 0L;
    //  for (var i = 1; i < int.MaxValue * taskLoad; i++)
    //    total += i;
    //  return total;
    //}
    #endregion Serial vs. Parallel loops

    #region Main method
    static void Main(string[] args)
    {
      var originalOutputEncoding = System.Console.OutputEncoding;

      try
      {
        System.Console.OutputEncoding = new System.Text.UnicodeEncoding(!System.BitConverter.IsLittleEndian, false);
        System.Console.WriteLine("Output encoding set to UTF-16");
      }
      catch
      {
        System.Console.OutputEncoding = System.Text.UnicodeEncoding.UTF8;
        System.Console.WriteLine("Output encoding set to UTF-8");
      }

      System.Console.WriteLine($"The console encoding is {System.Console.OutputEncoding.EncodingName} (code page {System.Console.OutputEncoding.CodePage})");
      System.Console.WriteLine();

      System.Console.WriteLine(Flux.Services.Performance.Measure(() => TimedMain(args), 1));

      Console.OutputEncoding = originalOutputEncoding;

      System.Console.WriteLine($"{System.Environment.NewLine}Press any key to exit...");
      System.Console.ReadKey();
    }
    #endregion Main method
  }
}
