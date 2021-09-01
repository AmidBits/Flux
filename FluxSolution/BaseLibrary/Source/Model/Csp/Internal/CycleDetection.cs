//using System.Linq;

//namespace Flux.Csp
//{
//	internal class CycleDetection
//	{
//		private int index;
//		private System.Collections.Generic.Stack<Node> nodeStack;

//		internal Graph Graph { get; set; } = new Graph();
//		internal System.Collections.Generic.List<System.Collections.Generic.List<Node>> StronglyConnectedComponents { get; set; }

//		internal void DetectCycle()
//		{
//			index = 0;
//			nodeStack = new System.Collections.Generic.Stack<Node>();

//			StronglyConnectedComponents = new System.Collections.Generic.List<System.Collections.Generic.List<Node>>();

//			var cycles = 0;
//			foreach (var node in this.Graph.Nodes.Where(node => node.Index < 0))
//				Connection(node, ref cycles);
//		}

//		private void Connection(Node node, ref int cycles)
//		{
//			node.Index = index;
//			node.Link = index;
//			index++;
//			nodeStack.Push(node);

//			foreach (var adjoiningNode in node.AdjoiningNodes)
//			{
//				if (adjoiningNode.Index < 0)
//				{
//					Connection(adjoiningNode, ref cycles);
//					node.Link = System.Math.Min(node.Link, adjoiningNode.Link);
//				}
//				else if (nodeStack.Contains(adjoiningNode))
//				{
//					node.Link = System.Math.Min(node.Link, adjoiningNode.Index);
//				}
//			}

//			if (node.Link != node.Index)
//				return;

//			var stronglyConnectedComponent = new System.Collections.Generic.List<Node>();

//			Node lastNode;

//			do
//			{
//				lastNode = nodeStack.Pop();
//				lastNode.CycleIndex = cycles;
//				stronglyConnectedComponent.Add(lastNode);
//			} while (node != lastNode);

//			++cycles;

//			StronglyConnectedComponents.Add(stronglyConnectedComponent);
//		}
//	}
//}
