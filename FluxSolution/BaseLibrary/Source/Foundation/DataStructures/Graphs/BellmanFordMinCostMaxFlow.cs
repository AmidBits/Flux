//namespace Flux.DataStructures.Graphs
//{
//  // https://www.geeksforgeeks.org/minimum-cost-maximum-flow-from-a-graph-using-bellman-ford-algorithm/
//  public class BellmanFordMinCostMaxFlow
//  {
//    // Stores the found edges
//    private readonly bool[] m_found;

//    // Stores the number of nodes
//    private readonly int m_numberOfVertices;

//    private readonly double[,] m_flow;

//    // Stores the distance from each node and picked edges for each node
//    private readonly int[] m_dad;
//    private readonly double[] m_dist;
//    private readonly double[] m_pi;

//    // Stores the capacity of each edge
//    public double[,] Capacity;
//    // Stores the cost per unit flow of each edge
//    public double[,] Cost;

//    public BellmanFordMinCostMaxFlow(double[,] capacity, double[,] cost)
//    {
//      Capacity = capacity;
//      Cost = cost;

//      m_numberOfVertices = System.Linq.Enumerable.Single(System.Linq.Enumerable.Distinct(new[] { capacity.GetLength(0), capacity.GetLength(1), cost.GetLength(0), cost.GetLength(1) }));

//      m_found = new bool[m_numberOfVertices];
//      m_flow = new double[m_numberOfVertices, m_numberOfVertices];
//      m_dist = new double[m_numberOfVertices + 1];
//      m_dad = new int[m_numberOfVertices];
//      m_pi = new double[m_numberOfVertices];
//    }
//    public BellmanFordMinCostMaxFlow(int numberOfVertices)
//      : this(new double[numberOfVertices, numberOfVertices], new double[numberOfVertices, numberOfVertices])
//    { }

//    // Determine if it is possible to have a flow from the source to target.
//    public bool Search(int source, int target)
//    {
//      System.Array.Fill(m_found, false); // Initialise found[] to false.

//      System.Array.Fill(m_dist, double.PositiveInfinity); // Initialise the dist[] to INF.

//      m_dist[source] = 0; // Distance from the source node.

//      while (source != m_numberOfVertices) // Iterate until source reaches the number of vertices.
//      {
//        var best = m_numberOfVertices;

//        m_found[source] = true;

//        for (var k = 0; k < m_numberOfVertices; k++)
//        {
//          if (m_found[k]) // If already found, continue.
//            continue;

//          if (m_flow[k, source] != 0) // Evaluate while flow is still in supply.
//          {
//            var val = m_dist[source] + m_pi[source] - m_pi[k] - Cost[k, source]; // Obtain the total value.

//            if (m_dist[k] > val)// If dist[k] is > minimum value, update.
//            {
//              m_dist[k] = val;
//              m_dad[k] = source;
//            }
//          }

//          if (m_flow[source, k] < Capacity[source, k])
//          {
//            var val = m_dist[source] + m_pi[source] - m_pi[k] + Cost[source, k];

//            if (m_dist[k] > val) // If dist[k] is > minimum value, update.
//            {
//              m_dist[k] = val;
//              m_dad[k] = source;
//            }
//          }

//          if (m_dist[k] < m_dist[best])
//            best = k;
//        }

//        source = best; // Update src to best for next iteration.
//      }

//      for (int k = 0; k < m_numberOfVertices; k++)
//        m_pi[k] = System.Math.Min(m_pi[k] + m_dist[k], double.PositiveInfinity);

//      return m_found[target]; // Return the value obtained at target.
//    }

//    // Obtain the maximum Flow.
//    public (double totalFlow, double totalCost) GetMaxFlow(int source, int target)
//    {
//      var totalFlow = 0d;
//      var totalCost = 0d;

//      while (Search(source, target)) // If a path exist from source to target.
//      {
//        var amt = double.PositiveInfinity; // Set the default amount.

//        for (var x = target; x != source; x = m_dad[x])
//          amt = System.Math.Min(amt, m_flow[x, m_dad[x]] != 0 ? m_flow[x, m_dad[x]] : Capacity[m_dad[x], x] - m_flow[m_dad[x], x]);

//        for (var x = target; x != source; x = m_dad[x])
//        {
//          if (m_flow[x, m_dad[x]] != 0)
//          {
//            m_flow[x, m_dad[x]] -= amt;
//            totalCost -= amt * Cost[x, m_dad[x]];
//          }
//          else
//          {
//            m_flow[m_dad[x], x] += amt;
//            totalCost += amt * Cost[m_dad[x], x];
//          }
//        }

//        totalFlow += amt;
//      }

//      return (totalFlow, totalCost); // Return pair total flow and cost.
//    }
//  }
//}

///*
//      int s = 0, t = 4;

//      var cap68 = new double[,] {
//        { 0, 3, 1, 0, 3 },
//        { 0, 0, 2, 0, 0 },
//        { 0, 0, 0, 1, 6 },
//        { 0, 0, 0, 0, 2 },
//        { 0, 0, 0, 0, 0 },
//      };

//      var cost68 = new double[,] {
//        { 0, 1, 0, 0, 2 },
//        { 0, 0, 0, 3, 0 },
//        { 0, 0, 0, 0, 0 },
//        { 0, 0, 0, 0, 1 },
//        { 0, 0, 0, 0, 0 }
//      };
//      // 6, 8

//      var cap101 = new double[,] {
//        { 0, 3, 4, 5, 0 },
//        { 0, 0, 2, 0, 0 },
//        { 0, 0, 0, 4, 1 },
//        { 0, 0, 0, 0, 10 },
//        { 0, 0, 0, 0, 0 }
//      };
//      var cost101 = new double[,] {
//        { 0, 1, 0, 0, 0 },
//        { 0, 0, 0, 0, 0 },
//        { 0, 0, 0, 0, 0 },
//        { 0, 0, 0, 0, 0 },
//        { 0, 0, 0, 0, 0 }
//      };
//      // 10, 1

//      var flow = new Flux.DataStructures.Graphs.BellmanFordMinCostMaxFlow(cap68,cost68);

//      var ret = flow.GetMaxFlow(s, t);
//*/
