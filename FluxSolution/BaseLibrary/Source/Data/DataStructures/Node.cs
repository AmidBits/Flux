//namespace Flux
//{
//  #region Extension methods.
//  public static partial class ExtensionMethods
//  {
//    public static System.Collections.Generic.IEnumerable<T> Values<T>(this System.Collections.Generic.IEnumerable<Node<T>> nodes)
//      => nodes.Select(n => n.Value);

//    public static System.Collections.Generic.IEnumerable<TSource> Duplicates<TSource, TKey>(this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource, TKey> selector)
//      => source.GroupBy(selector).Where(i => i.IsMultiple()).SelectMany(i => i);

//    public static bool IsMultiple<T>(this System.Collections.Generic.IEnumerable<T> source)
//    {
//      using var enumerator = source.GetEnumerator();

//      return enumerator.MoveNext() && enumerator.MoveNext();
//    }

//    public static System.Collections.Generic.IEnumerable<T> ToIEnumarable<T>(this T item)
//    {
//      yield return item;
//    }
//  }
//  #endregion Extension methods.

//  public class Node<T>
//    : System.Collections.Generic.IEqualityComparer<Node<T>>, System.Collections.Generic.IEnumerable<Node<T>>, System.Collections.Generic.IEnumerable<T>
//  {
//    private readonly List<Node<T>> m_children = new List<Node<T>>();

//    public Node<T> Parent { get; private set; }

//    public T Value { get; set; }

//    public Node(T value)
//    {
//      Value = value;
//    }

//    public Node<T> this[int index]
//      => m_children[index];

//    public Node<T> Add(T value, int index = -1)
//    {
//      var childNode = new Node<T>(value);
//      Add(childNode, index);
//      return childNode;
//    }

//    public void Add(Node<T> childNode, int index = -1)
//    {
//      if (index < -1)
//        throw new ArgumentException("The index can not be lower then -1");
//      if (index > Children.Count() - 1)
//        throw new ArgumentException("The index ({0}) can not be higher then index of the last iten. Use the AddChild() method without an index to add at the end");
//      if (!childNode.IsRoot)
//        throw new ArgumentException("The child node with value [{0}] can not be added because it is not a root node.");
//      if (Root == childNode)
//        throw new ArgumentException("The child node with value [{0}] is the rootnode of the parent.");
//      if (childNode.SelfAndDescendants.Any(n => this == n))
//        throw new ArgumentException("The childnode with value [{0}] can not be added to itself or its descendants.");

//      childNode.Parent = this;

//      if (index == -1)
//      {
//        m_children.Add(childNode);
//      }
//      else
//      {
//        m_children.Insert(index, childNode);
//      }
//    }

//    public void AddFirstChild(Node<T> childNode)
//      => Add(childNode, 0);
//    public Node<T> AddFirstChild(T value)
//    {
//      var childNode = new Node<T>(value);
//      AddFirstChild(childNode);
//      return childNode;
//    }

//    public void AddFirstSibling(Node<T> childNode)
//      => Parent.AddFirstChild(childNode);
//    public Node<T> AddFirstSibling(T value)
//    {
//      var siblingNode = new Node<T>(value);
//      AddFirstSibling(siblingNode);
//      return siblingNode;
//    }

//    public Node<T> AddLastSibling(T value)
//    {
//      var childNode = new Node<T>(value);
//      AddLastSibling(childNode);
//      return childNode;
//    }

//    public void AddLastSibling(Node<T> childNode)
//    {
//      Parent.Add(childNode);
//    }

//    public Node<T> AddParent(T value)
//    {
//      var newNode = new Node<T>(value);
//      AddParent(newNode);
//      return newNode;
//    }

//    public void AddParent(Node<T> parentNode)
//    {
//      if (!IsRoot)
//      {
//        throw new ArgumentException("This node [{0}] already has a parent", "parentNode");
//      }
//      parentNode.Add(this);
//    }

//    public System.Collections.Generic.IEnumerable<Node<T>> Ancestors
//      => IsRoot ? System.Linq.Enumerable.Empty<Node<T>>() : Parent.ToIEnumarable().Concat(Parent.Ancestors);

//    public System.Collections.Generic.IEnumerable<Node<T>> Descendants
//      => SelfAndDescendants.Skip(1);

//    public System.Collections.Generic.IEnumerable<Node<T>> Children
//      => m_children;

//    public System.Collections.Generic.IEnumerable<Node<T>> Siblings
//      => SelfAndSiblings.Where(Other);

//    private bool Other(Node<T> node)
//      => !ReferenceEquals(node, this);

//    public System.Collections.Generic.IEnumerable<Node<T>> SelfAndChildren
//      => this.ToIEnumarable().Concat(Children);

//    public System.Collections.Generic.IEnumerable<Node<T>> SelfAndAncestors
//      => this.ToIEnumarable().Concat(Ancestors);

