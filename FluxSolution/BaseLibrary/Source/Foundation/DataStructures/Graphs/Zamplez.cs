namespace Flux
{
  public static partial class Zamplez
  {
    public static void RunDataStructuresGraphs()
    {
      System.Console.WriteLine(nameof(RunDataStructuresGraphs));
      System.Console.WriteLine();

      AdjacencyList();

      AdjacencyMatrix1();
      AdjacencyMatrix2();

      static void AdjacencyList()
      {
        var gal = new Flux.DataStructures.Graphs.AdjacencyList();

        gal.AddVertex(0);
        gal.AddVertex(1);
        gal.AddVertex(2);
        gal.AddVertex(3);
        gal.AddVertex(4);

        // 6, 8
        gal.AddEdge(0, 1, (3, 1));
        gal.AddEdge(0, 2, (1, 0));
        gal.AddEdge(0, 4, (3, 2));
        gal.AddEdge(1, 2, (2, 0));
        gal.AddEdge(1, 3, (0, 3));
        gal.AddEdge(2, 3, (1, 0));
        gal.AddEdge(2, 4, (6, 0));
        gal.AddEdge(3, 4, (2, 1));

        // 10, 1
        //gal.AddEdge(0, 1, (3, 1));
        //gal.AddEdge(0, 2, (4, 0));
        //gal.AddEdge(0, 3, (5, 0));
        //gal.AddEdge(1, 2, (2, 0));
        //gal.AddEdge(2, 3, (4, 0));
        //gal.AddEdge(2, 4, (1, 0));
        //gal.AddEdge(3, 4, (10, 0));

        System.Console.WriteLine(gal.ToConsoleString());

        var spt = gal.GetDijkstraShortestPathTree(0, o => ((System.ValueTuple<int, int>)o).Item2);
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination}={distance}");
        System.Console.WriteLine();
      }

      static void AdjacencyMatrix1()
      {
        var gam = new Flux.DataStructures.Graphs.AdjacencyMatrix();

        gam.AddVertex(0);
        gam.AddVertex(1);
        gam.AddVertex(2);
        gam.AddVertex(3);
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

        var spt = gam.GetDijkstraShortestPathTree(0, o => ((System.ValueTuple<int, int>)o).Item2);
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination}={distance}");
        System.Console.WriteLine();
      }

      static void AdjacencyMatrix2()
      {
        var gam = new Flux.DataStructures.Graphs.AdjacencyMatrix();

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

        var mcmf = gam.GetBellmanFordMaxFlowMinCost(0, 4, o => o is null ? 0 : (int)o, o => o is null ? 0 : (int)o);
        System.Console.WriteLine($"BellmanFord Min-Cost-Max-Flow: {mcmf}");
        System.Console.WriteLine();

        var spt = gam.GetDijkstraShortestPathTree(0, o => (int)o);
        System.Console.WriteLine("Dijkstra 'Shortest Path Tree' (a.k.a. SPT) from vertex 0 (destination, distance):");
        foreach (var (destination, distance) in spt)
          System.Console.WriteLine($"{destination}={distance}");
        System.Console.WriteLine();
      }
    }
  }
}
