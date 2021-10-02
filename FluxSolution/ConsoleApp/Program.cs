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
      var os1 = new Flux.DataStructures.OrderedSet<int>() { 1, 2, 3, 4, 5, 6 };

      System.Console.WriteLine(nameof(os1));
      foreach (var item in os1)
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      var os2 = new Flux.DataStructures.OrderedSet<int>() { 4, 5, 6, 7, 8, 9 };

      System.Console.WriteLine(nameof(os2));
      foreach (var item in os2)
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.SourceDifference));
      foreach (var item in Flux.SetOps.SourceDifference(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.SymmetricDifference));
      foreach (var item in Flux.SetOps.SymmetricDifference(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.TargetDifference));
      foreach (var item in Flux.SetOps.TargetDifference(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.Intersection));
      foreach (var item in Flux.SetOps.Intersection(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.Union));
      foreach (var item in Flux.SetOps.Union(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.UnionAll));
      foreach (var item in Flux.SetOps.UnionAll(os1, os2))
        System.Console.WriteLine(item);
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.SetEquals));
      System.Console.WriteLine(Flux.SetOps.SetEquals(os1, os2));
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.IsOverlapping));
      System.Console.WriteLine(Flux.SetOps.IsOverlapping(os1, os2));
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.IsSubsetOf));
      System.Console.WriteLine(Flux.SetOps.IsSubsetOf(os1, os2));
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.IsProperSubsetOf));
      System.Console.WriteLine(Flux.SetOps.IsProperSubsetOf(os1, os2));
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.IsSupersetOf));
      System.Console.WriteLine(Flux.SetOps.IsSupersetOf(os1, os2));
      System.Console.WriteLine();

      System.Console.WriteLine(nameof(Flux.SetOps.IsProperSupersetOf));
      System.Console.WriteLine(Flux.SetOps.IsProperSupersetOf(os1, os2));
      System.Console.WriteLine();

      //var am = new Flux.DataStructures.Graph.AdjacentMatrix<char, double>(i => i);

      //am.AddVertex('a');
      //am.AddVertex('b');
      //am.AddVertex('c');
      //am.AddVertex('d');
      //am.AddVertex('e');
      //am.AddVertex('f');

      //am.AddDirectedEdge('a', 'b', 1.5);
      //am.AddDirectedEdge('a', 'c', 2.5);
      //am.AddDirectedEdge('b', 'd', 1.75);
      //am.AddDirectedEdge('c', 'e', 1.25);
      //am.AddDirectedEdge('d', 'f', 1.75);
      //am.AddDirectedEdge('e', 'f', 2.25);

      //am.DijkstraShortestPath('a', 'f');

      var dal = new Flux.DataStructures.Graph.DigraphAdjacentList<int, char, double>();
      var daligraph = (Flux.DataStructures.Graph.IDigraph<int, char, double>)dal;

      dal.AddVertex(0, 'a');
      dal.AddVertex(1, 'b');
      dal.AddVertex(2, 'c');
      dal.AddVertex(3, 'd');
      dal.AddVertex(4, 'e');
      dal.AddVertex(5, 'f');
      dal.AddVertex(6, 'g');
      dal.AddVertex(7, 'h');
      dal.AddVertex(8, 'i');

      dal.AddEdge(0, 1, 4);
      dal.AddEdge(0, 7, 8);
      dal.AddEdge(1, 2, 8);
      dal.AddEdge(1, 7, 11);
      dal.AddEdge(2, 8, 2);
      dal.AddEdge(2, 5, 4);
      dal.AddEdge(2, 3, 7);
      dal.AddEdge(3, 5, 14);
      dal.AddEdge(3, 4, 9);
      //dal.AddEdge(4, 3, 9);
      //dal.AddEdge(4, 5, 10);
      dal.AddEdge(5, 3, 14);
      dal.AddEdge(5, 4, 10);
      dal.AddEdge(6, 8, 6);
      dal.AddEdge(6, 5, 2);
      dal.AddEdge(7, 1, 11);
      dal.AddEdge(7, 8, 7);
      dal.AddEdge(7, 6, 1);
      dal.AddEdge(8, 2, 2);
      dal.AddEdge(8, 6, 6);

      var lvertices = dal.GetVertices().ToList();
      var ledges = dal.GetEdges().ToList();

      var ldspt = daligraph.DijkstraShortestPathTree(0, i => i);
      //var lpmst = daligraph.PrimsMinimumSpanningTree(0, i => i);

      System.Console.WriteLine(dal.ToString());
      System.Console.WriteLine();

      var dam = new Flux.DataStructures.Graph.DigraphAdjacentMatrix<int, char, double>();
      var damigraph = (Flux.DataStructures.Graph.IDigraph<int, char, double>)dam;

      foreach (var vertex in lvertices)
        dam.AddVertex(vertex, dal.GetVertexValue(vertex));
      foreach (var edge in ledges)
        dam.AddEdge(edge.source, edge.target, edge.value);

      var mvertices = dam.GetVertices().ToList();
      var medges = dam.GetEdges().ToList();

      var edgeVertices = medges.SelectMany(e => new int[] { e.source, e.target }).Distinct().Count();

      var mverts = new System.Collections.Generic.HashSet<int>();

      int c = 0, e = 0;
      foreach (var edge in medges.RandomElements(1))
      {
        e++;
        if (!mverts.Contains(edge.source))
          mverts.Add(edge.source);
        if (!mverts.Contains(edge.target))
          mverts.Add(edge.target);
        c++;
        if (mverts.Count == mvertices.Count)
          break;
      }

      var mdspt = damigraph.DijkstraShortestPathTree(0, i => i);
      //var mpmst= damigraph.PrimsMinimumSpanningTree(0, i => i);

      System.Console.WriteLine(dam.ToString());
      System.Console.WriteLine();

      return;

      var s1 = "and";
      var s2 = @"Wishing to have an extensive list of Synonyms and Antonyms for another project, I settled on Fallows’ amazing publication. However, I could not find an OCR text which was clear of the many OCR-scan errors.I did my own OCR, which joined many of the line-end hyphenated words automatically, but which still resulted in a very large number of nearly identical(to other OCR scans) errors.This is probably not the fault of the OCR software, but rather of the quality of the 19th Century source type and ink, which tended to be far from crisp.The software was not entirely perfect though and it would often invert n and u, or render either of those letters as ii, and render i as l (lower case L). R and K were regularly mixed, so ROCK could be rendered as KOCR – if it managed to get the circular letters correct.Just one example of variation is to be found in the rendering of the coded abbreviation for SYNONYM, ‘SYN.’ It might be rendered with a period, comma, both or none; and some of these variations were recorded:  \ BTH \ 2vN. \ 8_vs. \ BYN, \ KYN. \ PYN. \ RYN. \ SIN, \ Srn. \ STIC. \ STK. \ STN. \ STN., \ STW. \ SVN_ \ SXN. \ SY.V. \ SY:. \ SYH. \ SYK. \ SYN, \ Syn, \ SYN. \ SYN. \ SYN._ \ SYS,. \ SYS. \ SYW. \ SYX\. and \ YX\. !I know! Grrr!";
      var s3 = @"bbaaccaadd";

      var bmh = new Flux.Text.BoothsAlgorithm<char>();

      var r = bmh.MinimalRotation(s3);

      //Flux.AmbOps.Amb.Example();
      //System.Console.WriteLine();

      //Flux.ConstraintPropagationSolver.SolveForKnownFacts();
      //System.Console.WriteLine();

      //return;

      //int[,] board = {
      //  { 1, 0, 2 },
      //  { 1, 0, 0 },
      //  { 0, 0, 2 }
      //};

      //int[,] board4 = {
      //  { 0, 2, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 },
      //  { 0, 0, 0, 0, 0 },
      //  { 1, 0, 0, 1, 2 },
      //  //{ 0, 0, 0, 0, 0 },
      //  //{ 0, 0, 0, 0 },
      //  //{ 0, 0, 0, 0, 0, 0 },
      //};

      //System.Console.WriteLine(board4.ToConsoleBlock());

      //System.Console.WriteLine(Flux.Model.TicTacToe2.Game.GetCounts(board4, out var playerUp).ToConsoleString());
      //System.Console.WriteLine(playerUp);
      //System.Console.WriteLine();

      //var moves = Flux.Model.TicTacToe2.Game.GetMoves4(board4, out var maxMove, out var minMove);

      //System.Console.WriteLine($"Best for max: {maxMove} and best for min: {minMove}");
      //System.Console.WriteLine();

      //foreach (var move in moves)
      //  System.Console.WriteLine(move);
      //System.Console.WriteLine();

      ////      var scores = new int[] { 3, 5, 6, 9, 1, 2, 0, -1 };

      ////      var mm = new Flux.Model.MinMax();

      ////System.Console.WriteLine(      mm.Minimax(true, scores));

      var game = new Flux.Model.GameOfLife.Game(32, 32, true, 0.5);
      var cv = new Flux.Model.GameOfLife.Console(game);
      cv.Run(200);
      return;

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