//    public System.Collections.Generic.IEnumerable<Node<T>> SelfAndDescendants
//      => this.ToIEnumarable().Concat(Children.SelectMany(c => c.SelfAndDescendants));

//    public System.Collections.Generic.IEnumerable<Node<T>> SelfAndSiblings
//      => IsRoot ? this.ToIEnumarable() : Parent.Children;

//    public System.Collections.Generic.IEnumerable<Node<T>> All
//      => Root.SelfAndDescendants;

//    public System.Collections.Generic.IEnumerable<Node<T>> SameLevel
//      => SelfAndSameLevel.Where(Other);

//    public int Level
//      => Ancestors.Count();

//    public System.Collections.Generic.IEnumerable<Node<T>> SelfAndSameLevel
//      => GetNodesAtLevel(Level);

//    private System.Collections.Generic.IEnumerable<Node<T>> GetNodesAtLevelInternal(int level)
//      => level == Level ? this.ToIEnumarable() : Children.SelectMany(c => c.GetNodesAtLevelInternal(level));

//    public System.Collections.Generic.IEnumerable<Node<T>> GetNodesAtLevel(int level)
//      => Root.GetNodesAtLevelInternal(level);

//    public Node<T> Root
//      => SelfAndAncestors.Last();

//    public void Disconnect()
//    {
//      if (IsRoot) throw new System.InvalidOperationException("The root node [{0}] can not get disconnected from a parent.");

//      Parent.m_children.Remove(this);
//      Parent = null;
//    }

//    public bool IsRoot
//    {
//      get { return Parent == null; }
//    }

//    System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
//      => m_children.Values().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => m_children.GetEnumerator();

//    public System.Collections.Generic.IEnumerator<Node<T>> GetEnumerator()
//      => m_children.GetEnumerator();

//    public override string ToString()
//      => $"{Value}";

//    public static IEnumerable<Node<T>> CreateTree<TId>(IEnumerable<T> values, Func<T, TId> idSelector, Func<T, TId?> parentIdSelector)
//        where TId : struct
//    {
//      var valuesCache = values.ToList();
//      if (!valuesCache.Any())
//        return System.Linq.Enumerable.Empty<Node<T>>();
//      T itemWithIdAndParentIdIsTheSame = valuesCache.FirstOrDefault(v => IsSameId(idSelector(v), parentIdSelector(v)));
//      if (itemWithIdAndParentIdIsTheSame != null) // Hier verwacht je ook een null terug te kunnen komen
//      {
//        throw new ArgumentException("At least one value has the samen Id and parentId [{0}]");
//      }

//      var nodes = valuesCache.Select(v => new Node<T>(v));
//      return CreateTree(nodes, idSelector, parentIdSelector);

//    }

//    public static IEnumerable<Node<T>> CreateTree<TId>(IEnumerable<Node<T>> rootNodes, Func<T, TId> idSelector, Func<T, TId?> parentIdSelector)
//        where TId : struct

//    {
//      var rootNodesCache = rootNodes.ToList();
//      var duplicates = rootNodesCache.Duplicates(n => n).ToList();
//      if (duplicates.Any())
//      {
//        throw new ArgumentException("One or more values contains {0} duplicate keys. The first duplicate is: [{1}]");
//      }

//      foreach (var rootNode in rootNodesCache)
//      {
//        var parentId = parentIdSelector(rootNode.Value);
//        var parent = rootNodesCache.FirstOrDefault(n => IsSameId(idSelector(n.Value), parentId));

//        if (parent != null)
//        {
//          parent.Add(rootNode);
//        }
//        else if (parentId != null)
//        {
//          throw new ArgumentException("A value has the parent ID [{0}] but no other nodes has this ID");
//        }
//      }
//      var result = rootNodesCache.Where(n => n.IsRoot);
//      return result;
//    }


//    private static bool IsSameId<TId>(TId id, TId? parentId)
//        where TId : struct
//    {
//      return parentId != null && id.Equals(parentId.Value);
//    }

//    #region Equals en ==

//    public static bool operator ==(Node<T> value1, Node<T> value2)
//    {
//      if ((object)(value1) == null && (object)value2 == null)
//      {
//        return true;
//      }
//      return ReferenceEquals(value1, value2);
//    }

//    public static bool operator !=(Node<T> value1, Node<T> value2)
//    {
//      return !(value1 == value2);
//    }

//    public bool Equals(Node<T>? value1, Node<T>? value2)
//      => ReferenceEquals(value1, value2);
//    public bool Equals(Node<T>? value)
//      => Equals(this, value);

//    public int GetHashCode(Node<T> value)
//    {
//      return base.GetHashCode();
//    }
//    #endregion

//    public override bool Equals(object? obj)
//      => obj is Node<T> o && Equals(o);
//    public override int GetHashCode()
//    {
//      return GetHashCode(this);
//    }
//  }
//}
