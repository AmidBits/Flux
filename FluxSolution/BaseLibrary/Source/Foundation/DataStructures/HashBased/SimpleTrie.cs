namespace Flux.Collections.Generic
{
  /// <summary></summary>
  /// <see cref="https://en.wikipedia.org/wiki/Trie"/>
  /// <seealso cref="https://github.com/gmamaladze/trienet/tree/master/TrieNet"/>
  public class SimpleTrie<TKey>
    where TKey : System.IEquatable<TKey>
  {
    private readonly Node m_root = new Node(false);

    public int Count { get; private set; }

    /// <summary>Delete an entry set of <typeparamref name="TKey"/>.</summary>
    /// <param name="entry"></param>
    public void Delete(System.ReadOnlySpan<TKey> entry)
    {
      if (DeleteEntryOrToEnd(m_root, 0, entry))
        Count--;
    }
    private bool DeleteEntryOrToEnd(Node root, int index, System.ReadOnlySpan<TKey> array)
    {
      if (index == array.Length)
      {
        if (root.EndOfEntry && root.SubNodes.Count != 0)
          return root.EndOfEntry = false;

        return root.SubNodes.Count == 0;
      }

      if (!root.SubNodes.ContainsKey(array[index]))
        return false;

      if (DeleteEntryOrToEnd(root.SubNodes[array[index]], index + 1, array))
      {
        root.SubNodes.Remove(array[index]);

        if (index > 0 && root.EndOfEntry)
          return false;
      }

      return root.SubNodes.Count == 0;
    }

    /// <summary>Insert an entry set of <typeparamref name="TKey"/>.</summary>
    /// <param name="entry"></param>
    public void Insert(System.ReadOnlySpan<TKey> entry)
    {
      var temp = m_root;

      for (int index = 0; index < entry.Length; index++)
      {
        if (temp.SubNodes.Keys.Contains(entry[index]))
        {
          temp = temp.SubNodes[entry[index]];
        }
        else
        {
          var node = new Node(index == entry.Length - 1);

          temp.SubNodes.Add(entry[index], node);

          Count++;

          temp = node;
        }
      }
    }

    /// <summary>Search for an entry set of <typeparamref name="TKey"/>. Optionally accept a partial set (excluding at the end).</summary>
    /// <param name="entry"></param>
    public bool Search(bool acceptStartsWith, System.ReadOnlySpan<TKey> entry)
    {
      var node = m_root;

      for (int index = 0; index < entry.Length; index++)
      {
        if (node.SubNodes.Keys.Contains(entry[index]))
        {
          node = node.SubNodes[entry[index]];

          if (!acceptStartsWith && node.EndOfEntry && index == entry.Length - 1)
          {
            return true;
          }
        }
        else return false;
      }

      return acceptStartsWith;
    }

    private class Node
    {
      public bool EndOfEntry;

      public readonly System.Collections.Generic.IDictionary<TKey, Node> SubNodes;

      public Node(bool endOfEntry)
      {
        EndOfEntry = endOfEntry;

        SubNodes = new System.Collections.Generic.Dictionary<TKey, Node>();
      }
    }
  }
}

/*
  SimpleTrie<char> st = new SimpleTrie<char>();

  t.Add("abc");
  t.Add("abgl");
  t.Add("cdf");
  t.Add("abcd");
  t.Add("lmn");

  bool findPrefix1 = t.Search("ab", true);
  bool findPrefix2 = t.Search("lo", true);

  bool findWord1 = t.Search("lmn", false);
  bool findWord2 = t.Search("ab", false);
  bool findWord3 = t.Search("cdf", false);
  bool findWord4 = t.Search("ghi", false);

  t.Delete("abc");
  t.Delete("abgl");
  t.Delete("abcd");
  t.Delete("xyz");
*/
