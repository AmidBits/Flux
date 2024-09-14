namespace Flux.DataStructures
{
  public record class Trie<TKey, TValue>
    : ITrie<TKey, TValue>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.Dictionary<TKey, Trie<TKey, TValue>> m_children;
    private readonly TValue m_value;

    protected Trie(System.Collections.Generic.Dictionary<TKey, Trie<TKey, TValue>> dictionary, TValue value)
    {
      m_children = new(dictionary ?? new());
      m_value = value;
    }
    public Trie() : this(default!, default!) { }

    public System.Collections.Generic.IReadOnlyDictionary<TKey, ITrie<TKey, TValue>> Children => (System.Collections.Generic.IReadOnlyDictionary<TKey, ITrie<TKey, TValue>>)m_children;
    public TValue Value => m_value;

    public int Delete(ReadOnlySpan<TKey> keySpan) => Delete(this, keySpan);

    public bool Find(ReadOnlySpan<TKey> keySpan, out TValue value) => Find(this, keySpan, out value);

    /// <summary>
    /// <para>Tries to find the <paramref name="keySpan"/> and if so, returns the <paramref name="count"/> of children (as an out parameter) at the end point.</para>
    /// </summary>
    /// <param name="keySpan"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool FindStartsWith(ReadOnlySpan<TKey> keySpan, out int count) => FindStartsWith(this, keySpan, out count);

    public int Insert(ReadOnlySpan<TKey> keySpan, TValue value) => Insert(this, keySpan, value);

    public int InsertRange<TSource>(System.Collections.Generic.IEnumerable<TSource> collection, System.Func<TSource, System.Collections.Generic.IEnumerable<TKey>> keySelector, System.Func<TSource, TValue> valueSelector)
    {
      var insertCount = 0;

      foreach (var item in collection)
        insertCount += Insert(keySelector(item) switch { System.Collections.Generic.List<TKey> l => l.AsSpan(), TKey[] a => a, var o => o.ToArray() }, valueSelector(item));

      return insertCount;
    }

    public string ToConsoleString()
    {
      var sb = new System.Text.StringBuilder();

      AddString(this, 0);

      return sb.ToString();

      void AddString(Trie<TKey, TValue> node, int level)
      {
        foreach (var childNode in node.m_children)
        {
          sb.Append(string.Empty.PadLeft(level == 0 ? 0 : level + 1));
          if (level == 0) sb.Append('[', 1);
          sb.Append(childNode.Key.ToString());
          sb.Append(childNode.Value.ToString());
          //if (childNode.Value.IsTerminal) sb.Append("]", 1);
          //else sb.Append($"#{childNode.Value.Children.Count}", 1);
          //sb.Append($" ({childNode.Value.Value})", 1);
          sb.AppendLine();

          AddString(childNode.Value, level + 1);
        }
      }
    }

    #region Static methods

    public static int Delete(Trie<TKey, TValue> node, ReadOnlySpan<TKey> keySpan)
    {
      var deleteCount = 0;

      Delete(node, keySpan);

      return deleteCount; // System.Diagnostics.Debug.WriteLine($"Deleted {deleteCount} keys.");

      bool Delete(Trie<TKey, TValue> node, ReadOnlySpan<TKey> keySpan)
      {
        if (keySpan.Length > 0)
        {
          var key = keySpan[0]; // The first item in the span.

          if (node.m_children.TryGetValue(key, out var tmp))
          {
            var subKeySpan = keySpan[1..]; // The rest of the items in the span.

            if (Delete(tmp, subKeySpan))
            {
              if (((subKeySpan.Length == 0 && tmp is TerminalTrie) || (subKeySpan.Length > 0 && tmp is not TerminalTrie)) && tmp.m_children.Count == 0)
              {
                node.m_children.Remove(key);

                deleteCount++;

                return true;
              }
              else if (subKeySpan.Length == 0 && tmp is TerminalTrie && tmp.m_children.Count != 0) // We need to rebuild the last key node with terminal = false, since this is no longer a key-span.
                node.m_children[key] = new Trie<TKey, TValue>(tmp.m_children, tmp.m_value);
            }
          }
        }
        else // Now trace all recursed nodes back.
          return true;

        return false;
      }
    }

    public static bool Find(Trie<TKey, TValue> node, ReadOnlySpan<TKey> keySpan, out TValue value)
    {
      var maxIndex = keySpan.Length - 1;

      for (int index = 0; index <= maxIndex; index++)
      {
        if (!node.m_children.TryGetValue(keySpan[index], out var tmp))
          break;

        node = tmp;

        if (node is TerminalTrie && index == maxIndex)
        {
          value = node.m_value;
          return true;
        }
      }

      value = default!;
      return false;
    }

    public static bool FindStartsWith(Trie<TKey, TValue> node, System.ReadOnlySpan<TKey> keySpan, out int count)
    {
      for (int index = 0; index < keySpan.Length; index++)
      {
        if (!node.m_children.TryGetValue(keySpan[index], out var tmp))
        {
          count = default;
          return false;
        }

        node = tmp;
      }

      count = node.m_children.Count;
      return true;
    }

    public static int Insert(Trie<TKey, TValue> node, ReadOnlySpan<TKey> keySpan, TValue value)
    {
      var insertCount = 0;

      for (int index = 0, maxIndex = keySpan.Length - 1; index <= maxIndex; index++)
      {
        var key = keySpan[index];

        if (!node.m_children.TryGetValue(key, out var tmp))
        {
          tmp = (index == maxIndex) ? new TerminalTrie(value) : new Trie<TKey, TValue>(default!, default!);

          node.m_children.Add(key, tmp);

          insertCount++;
        }

        if (index == maxIndex && insertCount == 0) // Here we have to re-build the last key node with terminal = true, since this is now a new key-span.
          node.m_children[key] = new TerminalTrie(tmp.m_children, tmp.m_value);

        node = tmp;
      }

      return insertCount; // System.Diagnostics.Debug.WriteLine($"Inserted {insertCount} keys.");
    }

    #endregion // Static methods

    public override string ToString() => $"({m_children.Count}) {(m_value is null || m_value.Equals(default!) ? string.Empty : $"({m_value})")}";

    private record class TerminalTrie
      : Trie<TKey, TValue>
    {
      public TerminalTrie(System.Collections.Generic.Dictionary<TKey, Trie<TKey, TValue>> dictionary, TValue value) : base(dictionary, value) { }
      public TerminalTrie(TValue value) : base(default!, value) { }

      public override string ToString() => $"] {(m_value is null || m_value.Equals(default!) ? string.Empty : $"({m_value})")}";
    }
  }
}

