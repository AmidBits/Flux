<<<<<<< HEAD
﻿namespace Flux.Csp
{
  internal class Node
  {
    internal System.Collections.Generic.LinkedList<Node> AdjoiningNodes { get; set; }
    internal int CycleIndex { get; set; }
    internal int Index { get; set; }
    internal string Label { get; private set; }
    internal int Link { get; set; }

    internal Node()
    {
      AdjoiningNodes = new System.Collections.Generic.LinkedList<Node>();
      CycleIndex = -1;
      Index = -1;
      Label = string.Empty;
      Link = -1;
    }

    internal Node(string label)
      : this() 
      => Label = label;

    public override string ToString() 
      => Label;
  }
}
=======
﻿//namespace Flux.Csp
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
>>>>>>> 5d90945ace2018ef216bdfb2933f94f9d71845a4
