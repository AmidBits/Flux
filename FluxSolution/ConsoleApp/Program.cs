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
      var m1 = new int[,] { 
        { 1, 2, 3, 4 }, 
        { 5, 6, 7, 8 },
        { 9, 10, 11, 12 },
        { 13, 14, 15, 16 }
      };
      System.Console.WriteLine(m1.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      var m2 = m1.Insert(1, 1, 2, -9);
      System.Console.WriteLine(m2.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      m2.Fill(1, 1, 2, 2, -1, -2, -3);
      System.Console.WriteLine(m2.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      var m3 = m2.Flip(0);
      System.Console.WriteLine(m3.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      var m4 = m3.Remove(1, 1, 2);
      System.Console.WriteLine(m4.ToConsoleBlock(uniformWidth: true));
      System.Console.WriteLine();
      //var m3 = m1.RotateClockwise().Duplicate(4, 4, 3, 3, 0, 0, 1, 1);
      //System.Console.WriteLine(m3.ToConsoleBlock());
      //System.Console.WriteLine();
      //var m4 = m1.RotateClockwise().ToNewArray(0,0,3,3,1,1,0,0);
      //System.Console.WriteLine(m4.ToConsoleBlock());
      //System.Console.WriteLine();

      return;

      var gam = new Flux.DataStructures.Graphs.AdjacencyMatrix();

      gam.AddVertex(4);

      // 6, 8
      gam.AddEdge(0, 1, (3, 1));
      gam.AddEdge(0, 2, (1, 0));
      gam.AddEdge(0, 4, (3, 2));
      gam.AddEdge(1, 2, (2, 0));
      gam.AddEdge(1, 3, (0, 3));
      gam.AddEdge(2, 3, (1, 0));
      gam.AddEdge(2, 4, (6, 0));
      gam.AddEdge(3, 4, (2, 1));

      // 10, 1
      //gam.AddEdge(0, 1, (3, 1));
      //gam.AddEdge(0, 2, (4, 0));
      //gam.AddEdge(0, 3, (5, 0));
      //gam.AddEdge(1, 2, (2, 0));
      //gam.AddEdge(2, 3, (4, 0));
      //gam.AddEdge(2, 4, (1, 0));
      //gam.AddEdge(3, 4, (10, 0));

      System.Console.WriteLine(gam.ToConsoleString());

      var mcmf = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o is null ? 0 : ((System.ValueTuple<int, int>)o).Item1, o => o is null ? 0 : ((System.ValueTuple<int, int>)o).Item2);
      System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmf}");
      System.Console.WriteLine();

      var cap101 = new double[,] {
        { 0, 3, 4, 5, 0 },
        { 0, 0, 2, 0, 0 },
        { 0, 0, 0, 4, 1 },
        { 0, 0, 0, 0, 10 },
        { 0, 0, 0, 0, 0 }
      };
      var cost101 = new double[,] {
        { 0, 1, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0 }
      };



      return;

      var am = new Flux.DataStructures.Graphs.AdjacencyMatrix();

      am.AddVertex(0, 'a');
      am.AddVertex(1, 'b');
      am.AddVertex(2, 'c');
      am.AddVertex(3, 'd');
      am.AddVertex(4, 'e');
      am.AddVertex(5, 'f');
      am.AddVertex(6, 'g');
      am.AddVertex(7, 'h');
      am.AddVertex(8, 'i');

      am.AddEdge(0, 1, 4);
      am.AddEdge(1, 0, 4);

      am.AddEdge(0, 7, 8);

      am.AddEdge(1, 2, 8);

      am.AddEdge(2, 1, 8);
      am.AddEdge(2, 3, 7);
      am.AddEdge(2, 5, 4);
      am.AddEdge(2, 8, 2);

      am.AddEdge(3, 2, 7);
      am.AddEdge(3, 5, 14);
      am.AddEdge(3, 3, 13);
      am.AddEdge(3, 4, 9);

      am.AddEdge(4, 3, 9);
      am.AddEdge(4, 5, 10);

      am.AddEdge(5, 2, 4);
      am.AddEdge(5, 3, 14);
      am.AddEdge(5, 4, 10);
      am.AddEdge(5, 6, 2);

      am.AddEdge(6, 5, 2);
      am.AddEdge(6, 7, 1);
      am.AddEdge(6, 8, 6);

      am.AddEdge(7, 0, 8);
      am.AddEdge(7, 1, 11);
      am.AddEdge(7, 6, 1);
      am.AddEdge(7, 8, 7);

      am.AddEdge(8, 2, 2);
      am.AddEdge(8, 6, 6);
      am.AddEdge(8, 7, 7);

      System.Console.WriteLine(am.ToConsoleString());

      System.Console.WriteLine(@"Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      System.Console.WriteLine($"{string.Join(System.Environment.NewLine, am.GetDijkstraShortestPathTree(0, o => System.Convert.ToDouble(o)))}");

      return;

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

      var g = new Flux.DataStructures.Graphs.AdjacencyMatrix<int, char, double>();

      g.AddVertex(0, 'a');
      g.AddVertex(1, 'b');
      g.AddVertex(2, 'c');
      g.AddVertex(3, 'd');
      g.AddVertex(4, 'e');
      g.AddVertex(5, 'f');
      g.AddVertex(6, 'g');
      g.AddVertex(7, 'h');
      g.AddVertex(8, 'i');

      g.AddSimpleDirectedEdge(0, 1, 4);
      g.AddSimpleDirectedEdge(1, 0, 4);

      g.AddSimpleDirectedEdge(0, 7, 8);

      g.AddSimpleDirectedEdge(1, 2, 8);

      g.AddSimpleDirectedEdge(2, 1, 8);
      g.AddSimpleDirectedEdge(2, 3, 7);
      g.AddSimpleDirectedEdge(2, 5, 4);
      g.AddSimpleDirectedEdge(2, 8, 2);

      g.AddSimpleDirectedEdge(3, 2, 7);
      g.AddSimpleDirectedEdge(3, 5, 14);
      g.AddSimpleDirectedEdge(3, 3, 13);
      g.AddSimpleDirectedEdge(3, 4, 9);

      g.AddSimpleDirectedEdge(4, 3, 9);
      g.AddSimpleDirectedEdge(4, 5, 10);

      g.AddSimpleDirectedEdge(5, 2, 4);
      g.AddSimpleDirectedEdge(5, 3, 14);
      g.AddSimpleDirectedEdge(5, 4, 10);
      g.AddSimpleDirectedEdge(5, 6, 2);

      g.AddSimpleDirectedEdge(6, 5, 2);
      g.AddSimpleDirectedEdge(6, 7, 1);
      g.AddSimpleDirectedEdge(6, 8, 6);

      g.AddSimpleDirectedEdge(7, 0, 8);
      g.AddSimpleDirectedEdge(7, 1, 11);
      g.AddSimpleDirectedEdge(7, 6, 1);
      g.AddSimpleDirectedEdge(7, 8, 7);

      g.AddSimpleDirectedEdge(8, 2, 2);
      g.AddSimpleDirectedEdge(8, 6, 6);
      g.AddSimpleDirectedEdge(8, 7, 7);

      var vertices = g.GetVertices().ToList();
      var edges = g.GetDirectedEdges().ToList();

      var dspt = g.GetDijkstraShortestPathTree(0, i => i);
      //var lpmst = daligraph.PrimsMinimumSpanningTree(0, i => i);

      var verticesWithDegrees = g.GetVerticesWithDegree();
      var verticesWithValues = g.GetVerticesWithValue();

      System.Console.WriteLine(@"Graph Vertices (key, value, degree):");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, vertices.Select((e, i) => $"{(i + 1):D2} = {e}")));
      System.Console.WriteLine(@"Graph Vertices (key, degree):");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, verticesWithDegrees.Select((e, i) => $"{(i + 1):D2} = {e}")));
      System.Console.WriteLine(@"Graph Vertices (key, value):");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, verticesWithValues.Select((e, i) => $"{(i + 1):D2} = {e}")));
      System.Console.WriteLine(@"Graph Edges (source-key, target-key, value):");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, edges.Select((e, i) => $"{(i + 1):D2} = {e}")));
      //System.Console.WriteLine(@"Graph Edges Exploded (source-key, target-key, value):");
      //System.Console.WriteLine(string.Join(System.Environment.NewLine, edgesExploded.Select((e, i) => $"{(i + 1):D2} = {e}")));
      System.Console.WriteLine(@"Dijkstra's Shortest Path Tree (SPT) (destination-key, distance):");
      System.Console.WriteLine(string.Join(System.Environment.NewLine, dspt.Select((e, i) => $"{(i + 1):D2} = {e}")));
      System.Console.WriteLine();

      System.Console.WriteLine(g.ToConsoleString());
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