/*
  var t = new Flux.DataStructures.Trie<char, string>();

  //foreach (var d in System.IO.Directory.GetDirectories(@"C:\Users\Rob\OneDrive\", "*", System.IO.SearchOption.AllDirectories))
  //  t.Insert(d);

  t.Insert(" ", null);
  t.Delete(" ");

  t.Insert("abc", null);
  t.Insert("abgl", null);
  t.Insert("cdf", null);
  t.Insert("abcd", null);
  t.Insert("lmn", null);

  var b = t.FindStartsWith("ab", out var val);

  System.Console.WriteLine(t.ToConsoleString());

  bool findPrefix1 = t.FindStartsWith("ab", out var _);
  bool findPrefix2 = t.FindStartsWith("lo", out var _);

  bool findWord1 = t.Find("lmn", out var _);
  bool findWord2 = t.Find("ab", out var _);
  bool findWord3 = t.Find("cdf", out var _);
  bool findWord4 = t.Find("ghi", out var _);
  bool findWord5 = t.FindStartsWith("abc", out var _);

  t.Delete("abc");
  bool findWord6 = t.FindStartsWith("abc", out var _);
  bool findWord7 = t.Find("abc", out var _);
  t.Delete("abgl");
  t.Delete("abcd");
  t.Delete("lmn");
  t.Delete("xyz");
  t.Delete("cdf");
*/
