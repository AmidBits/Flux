namespace Flux.DataStructures
{
  /// <summary>A simple implementation of a trie data structure, which essentially is a storage structure for storing sequential data by their smaller components, e.g. <see cref="string"/>s (where the storage unit is <see cref="char"/>).</summary>
  /// <see href="https://en.wikipedia.org/wiki/Trie"/>
  /// <seealso cref="https://github.com/gmamaladze/trienet/tree/master/TrieNet"/>
  public sealed class SimpleTrie<TKey>
    where TKey : notnull
  {
    private readonly Node m_root = new(false);

    public int Count() => TrieCount(m_root);

    /// <summary>Delete an entry set of <typeparamref name="TKey"/>.</summary>
    /// <param name="key"></param>
    public void Delete(System.ReadOnlySpan<TKey> key) => TrieDelete(m_root, key);

    /// <summary>Search for an entry set of <typeparamref name="TKey"/>. Optionally accept a partial set (excluding at the end).</summary>
    /// <param name="key"></param>
    public bool Find(bool acceptStartsWith, System.ReadOnlySpan<TKey> key) => acceptStartsWith ? TrieStartsWith(m_root, key) : TrieFind(m_root, key);

    /// <summary>Insert an entry set of <typeparamref name="TKey"/>.</summary>
    /// <param name="key"></param>
    public void Insert(System.ReadOnlySpan<TKey> key) => TrieInsert(m_root, key);

    #region Static methods

    private static int TrieCount(Node node)
    {
      var count = 0;

      foreach (var kvp in node.Children)
      {
        if (kvp.Value.IsTerminal)
          count++;

        count += TrieCount(kvp.Value);
      }

      return count;
    }

    private static bool TrieDelete(Node node, System.ReadOnlySpan<TKey> set)
    {
      if (set.Length == 0) // We have recursed the entire key.
      {
        if (node.IsTerminal && node.Children.Count != 0) // We have a full terminal key, but there are subkeys consisting of the same key data..
          return (node.IsTerminal = false); // ..so we cannot allow the key data to be deleted, instead we mark the terminal key as no longer being terminal.

        return node.Children.Count == 0; // If there is no more key data, there are no subkeys, so we allow all key data to be deleted.
      }

      var keyStone = set[0];

      if (!node.Children.ContainsKey(keyStone))
        return false;

      if (TrieDelete(node.Children[keyStone], set[1..]))
        node.Children.Remove(keyStone);

      return node.Children.Count == 0;
    }

    private static bool TrieFind(Node node, System.ReadOnlySpan<TKey> set)
    {
      for (int index = 0; index < set.Length; index++)
      {
        if (!node.Children.TryGetValue(set[index], out var temp))
          break;

        node = temp;

        if (node.IsTerminal && index == set.Length - 1)
          return true;
      }

      return false;
    }

    private static void TrieInsert(Node node, System.ReadOnlySpan<TKey> set)
    {
      for (int index = 0; index < set.Length; index++)
      {
        if (!node.Children.TryGetValue(set[index], out var temp))
        {
          temp = new Node(index == set.Length - 1);

          node.Children.Add(set[index], temp);
        }

        node = temp;
      }
    }

    private static bool TrieStartsWith(Node node, System.ReadOnlySpan<TKey> set)
    {
      for (int index = 0; index < set.Length; index++)
      {
        if (!node.Children.TryGetValue(set[index], out var temp))
          return false;

        node = temp;
      }

      return true;
    }

    #endregion // Static methods

    public string ToConsoleString()
    {
      var sb = new SpanBuilder<char>();

      AddString(m_root, 0);

      return sb.ToString();

      void AddString(Node node, int level)
      {
        foreach (var childNode in node.Children)
        {
          sb.Append(string.Empty.PadLeft(level == 0 ? 0 : level + 1), 1);
          if (level == 0) sb.Append('[', 1);
          sb.Append(childNode.Key.ToString(), 1);
          if (childNode.Value.IsTerminal) sb.Append("]", 1);
          else sb.Append($"#{childNode.Value.Children.Count}", 1);
          sb.Append(System.Environment.NewLine, 1);

          AddString(childNode.Value, level + 1);
        }
      }
    }

    public override string ToString() => $"{GetType().Name} {{ Count = {Count()} }}";

    private record class Node
    {
      public readonly System.Collections.Generic.IDictionary<TKey, Node> Children;
      public bool IsTerminal;

      public Node(bool isTerminal)
      {
        Children = new System.Collections.Generic.Dictionary<TKey, Node>();
        IsTerminal = isTerminal;
      }

      //public override string ToString() => $"Children = {Children.Count}, IsTerminal = {IsTerminal}";
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
