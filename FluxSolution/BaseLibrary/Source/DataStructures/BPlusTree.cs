//https://www.geeksforgeeks.org/deletion-in-b-tree/

//using System;
//using System.Collections.Generic;
//using System.Linq;

//class Node
//{
//  public List<int> Keys { get; set; }
//  public List<Node> Values { get; set; }
//  public bool Leaf { get; set; }
//  public Node Next { get; set; }

//  public Node(bool leaf)
//  {
//    Keys = new List<int>();
//    Values = new List<Node>();
//    Leaf = leaf;
//    Next = null;
//  }
//}

//public class BPlusTree
//{
//  private Node root;
//  private int degree;

//  public BPlusTree(int degree)
//  {
//    root = new Node(true);
//    this.degree = degree;
//  }

//  public bool Search(int key)
//  {
//    Node curr = root;
//    while (!curr.Leaf)
//    {
//      int i = 0;
//      while (i < curr.Keys.Count)
//      {
//        if (key < curr.Keys[i])
//        {
//          break;
//        }
//        i += 1;
//      }
//      curr = curr.Values[i];
//    }
//    int j = 0;
//    while (j < curr.Keys.Count)
//    {
//      if (curr.Keys[j] == key)
//      {
//        return true;
//      }
//      j += 1;
//    }
//    return false;
//  }

//  public void Insert(int key)
//  {
//    Node curr = root;
//    if (curr.Keys.Count == 2 * degree)
//    {
//      Node newRoot = new Node(false);
//      root = newRoot;
//      newRoot.Values.Add(curr);
//      Split(newRoot, 0, curr);
//      InsertNonFull(newRoot, key);
//    }
//    else
//    {
//      InsertNonFull(curr, key);
//    }
//  }

//  private void InsertNonFull(Node curr, int key)
//  {
//    int i = 0;
//    while (i < curr.Keys.Count)
//    {
//      if (key < curr.Keys[i])
//      {
//        break;
//      }
//      i += 1;
//    }
//    if (curr.Leaf)
//    {
//      curr.Keys.Insert(i, key);
//    }
//    else
//    {
//      if (curr.Values[i].Keys.Count == 2 * degree)
//      {
//        Split(curr, i, curr.Values[i]);
//        if (key > curr.Keys[i])
//        {
//          i += 1;
//        }
//      }
//      InsertNonFull(curr.Values[i], key);
//    }
//  }

//  private void Split(Node parent, int index, Node node)
//  {
//    Node new_node = new Node(node.Leaf);
//    parent.Values.Insert(index + 1, new_node);
//    parent.Keys.Insert(index, node.Keys[degree - 1]);

//    new_node.Keys.AddRange(node.Keys.GetRange(degree, node.Keys.Count - degree));
//    node.Keys.RemoveRange(degree - 1, node.Keys.Count - degree + 1);

//    if (!node.Leaf)
//    {
//      new_node.Values.AddRange(node.Values.GetRange(degree, node.Values.Count - degree));
//      node.Values.RemoveRange(degree, node.Values.Count - degree);
//    }
//  }


//  private void StealFromLeft(Node parent, int i)
//  {
//    Node node = parent.Values[i];
//    Node leftSibling = parent.Values[i - 1];
//    node.Keys.Insert(0, parent.Keys[i - 1]);
//    parent.Keys[i - 1] = leftSibling.Keys[leftSibling.Keys.Count - 1];
//    leftSibling.Keys.RemoveAt(leftSibling.Keys.Count - 1);
//    if (!node.Leaf)
//    {
//      node.Values.Insert(0, leftSibling.Values[leftSibling.Values.Count - 1]);
//      leftSibling.Values.RemoveAt(leftSibling.Values.Count - 1);
//    }
//  }

//  private void StealFromRight(Node parent, int i)
//  {
//    Node node = parent.Values[i];
//    Node rightSibling = parent.Values[i + 1];
//    node.Keys.Add(parent.Keys[i]);
//    parent.Keys[i] = rightSibling.Keys[0];
//    rightSibling.Keys.RemoveAt(0);
//    if (!node.Leaf)
//    {
//      node.Values.Add(rightSibling.Values[0]);
//      rightSibling.Values.RemoveAt(0);
//    }
//  }

//  public void Delete(int key)
//  {
//    Node curr = root;
//    bool found = false;
//    int i = 0;
//    while (i < curr.Keys.Count)
//    {
//      if (key == curr.Keys[i])
//      {
//        found = true;
//        break;
//      }
//      else if (key < curr.Keys[i])
//      {
//        break;
//      }
//      i += 1;
//    }
//    if (found)
//    {
//      if (curr.Leaf)
//      {
//        curr.Keys.RemoveAt(i);
//      }
//      else
//      {
//        Node pred = curr.Values[i];
//        if (pred.Keys.Count >= degree)
//        {
//          int predKey = GetMaxKey(pred);
//          curr.Keys[i] = predKey;
//          DeleteFromLeaf(predKey, pred);
//        }
//        else
//        {
//          Node succ = curr.Values[i + 1];
//          if (succ.Keys.Count >= degree)
//          {
//            int succKey = GetMinKey(succ);
//            curr.Keys[i] = succKey;
//            DeleteFromLeaf(succKey, succ);
//          }
//          else
//          {
//            Merge(curr, i, pred, succ);
//            DeleteFromLeaf(key, pred);
//          }
//        }

