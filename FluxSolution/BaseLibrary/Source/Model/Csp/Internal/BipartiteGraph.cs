//using System.Linq;

//namespace Flux.Csp
//{
//  internal class BipartiteGraph : Graph
//  {
//    internal System.Collections.Generic.Dictionary<int, NodeVariable> Variables { get; set; }
//    internal System.Collections.Generic.Dictionary<int, NodeValue> Values { get; set; }
//    internal System.Collections.Generic.Dictionary<Node, int> Distance { get; set; }
//    internal System.Collections.Generic.Dictionary<Node, Node> Pair { get; set; }

//    public Node NullNode { get; private set; }
//    private System.Collections.Generic.Queue<Node> queue;

//    internal BipartiteGraph(System.Collections.Generic.IEnumerable<VariableInteger> variables)
//    {
//      this.Variables = new System.Collections.Generic.Dictionary<int, NodeVariable>();
//      this.Values = new System.Collections.Generic.Dictionary<int, NodeValue>();
//      var linkedList = new System.Collections.Generic.LinkedList<Node>();

//      int index = 0;
//      foreach (var variable in variables)
//      {
//        this.Variables[index] = new NodeVariable(variable);
//        linkedList.AddLast(this.Variables[index]);
//        foreach (int value in variable.Domain)
//        {
//          if (!this.Values.ContainsKey(value))
//          {
//            this.Values[value] = new NodeValue(value);
//            linkedList.AddLast(this.Values[value]);
//          }

//          this.Variables[index].AdjoiningNodes.AddLast(this.Values[value]);
//          this.Values[value].AdjoiningNodes.AddLast(this.Variables[index]);
//        }

//        ++index;
//      }

//      this.Nodes = new System.Collections.Generic.List<Node>(linkedList);
//    }

//    internal int MaximalMatching()
//    {
//      var matching = 0;
//      this.NullNode = new Node("NULL");
//      this.queue = new System.Collections.Generic.Queue<Node>();
//      this.Pair = new System.Collections.Generic.Dictionary<Node, Node>(this.Nodes.Count);
//      this.Distance = new System.Collections.Generic.Dictionary<Node, int>(this.Nodes.Count);
//      foreach (var node in this.Nodes)
//        this.Pair[node] = this.NullNode;

//      while (BreadthFirstSearch())
//      {
//        matching += this.Variables.Values.Count(node => this.Pair[node] == this.NullNode && DepthFirstSearch(node));
//      }

//      //--- store current graph in stack

//      UndirectedToDirected();

//      return matching;
//    }

//    private void UndirectedToDirected()
//    {
//      var toNullNode = new System.Collections.Generic.LinkedList<Node>();
//      foreach (var node in this.Variables.Values)
//      {
//        node.AdjoiningNodes = new System.Collections.Generic.LinkedList<Node>(new[] { this.Pair[node] });
//        this.Pair[node].AdjoiningNodes.Remove(node);
//        this.Pair[node].AdjoiningNodes.AddFirst(this.NullNode);
//        toNullNode.AddLast(this.Pair[node]);
//      }

//      foreach (var node in this.Values.Values.Except(toNullNode))
//      {
//        this.NullNode.AdjoiningNodes.AddLast(node);
//      }
//    }

//    private bool BreadthFirstSearch()
//    {
//      foreach (var node in this.Variables.Values)
//        if (this.Pair[node] == this.NullNode)
//        {
//          this.Distance[node] = 0;
//          this.queue.Enqueue(node);
//        }
//        else
//          this.Distance[node] = System.Int32.MaxValue;

//      this.Distance[this.NullNode] = System.Int32.MaxValue;

//      while (this.queue.Any())
//      {
//        var node = this.queue.Dequeue();
//        foreach (var adjoinedNode in node.AdjoiningNodes.
//          Where(adjoinedNode => this.Distance[this.Pair[adjoinedNode]] == System.Int32.MaxValue))
//        {
//          this.Distance[this.Pair[adjoinedNode]] = this.Distance[node] + 1;
//          this.queue.Enqueue(this.Pair[adjoinedNode]);
//        }
//      }

//      return this.Distance[this.NullNode] != System.Int32.MaxValue;
//    }

//    private bool DepthFirstSearch(Node node)
//    {
//      if (node == this.NullNode)
//        return true;

//      foreach (var adjoinedNode in node.AdjoiningNodes.Where(adjoinedNode =>
//        (this.Distance[this.Pair[adjoinedNode]] == this.Distance[node] + 1) &&
//        DepthFirstSearch(this.Pair[adjoinedNode])))
//      {
//        this.Pair[adjoinedNode] = node;
//        this.Pair[node] = adjoinedNode;
//        return true;
//      }

//      this.Distance[node] = System.Int32.MaxValue;
//      return false;
//    }
//  }
//}
