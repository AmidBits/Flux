
namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A simple implementation of a trie data structure, which essentially is a storage structure for storing sequential data by their smaller components, e.g. <see cref="string"/>s (where the storage unit is <see cref="char"/>).</para>
  /// <para>This data structure is good for things like file paths and names (e.g. street names) where many variations of similar names exists.</para>
  /// </summary>
  /// <see href="https://en.wikipedia.org/wiki/Trie"/>
  /// <seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Bit-slice_trees"/>
  /// <seealso href="https://github.com/gmamaladze/trienet/tree/master/TrieNet"/>
  //public sealed class SimpleTrie<TKey, TValue>
  //  //: ITrie<TKey, TValue>
  //  where TKey : notnull
  //{
  //  private readonly TrieNode m_root = new(false, default!);

  //  /// <summary>
  //  /// <para>Count the number of terminal (complete) <paramref name="key"/>s (sets of <typeparamref name="TKey"/>).</para>
  //  /// </summary>
  //  public int Count() => TrieCount(m_root);

  //  /// <summary>
  //  /// <para>Delete a <paramref name="key"/> (set of <typeparamref name="TKey"/>).</para>
  //  /// </summary>
  //  /// <param name="key"></param>
  //  public bool Delete(System.ReadOnlySpan<TKey> key) => TrieDelete(m_root, key);

  //  /// <summary>
  //  /// <para>Search for an <paramref name="key"/> (set of <typeparamref name="TKey"/>). Returns the <paramref name="value"/> as an out parameter.</para>
  //  /// </summary>
  //  /// <param name="key"></param>
  //  /// <param name="value"></param>
  //  /// <returns>Whether the key/value was found.</returns>
  //  public bool Find(System.ReadOnlySpan<TKey> key, out TValue value) => TrieFind(m_root, key, out value);

  //  /// <summary>
  //  /// <para>Search for an entry that starts-with <paramref name="key"/> (set of <typeparamref name="TKey"/>). Returns the <paramref name="value"/>, found at the partial entry, as an out parameter.</para>
  //  /// </summary>
  //  /// <param name="key"></param>
  //  /// <param name="value"></param>
  //  /// <returns></returns>
  //  public bool FindStartsWith(System.ReadOnlySpan<TKey> key, out TValue value) => TrieStartsWith(m_root, key, out value);

  //  /// <summary>
  //  /// <para>Insert a <paramref name="key"/> (set of <typeparamref name="TKey"/>) with a <paramref name="value"/>.</para>
  //  /// </summary>
  //  /// <param name="key"></param>
  //  /// <param name="value"></param>
  //  public void Insert(System.ReadOnlySpan<TKey> key, TValue value) => TrieInsert(m_root, key, value);

  //  /// <summary>
  //  /// <para>Insert a <paramref name="key"/> (set of <typeparamref name="TKey"/>).</para>
  //  /// </summary>
  //  /// <param name="key"></param>
  //  public void Insert(System.ReadOnlySpan<TKey> key) => TrieInsert(m_root, key, default!);

  //  public string ToConsoleString()
  //  {
  //    var sb = new System.Text.StringBuilder();

  //    AddString(m_root, 0);

  //    return sb.ToString();

  //    void AddString(TrieNode node, int level)
  //    {
  //      foreach (var childNode in node.Children)
  //      {
  //        sb.Append(string.Empty.PadLeft(level == 0 ? 0 : level + 1));
  //        if (level == 0) sb.Append('[', 1);
  //        sb.Append(childNode.Key.ToString());
  //        sb.Append(childNode.Value.ToString());
  //        //if (childNode.Value.IsTerminal) sb.Append("]", 1);
  //        //else sb.Append($"#{childNode.Value.Children.Count}", 1);
  //        //sb.Append($" ({childNode.Value.Value})", 1);
  //        sb.AppendLine();

  //        AddString(childNode.Value, level + 1);
  //      }
  //    }
  //  }

  //  #region Static methods

  //  private static int TrieCount(TrieNode node)
  //  {
  //    var count = 0;

  //    foreach (var kvp in node.Children)
  //    {
  //      if (kvp.Value.IsTerminal)
  //        count++;

  //      count += TrieCount(kvp.Value);
  //    }

  //    return count;
  //  }

  //  private static bool TrieDelete(TrieNode node, System.ReadOnlySpan<TKey> set)
  //  {
  //    if (set.Length == 0) // We have recursed the entire key.
  //    {
  //      if (node.IsTerminal && node.Children.Count != 0) // We have a full terminal key, but there are subkeys consisting of the same key data..
  //        return (node.IsTerminal = false); // ..so we cannot allow the key data to be deleted, instead we mark the terminal key as no longer being terminal.

  //      return node.Children.Count == 0; // If there is no more key data, there are no subkeys, so we allow all key data to be deleted.
  //    }

  //    var keyStone = set[0];

  //    if (!node.Children.TryGetValue(keyStone, out SimpleTrie<TKey, TValue>.TrieNode? keyStoneValue))
  //      return false;

  //    if (TrieDelete(keyStoneValue, set[1..]))
  //      node.Children.Remove(keyStone);

  //    return node.Children.Count == 0;
  //  }

  //  private static bool TrieFind(TrieNode node, System.ReadOnlySpan<TKey> set, out TValue value)
  //  {
  //    for (int index = 0; index < set.Length; index++)
  //    {
  //      if (!node.Children.TryGetValue(set[index], out var temp))
  //        break;

  //      node = temp;

  //      if (node.IsTerminal && index == set.Length - 1)
  //      {
  //        value = node.Value;
  //        return true;
  //      }
  //    }

  //    value = default!;
  //    return false;
  //  }

  //  private static void TrieInsert(TrieNode node, System.ReadOnlySpan<TKey> set, TValue value)
  //  {
  //    for (int index = 0; index < set.Length; index++)
  //    {
  //      if (!node.Children.TryGetValue(set[index], out var temp))
  //      {
  //        temp = new TrieNode(index == set.Length - 1, value);

  //        node.Children.Add(set[index], temp);
  //      }

  //      node = temp;
  //    }
  //  }

  //  private static bool TrieStartsWith(TrieNode node, System.ReadOnlySpan<TKey> set, out TValue value)
  //  {
  //    for (int index = 0; index < set.Length; index++)
  //    {
  //      if (!node.Children.TryGetValue(set[index], out var temp))
  //      {
  //        value = default!;
  //        return false;
  //      }

  //      node = temp;
  //    }

  //    value = node.Value;
  //    return true;
  //  }

  //  #endregion // Static methods

  //  public override string ToString() => $"{GetType().Name} {{ Count = {Count()} }}";

  //  private record class TrieNode
  //  {
  //    public readonly System.Collections.Generic.Dictionary<TKey, TrieNode> Children;
  //    public bool IsTerminal;
  //    public TValue Value;

  //    public TrieNode(bool isTerminal, TValue value)
  //    {
  //      Children = new System.Collections.Generic.Dictionary<TKey, TrieNode>();
  //      IsTerminal = isTerminal;
  //      Value = value;
  //    }

  //    public override string ToString() => $"{(IsTerminal ? "]" : $"#{Children.Count}")} {(Value is null || Value.Equals(default!) ? string.Empty : $"({Value})")}";
  //  }
  //}
}

/*
  var t = new Flux.DataStructures.SimpleTrie<char, string>();

  //foreach (var d in System.IO.Directory.GetDirectories(@"C:\Users\Rob\OneDrive\", "*", System.IO.SearchOption.AllDirectories))
  //  t.Insert(d);

  t.Insert(" ");
  t.Delete(" ");

  t.Insert("abc");
  t.Insert("abgl");
  t.Insert("cdf");
  t.Insert("abcd");
  t.Insert("lmn");

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
