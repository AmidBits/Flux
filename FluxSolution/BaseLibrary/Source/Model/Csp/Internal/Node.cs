//namespace Flux.Csp
//{
//  internal class Node
//  {
//    internal int Index { get; set; }
//    internal int Link { get; set; }
//    internal System.Collections.Generic.LinkedList<Node> AdjoiningNodes { get; set; }
//    internal string Label { get; private set; }
//    internal int CycleIndex { get; set; }

//    internal Node()
//    {
//      this.AdjoiningNodes = new System.Collections.Generic.LinkedList<Node>();
//      this.Index = -1;
//      this.Link = -1;
//      this.CycleIndex = -1;
//    }

//    internal Node(string label)
//      : this()
//    {
//      this.Label = label;
//    }

//    public override string ToString()
//    {
//      return this.Label;
//    }
//  }
//}
