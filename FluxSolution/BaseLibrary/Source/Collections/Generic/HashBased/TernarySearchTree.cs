//using System.Linq;

//namespace Flux.Collections.Generic
//{
//  // https://stackoverflow.com/questions/61210562/what-is-the-name-of-this-data-structure-search-algorithm
//  public class TernarySearchTree<T>
//    where T : System.IComparable<T>
//  {
//    private Node m_root = default;

//    public void Insert(ref object data, System.Collections.Generic.IList<T> keys)
//    {
//      if (keys is null) throw new System.ArgumentNullException(nameof(keys));

//      var _this = this;

//      Insert(ref _this, ref data, keys);
//    }

//    private static void Insert(ref TernarySearchTree<T> trie, ref object data, System.Collections.Generic.IList<T> keys)
//    {
//      if (trie.m_root is null)
//      {
//        trie.m_root = new Node() { Key = keys[0] };
//        keys.RemoveAt(0);

//        if (keys.Count > 0)
//        {
//          trie.m_root.NextTree.Insert(ref data, keys);
//          return;
//        }
//        else
//        {
//          trie.m_root.Data = data;
//          return;
//        }
//      }

//      var node = trie.m_root;

//      do
//      {
//        var compare = keys[0].CompareTo(node.Key);

//        if (compare > 0)
//        {
//          if (node.Right is null)
//          {
//            node.Right = new Node() { Key = keys[0] };
//            keys.RemoveAt(0);

//            if (keys.Count > 0)
//            {
//              node.Right.NextTree.Insert(ref data, keys);
//              return;
//            }
//            else
//            {
//              node.Right.Data = data;
//              return;
//            }
//          }
//          else node = node.Right;
//        }
//        else if (compare < 0)
//        {
//          if (node.Left is null)
//          {
//            node.Left = new Node() { Key = keys[0] };
//            keys.RemoveAt(0);

//            if (keys.Count > 0)
//            {
//              node.Left.NextTree.Insert(ref data, keys);
//              return;
//            }
//            else
//            {
//              node.Left.Data = data;
//              return;
//            }
//          }
//          else node = node.Left;
//        }
//        else
//        {
//          keys.RemoveAt(0);

//          if (keys.Count > 0)
//          {
//            node.NextTree.Insert(ref data, keys);
//            return;
//          }
//          else
//          {
//            node.Data = data;
//            return;
//          }
//        }
//      }
//      while (true);
//    }

//    public object Search(System.Collections.Generic.IList<T> keys)
//    {
//      if (keys is null) throw new System.ArgumentNullException(nameof(keys));

//      var _this = this;

//      return Search(ref _this, keys);
//    }

//    private static object Search(ref TernarySearchTree<T> trie, System.Collections.Generic.IList<T> keys)
//    {
//      if (trie.m_root is null) return null;

//      var node = trie.m_root;

//      do
//      {
//        var compare = keys[0].CompareTo(node.Key);

//        if (compare > 0)
//        {
//          if (node.Right is null) return null;
//          else node = node.Right;
//        }
//        else if (compare < 0)
//        {
//          if (node.Left is null) return null;
//          else node = node.Left;
//        }
//        else
//        {
//          keys.RemoveAt(0);

//          if (keys.Count > 0) return node.NextTree.Search(keys);
//          else return node.Data;
//        }
//      }
//      while (true);
//    }

//    private class Node
//    {
//      public T Key = default;
//      public Node Left = null;
//      public Node Right = null;
//      public TernarySearchTree<T> NextTree = new TernarySearchTree<T>();
//      public object Data = default;
//    }
//  }
//}
