//namespace Flux.Csp
//{
//  internal class Node
//  {
//    internal System.Collections.Generic.LinkedList<Node> AdjoiningNodes { get; set; }
//    internal int CycleIndex { get; set; }
//    internal int Index { get; set; }
//    internal string Label { get; private set; }
//    internal int Link { get; set; }

//    internal Node()
//    {
//      AdjoiningNodes = new System.Collections.Generic.LinkedList<Node>();
//      CycleIndex = -1;
//      Index = -1;
//      Label = string.Empty;
//      Link = -1;
//    }

//    internal Node(string label)
//      : this()
//      => Label = label;

//    public override string ToString()
//      => Label;
//  }
//}