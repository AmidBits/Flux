using System;
using System.Collections.Generic;
using System.Linq;
using Flux;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataStructures
{
  [TestClass]
  public partial class Graphs
  {
    [TestMethod]
    public void AdjacencyMatrix1()
    {
      var gam = new Flux.DataStructure.Graph.AdjacencyMatrix<int, (int capacity, int cost)>();

      gam.AddVertex(0);
      gam.AddVertex(1);
      gam.AddVertex(2);
      gam.AddVertex(3);
      gam.AddVertex(4);

      // BellmanFord: 6, 8
      gam.AddEdge(0, 1, (3, 1));
      gam.AddEdge(0, 2, (1, 0));
      gam.AddEdge(0, 4, (3, 2));
      gam.AddEdge(1, 2, (2, 0));
      gam.AddEdge(1, 3, (0, 3));
      gam.AddEdge(2, 3, (1, 0));
      gam.AddEdge(2, 4, (6, 0));
      gam.AddEdge(3, 4, (2, 1));

      System.Console.WriteLine(gam.ToConsoleString());

      var mcmfExpected = (6, 8);
      var mcmfActual = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o.capacity, o => o.cost);
      System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmfActual}");
      System.Console.WriteLine();
      Assert.AreEqual(mcmfExpected, mcmfActual);

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (2, 1), (3, 2), (1, 3), (4, 3) };
      var sptActual = gam.GetDijkstraShortestPathTree(0, o => o.capacity).ToList();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination = distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination} = {distance}");
      System.Console.WriteLine(System.Environment.NewLine);
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }

    [TestMethod]
    public void AdjacencyMatrix2()
    {
      var gam = new Flux.DataStructure.Graph.AdjacencyMatrix<int, (int capacity, int cost)>();

      gam.AddVertex(0);
      gam.AddVertex(1);
      gam.AddVertex(2);
      gam.AddVertex(3);
      gam.AddVertex(4);

      // BellmanFord: 10, 1
      gam.AddEdge(0, 1, (3, 1));
      gam.AddEdge(0, 2, (4, 0));
      gam.AddEdge(0, 3, (5, 0));
      gam.AddEdge(1, 2, (2, 0));
      gam.AddEdge(2, 3, (4, 0));
      gam.AddEdge(2, 4, (1, 0));
      gam.AddEdge(3, 4, (10, 0));

      System.Console.WriteLine(gam.ToConsoleString());

      var mcmfExpected = (10, 1);
      var mcmfActual = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o.capacity, o => o.cost);
      System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmfActual}");
      System.Console.WriteLine();
      Assert.AreEqual(mcmfExpected, mcmfActual);

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (1, 3), (2, 4), (3, 5), (4, 5) };
      var sptActual = gam.GetDijkstraShortestPathTree(0, o => o.capacity).ToList();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination = distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination} = {distance}");
      System.Console.WriteLine(System.Environment.NewLine);
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }

    [TestMethod]
    public void AdjacencyMatrix3()
    {
      var gam = new Flux.DataStructure.Graph.AdjacencyMatrix<char, int>();

      gam.AddVertex(0, 'a');
      gam.AddVertex(1, 'b');
      gam.AddVertex(2, 'c');
      gam.AddVertex(3, 'd');
      gam.AddVertex(4, 'e');
      gam.AddVertex(5, 'f');
      gam.AddVertex(6, 'g');
      gam.AddVertex(7, 'h');
      gam.AddVertex(8, 'i');

      gam.AddEdge(0, 1, 4);
      gam.AddEdge(1, 0, 4);

      gam.AddEdge(0, 7, 8);

      gam.AddEdge(1, 2, 8);

      gam.AddEdge(2, 1, 8);
      gam.AddEdge(2, 3, 7);
      gam.AddEdge(2, 5, 4);
      gam.AddEdge(2, 8, 2);

      gam.AddEdge(3, 2, 7);
      gam.AddEdge(3, 5, 14);
      gam.AddEdge(3, 3, 13);
      gam.AddEdge(3, 4, 9);

      gam.AddEdge(4, 3, 9);
      gam.AddEdge(4, 5, 10);

      gam.AddEdge(5, 2, 4);
      gam.AddEdge(5, 3, 14);
      gam.AddEdge(5, 4, 10);
      gam.AddEdge(5, 6, 2);

      gam.AddEdge(6, 5, 2);
      gam.AddEdge(6, 7, 1);
      gam.AddEdge(6, 8, 6);

      gam.AddEdge(7, 0, 8);
      gam.AddEdge(7, 1, 11);
      gam.AddEdge(7, 6, 1);
      gam.AddEdge(7, 8, 7);

      gam.AddEdge(8, 2, 2);
      gam.AddEdge(8, 6, 6);
      gam.AddEdge(8, 7, 7);

      System.Console.WriteLine(gam.ToConsoleString());

      var mcmfExpected = (12, 396);
      var mcmfActual = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o, o => o);
      System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmfActual}");
      System.Console.WriteLine();
      Assert.AreEqual(mcmfExpected, mcmfActual);

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (1, 4), (7, 8), (6, 9), (5, 11), (2, 12), (8, 14), (3, 19), (4, 21) };
      var sptActual = gam.GetDijkstraShortestPathTree(0, o => (int)o).ToList();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination}={distance}");
      System.Console.WriteLine();
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }

    [TestMethod]
    public void AdjacencyList1()
    {
      var gal = new Flux.DataStructure.Graph.AdjacencyList<int, (int unused, int distance)>();

      gal.AddVertex(0);
      gal.AddVertex(1);
      gal.AddVertex(2);
      gal.AddVertex(3);
      gal.AddVertex(4);
      gal.AddVertex(5);
      gal.AddVertex(6);
      gal.AddVertex(7);
      gal.AddVertex(8);

      // 6, 8
      gal.AddEdge(0, 1, (3, 1));
      gal.AddEdge(0, 2, (1, 0));
      gal.AddEdge(0, 4, (3, 2));
      gal.AddEdge(1, 2, (2, 0));
      gal.AddEdge(1, 3, (0, 3));
      gal.AddEdge(2, 3, (1, 0));
      gal.AddEdge(2, 4, (6, 0));
      gal.AddEdge(3, 4, (2, 1));

      System.Console.WriteLine(gal.ToConsoleString());

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (2, 0), (3, 0), (4, 0), (1, 1) };
      var sptActual = gal.GetDijkstraShortestPathTree(0, o => o.distance).ToList();
      System.Console.WriteLine();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination}={distance}");
      System.Console.WriteLine();
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }

    [TestMethod]
    public void AdjacencyList2()
    {
      var gal = new Flux.DataStructure.Graph.AdjacencyList<int, (int unused, int distance)>();

      gal.AddVertex(0);
      gal.AddVertex(1);
      gal.AddVertex(2);
      gal.AddVertex(3);
      gal.AddVertex(4);
      gal.AddVertex(5);
      gal.AddVertex(6);
      gal.AddVertex(7);
      gal.AddVertex(8);

      // 10, 1
      gal.AddEdge(0, 1, (3, 1));
      gal.AddEdge(0, 2, (4, 0));
      gal.AddEdge(0, 3, (5, 0));
      gal.AddEdge(1, 2, (2, 0));
      gal.AddEdge(2, 3, (4, 0));
      gal.AddEdge(2, 4, (1, 0));
      gal.AddEdge(3, 4, (10, 0));

      System.Console.WriteLine(gal.ToConsoleString());

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (2, 0), (3, 0), (4, 0), (1, 1) };
      var sptActual = gal.GetDijkstraShortestPathTree(0, o => o.distance).ToList();
      System.Console.WriteLine();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination}={distance}");
      System.Console.WriteLine();
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }

    [TestMethod]
    public void AdjacencyList3()
    {
      var gal = new Flux.DataStructure.Graph.AdjacencyList<int, (int unused, int distance)>();

      gal.AddVertex(0);
      gal.AddVertex(1);
      gal.AddVertex(2);
      gal.AddVertex(3);
      gal.AddVertex(4);
      gal.AddVertex(5);
      gal.AddVertex(6);
      gal.AddVertex(7);
      gal.AddVertex(8);

      gal.AddEdge(0, 1, (0, 4));
      gal.AddEdge(0, 7, (0, 8));
      gal.AddEdge(1, 0, (0, 4));
      gal.AddEdge(1, 2, (0, 8));
      gal.AddEdge(1, 7, (0, 11));
      gal.AddEdge(2, 1, (0, 8));
      gal.AddEdge(2, 3, (0, 7));
      gal.AddEdge(2, 5, (0, 4));
      gal.AddEdge(2, 8, (0, 2));
      gal.AddEdge(3, 2, (0, 7));
      gal.AddEdge(3, 4, (0, 9));
      gal.AddEdge(3, 5, (0, 14));
      gal.AddEdge(4, 3, (0, 9));
      gal.AddEdge(4, 5, (0, 10));
      gal.AddEdge(5, 2, (0, 4));
      gal.AddEdge(5, 3, (0, 14));
      gal.AddEdge(5, 4, (0, 10));
      gal.AddEdge(5, 6, (0, 2));
      gal.AddEdge(6, 5, (0, 2));
      gal.AddEdge(6, 7, (0, 1));
      gal.AddEdge(6, 8, (0, 6));
      gal.AddEdge(7, 0, (0, 8));
      gal.AddEdge(7, 1, (0, 11));
      gal.AddEdge(7, 6, (0, 1));
      gal.AddEdge(7, 8, (0, 7));
      gal.AddEdge(8, 2, (0, 2));
      gal.AddEdge(8, 6, (0, 6));
      gal.AddEdge(8, 7, (0, 7));

      System.Console.WriteLine(gal.ToConsoleString());

      var sptExpected = new List<(int destination, double distance)>() { (0, 0), (1, 4), (7, 8), (6, 9), (5, 11), (2, 12), (8, 14), (3, 19), (4, 21) };
      var sptActual = gal.GetDijkstraShortestPathTree(0, o => o.distance).ToList();
      System.Console.WriteLine();
      System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
      foreach (var (destination, distance) in sptActual)
        System.Console.WriteLine($"{destination}={distance}");
      System.Console.WriteLine();
      CollectionAssert.AreEqual(sptExpected, sptActual);
    }
  }
}

