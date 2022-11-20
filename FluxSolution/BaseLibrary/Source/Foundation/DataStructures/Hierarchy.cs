namespace Flux
{
  // https://www.siepman.nl/blog/a-generic-tree-of-nodes-the-easy-way#:~:text=%2F%2F%20With%20just%20one%20line%20of%20code%21%21%20var,one%20tree%2C%20otherwise%20there%20would%20be%20more%20rootnodes
  #region Extension methods.
  public static partial class ExtensionMethods
  {
    public static System.Collections.Generic.IEnumerable<TValue> Values<TKey, TValue>(this System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> source)
      => source.Select(n => n.Value);

    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> Duplicates<TKey, TValue>(this System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> source, System.Func<IHierarchy<TKey, TValue>, TKey> selector)
      => source.GroupBy(selector).Where(i => i.IsMultiple()).SelectMany(i => i);

    public static bool IsMultiple<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      using var enumerator = source.GetEnumerator();

      return enumerator.MoveNext() && enumerator.MoveNext();
    }

    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> ToIEnumarable<TKey, TValue>(this IHierarchy<TKey, TValue> source)
    {
      yield return source;
    }

    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> All<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.Root().SelfAndDescendants();
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> Ancestors<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.IsRoot ? System.Linq.Enumerable.Empty<IHierarchy<TKey, TValue>>() : source.Parent.ToIEnumarable().Concat(source.Parent.Ancestors());
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> Descendants<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.Children.SelectMany(c => c.SelfAndDescendants());
    public static int Level<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.Ancestors().Count();
    private static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> GetNodesAtLevelInternal<TKey, TValue>(this IHierarchy<TKey, TValue> source, int level)
      => source.Level() == level ? source.ToIEnumarable() : source.Children.SelectMany(c => c.GetNodesAtLevelInternal(level));
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> GetNodesAtLevel<TKey, TValue>(this IHierarchy<TKey, TValue> source, int level)
      => source.Root().GetNodesAtLevelInternal(level);
    public static IHierarchy<TKey, TValue> Root<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.SelfAndAncestors().Last();
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SameLevel<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.SelfAndSameLevel().Where(sl => !ReferenceEquals(sl, source));
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SelfAndAncestors<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.ToIEnumarable().Concat(source.Ancestors());
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SelfAndChildren<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.ToIEnumarable().Concat(source.Children);
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SelfAndDescendants<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.ToIEnumarable().Concat(source.Children.SelectMany(c => c.SelfAndDescendants()));
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SelfAndSameLevel<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.GetNodesAtLevel(source.Level());
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> SelfAndSiblings<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.IsRoot ? source.ToIEnumarable() : source.Parent.Children;
    public static System.Collections.Generic.IEnumerable<IHierarchy<TKey, TValue>> Siblings<TKey, TValue>(this IHierarchy<TKey, TValue> source)
      => source.SelfAndSiblings().Where(s => !ReferenceEquals(s, source));
  }
  #endregion Extension methods.

  public interface IHierarchy<TKey, TValue>
  {
    bool IsEmpty { get; }
    bool IsRoot { get; }

    System.Collections.Generic.IReadOnlyList<IHierarchy<TKey, TValue>> Children { get; }
    TKey Key { get; }
    TValue Value { get; }
    IHierarchy<TKey, TValue> Parent { get; }
  }

  public sealed class Hierarchy<TKey, TValue>
    : IHierarchy<TKey, TValue>
    where TKey : struct
    where TValue : struct

    //: System.IEquatable<Hierarchy<T>>//, System.Collections.Generic.IEnumerable<Hierarchy<T>>, System.Collections.Generic.IEnumerable<T>
  {
    public static readonly IHierarchy<TKey, TValue> Empty = new EmptyHierarchy();

    private readonly System.Collections.Generic.List<Hierarchy<TKey, TValue>> m_children;
    private readonly TKey m_key;
    private readonly TValue m_value;
    private readonly IHierarchy<TKey, TValue> m_parent;

    public Hierarchy(TKey key, TValue value, IHierarchy<TKey, TValue> parent)
    {
      if (parent is null) throw new System.ArgumentNullException(nameof(parent));

      m_children = new System.Collections.Generic.List<Hierarchy<TKey, TValue>>();
      m_key = key;
      m_value = value;
      m_parent = parent;
    }
    public Hierarchy(TKey key, TValue value)
      : this(key, value, Empty)
    {
    }

    public bool IsEmpty => false;
    public bool IsRoot => Empty.Equals(m_parent);
    public System.Collections.Generic.IReadOnlyList<IHierarchy<TKey, TValue>> Children => m_children;
    public TKey Key => m_key;
    public TValue Value => m_value;
    public IHierarchy<TKey, TValue> Parent => m_parent;

    public IHierarchy<TKey, TValue> this[int index]
    => m_children[index];

    //public void Add(IHierarchy<TKey, TValue> childNode)
    //{
    //  if (!childNode.IsRoot) throw new System.ArgumentException("The child node with value [{0}] can not be added because it is not a root node.");
    //  if (Root == childNode) throw new System.ArgumentException("The child node with value [{0}] is the rootnode of the parent.");
    //  if (childNode.SelfAndDescendants.Any(n => this == n)) throw new System.ArgumentException("The childnode with value [{0}] can not be added to itself or its descendants.");

    //  childNode.Parent = this;
    //  m_children.Add(childNode);
    //}

    //public void Insert(int index, IHierarchy<T> childNode)
    //{
    //  if (!childNode.IsRoot) throw new System.ArgumentException("The child node with value [{0}] can not be added because it is not a root node.");
    //  if (Root == childNode) throw new System.ArgumentException("The child node with value [{0}] is the rootnode of the parent.");
    //  if (childNode.SelfAndDescendants.Any(n => this == n)) throw new System.ArgumentException("The childnode with value [{0}] can not be added to itself or its descendants.");

    //  childNode.Parent = this;
    //  m_children.Insert(index, childNode);
    //}

    //public Hierarchy<T> Add(T value)
    //{
    //  var childNode = new Hierarchy<T>(value, this);
    //  m_children.Add(childNode);
    //  return childNode;
    //}

    //public Hierarchy<T> Insert(T value, int index)
    //{
    //  if (index < 0 || index >= m_children.Count) throw new System.ArgumentOutOfRangeException(nameof(index));

    //  var childNode = new Hierarchy<T>(value, this);
    //  m_children.Insert(index, childNode);
    //  return childNode;
    //}

    //public void Add(Hierarchy<T> childNode, int index = -1)
    //{
    //  if (index < -1)
    //    throw new ArgumentException("The index can not be lower then -1");
    //  if (index > Children.Count() - 1)
    //    throw new ArgumentException("The index ({0}) can not be higher then index of the last iten. Use the AddChild() method without an index to add at the end");
    //  if (!childNode.IsRoot)
    //    throw new ArgumentException("The child node with value [{0}] can not be added because it is not a root node.");
    //  if (Root == childNode)
    //    throw new ArgumentException("The child node with value [{0}] is the rootnode of the parent.");
    //  if (childNode.SelfAndDescendants.Any(n => this == n))
    //    throw new ArgumentException("The childnode with value [{0}] can not be added to itself or its descendants.");

    //  childNode.Parent = this;

    //  if (index == -1)
    //  {
    //    m_children.Add(childNode);
    //  }
    //  else
    //  {
    //    m_children.Insert(index, childNode);
    //  }
    //}

    //public void AddFirstChild(Hierarchy<T> childNode)
    //  => Add(childNode, 0);
    //public Hierarchy<T> AddFirstChild(T value)
    //{
    //  var childNode = new Hierarchy<T>(value);
    //  AddFirstChild(childNode);
    //  return childNode;
    //}

    //public void AddFirstSibling(Hierarchy<T> childNode)
    //  => Parent.AddFirstChild(childNode);
    //public Hierarchy<T> AddFirstSibling(T value)
    //{
    //  var siblingNode = new Hierarchy<T>(value);
    //  AddFirstSibling(siblingNode);
    //  return siblingNode;
    //}

    //public Hierarchy<T> AddLastSibling(T value)
    //{
    //  var childNode = new Hierarchy<T>(value);
    //  AddLastSibling(childNode);
    //  return childNode;
    //}

    //public void AddLastSibling(Hierarchy<T> childNode)
    //{
    //  Parent.Add(childNode);
    //}

    //public Hierarchy<T> AddParent(T value)
    //{
    //  var newNode = new Hierarchy<T>(value);
    //  AddParent(newNode);
    //  return newNode;
    //}

    //public void AddParent(Hierarchy<T> parentNode)
    //{
    //  if (!IsRoot)
    //  {
    //    throw new ArgumentException("This node [{0}] already has a parent", "parentNode");
    //  }
    //  parentNode.Add(this);
    //}

    //public void Disconnect()
    //{
    //  if (IsRoot) throw new System.InvalidOperationException("The root node [{0}] can not get disconnected from a parent.");

    //  Parent.m_children.Remove(this);
    //  Parent = null;
    //}

    ////System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
    ////  => m_children.Values().GetEnumerator();

    ////System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    ////  => m_children.GetEnumerator();

    ////public System.Collections.Generic.IEnumerator<Hierarchy<T>> GetEnumerator()
    ////  => m_children.GetEnumerator();

    //public override string ToString()
    //  => $"{Value}";

    //public static IEnumerable<IHierarchy<TKey,TValue>> CreateTree<TId>(IEnumerable<IHierarchy<TKey, TValue>> values, Func<IHierarchy<TKey, TValue>, TId> idSelector, Func<IHierarchy<TKey, TValue>, TId?> parentIdSelector)
    //  where TId : struct
    //{
    //  var valuesCache = values.ToList();
    //  if (!valuesCache.Any())
    //    return System.Linq.Enumerable.Empty<IHierarchy<TKey, TValue>>();
    //  IHierarchy<TKey, TValue> itemWithIdAndParentIdIsTheSame = valuesCache.FirstOrDefault(v => IsSameId(idSelector(v), parentIdSelector(v)));
    //  if (itemWithIdAndParentIdIsTheSame != null) // Hier verwacht je ook een null terug te kunnen komen
    //  {
    //    throw new ArgumentException("At least one value has the samen Id and parentId [{0}]");
    //  }

    //  var nodes = valuesCache.Select(v => new Hierarchy<TKey, TValue>(v));
    //  return CreateTree(nodes, idSelector, parentIdSelector);

    //}

    //public static IEnumerable<IHierarchy<TKey, TValue>> CreateTree<TId>(IEnumerable<Hierarchy<TKey,TValue>> rootNodes, Func<IHierarchy<TKey, TValue>, TId> idSelector, Func<IHierarchy<TKey, TValue>, TId?> parentIdSelector)
    //  where TId : struct
    //{
    //  var rootNodesCache = rootNodes.ToList();
    //  var duplicates = rootNodesCache.Duplicates(n => n).ToList();
    //  if (duplicates.Any())
    //  {
    //    throw new ArgumentException("One or more values contains {0} duplicate keys. The first duplicate is: [{1}]");
    //  }

    //  foreach (var rootNode in rootNodesCache)
    //  {
    //    var parentId = parentIdSelector(rootNode.Value);
    //    var parent = rootNodesCache.FirstOrDefault(n => IsSameId(idSelector(n.Value), parentId));

    //    if (parent != null)
    //    {
    //      parent.Add(rootNode);
    //    }
    //    else if (parentId != null)
    //    {
    //      throw new ArgumentException("A value has the parent ID [{0}] but no other nodes has this ID");
    //    }
    //  }
    //  var result = rootNodesCache.Where(n => n.IsRoot);
    //  return result;
    //}

    //private static bool IsSameId<TKey, TValue>(IHierarchy<TKey, TValue> id, IHierarchy<TKey, TValue>? parentId)
    //  //where TId : struct
    //{
    //  return parentId != null && id.Equals(parentId.Value);
    //}

    #region Overloaded operators
    public static bool operator ==(Hierarchy<TKey, TValue> value1, Hierarchy<TKey, TValue> value2)
      => ReferenceEquals(value1, value2);
    public static bool operator !=(Hierarchy<TKey, TValue> value1, Hierarchy<TKey, TValue> value2)
      => !ReferenceEquals(value1, value2);
    #endregion Overloaded operators

    #region Implemented interfaces
    public bool Equals(IHierarchy<TKey, TValue>? other)
      => Equals(this, other);
    #endregion Implemented interfaces

    public override bool Equals(object? obj)
      => obj is IHierarchy<TKey, TValue> o && Equals(o);
    public override int GetHashCode()
    {
      var hc = new System.HashCode();
      for (var index = 0; index < m_children.Count; index++)
        hc.Add(m_children[index]);
      hc.Add(m_key);
      hc.Add(m_value);
      hc.Add(m_parent);
      return hc.ToHashCode();
    }

    private sealed class EmptyHierarchy
      : IHierarchy<TKey, TValue>
    {
      public bool IsEmpty
        => true;
      public bool IsRoot
        => throw new System.Exception(nameof(EmptyHierarchy));
      public System.Collections.Generic.IReadOnlyList<IHierarchy<TKey, TValue>> Children
        => throw new System.Exception(nameof(EmptyHierarchy));
      public TKey Key
        => throw new System.Exception(nameof(EmptyHierarchy));
      public TValue Value
        => throw new System.Exception(nameof(EmptyHierarchy));
      public IHierarchy<TKey, TValue> Parent
        => throw new System.Exception(nameof(EmptyHierarchy));
    }
  }
}