//        if (curr == root && curr.Keys.Count == 0)
//        {
//          root = curr.Values[0];
//        }
//      }
//    }
//    else
//    {
//      if (curr.Leaf)
//      {
//        return;
//      }
//      else
//      {
//        if (curr.Values[i].Keys.Count < degree)
//        {
//          if (i != 0 && curr.Values[i - 1].Keys.Count >= degree)
//          {
//            StealFromLeft(curr, i);
//          }
//          else if (i != curr.Keys.Count && curr.Values[i + 1].Keys.Count >= degree)
//          {
//            StealFromRight(curr, i);
//          }
//          else
//          {
//            if (i == curr.Keys.Count)
//            {
//              i -= 1;
//            }
//            Merge(curr, i, curr.Values[i], curr.Values[i + 1]);
//          }
//        }

//        Delete(key);
//      }
//    }
//  }

//  private void DeleteFromLeaf(int key, Node leaf)
//  {
//    leaf.Keys.Remove(key);

//    if (leaf == root || leaf.Keys.Count >= Math.Floor(degree / 2.0))
//    {
//      return;
//    }

//    Node parent = FindParent(leaf);
//    int i = parent.Values.IndexOf(leaf);

//    if (i > 0 && parent.Values[i - 1].Keys.Count > Math.Floor(degree / 2.0))
//    {
//      RotateRight(parent, i);
//    }
//    else if (i < parent.Keys.Count && parent.Values[i + 1].Keys.Count > Math.Floor(degree / 2.0))
//    {
//      RotateLeft(parent, i);
//    }
//    else
//    {
//      if (i == parent.Keys.Count)
//      {
//        i -= 1;
//      }
//      Merge(parent, i, parent.Values[i], parent.Values[i + 1]);
//    }
//  }

//  private int GetMinKey(Node node)
//  {
//    while (!node.Leaf)
//    {
//      node = node.Values[0];
//    }
//    return node.Keys[0];
//  }

//  private int GetMaxKey(Node node)
//  {
//    while (!node.Leaf)
//    {
//      node = node.Values[node.Values.Count - 1];
//    }
//    return node.Keys[node.Keys.Count - 1];
//  }

//  private Node FindParent(Node child)
//  {
//    Node curr = root;
//    while (!curr.Leaf)
//    {
//      int i = 0;
//      while (i < curr.Values.Count)
//      {
//        if (child == curr.Values[i])
//        {
//          return curr;
//        }
//        else if (child.Keys[0] < curr.Values[i].Keys[0])
//        {
//          break;
//        }
//        i += 1;
//      }
//      curr = curr.Values[i];
//    }
//    return null;
//  }

//  private void Merge(Node parent, int i, Node pred, Node succ)
//  {
//    pred.Keys.AddRange(succ.Keys);
//    pred.Values.AddRange(succ.Values);
//    parent.Values.RemoveAt(i + 1);
//    parent.Keys.RemoveAt(i);

//    if (parent == root && parent.Keys.Count == 0)
//    {
//      root = pred;
//    }
//  }

//  private void RotateRight(Node parent, int i)
//  {
//    Node node = parent.Values[i];
//    Node prev = parent.Values[i - 1];
//    node.Keys.Insert(0, parent.Keys[i - 1]);
//    parent.Keys[i - 1] = prev.Keys[prev.Keys.Count - 1];
//    prev.Keys.RemoveAt(prev.Keys.Count - 1);
//    if (!node.Leaf)
//    {
//      node.Values.Insert(0, prev.Values[prev.Values.Count - 1]);
//      prev.Values.RemoveAt(prev.Values.Count - 1);
//    }
//  }

//  private void RotateLeft(Node parent, int i)
//  {
//    Node node = parent.Values[i];
//    Node next = parent.Values[i + 1];
//    node.Keys.Add(parent.Keys[i]);
//    parent.Keys[i] = next.Keys[0];
//    next.Keys.RemoveAt(0);
//    if (!node.Leaf)
//    {
//      node.Values.Add(next.Values[0]);
//      next.Values.RemoveAt(0);
//    }
//  }

//  public void PrintTree()
//  {
//    List<Node> currLevel = new List<Node>();
//    currLevel.Add(root);

//    while (currLevel.Count > 0)
//    {
//      List<Node> nextLevel = new List<Node>();

//      foreach (Node node in currLevel)
//      {
//        Console.Write("[" + string.Join(", ", node.Keys.Select(k => k.ToString()).ToArray()) + "] ");

//        if (!node.Leaf)
//        {
//          nextLevel.AddRange(node.Values);
//        }
//      }

//      Console.WriteLine();
//      currLevel = nextLevel;
//    }
//  }

//  public static void Main(string[] args)
//  {
//    // create a B+ tree with degree 3
//    BPlusTree tree = new BPlusTree(3);

//    // insert some keys
//    tree.Insert(1);
//    tree.Insert(2);
//    tree.Insert(3);
//    tree.Insert(4);
//    tree.Insert(5);
//    tree.Insert(6);
//    tree.Insert(7);
//    tree.Insert(8);
//    tree.Insert(9);

//    // print the tree
//    tree.PrintTree(); // [4] [2, 3] [6, 7, 8, 9] [1] [5]

//    // delete a key
//    tree.Delete(3);

//    // print the tree
//    tree.PrintTree(); // [4] [2] [6, 7, 8, 9] [1] [5]
//  }
//}
