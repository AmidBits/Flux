//namespace Flux.Collections.Immutable
//{
//  public class ImmutableList<T>
//    : System.Collections.Generic.IEnumerable<T>
//  {
//    public T Head { get; set; }
//    public ImmutableList<T> Tail { get; set; }

//    protected ImmutableList()
//    {
//    }

//    public static ImmutableList<T> WithItems(System.Collections.Generic.IEnumerable<T> items)
//    {
//      if (items.Count() == 1)
//      {
//        return new ImmutableList<T>()
//        {
//          Head = items.Single(),
//          Tail = new EmptyList<T>()
//        };
//      }

//      return new ImmutableList<T>()
//      {
//        Head = items.First(),
//        Tail = WithItems(items.Skip(1))
//      };
//    }

//    public ImmutableList<T> Add(T item)
//    {
//      if (this is EmptyList<T>)
//      {
//        return new ImmutableList<T>() { Head = item, Tail = new EmptyList<T>() };
//      }

//      return new ImmutableList<T>()
//      {
//        Head = Head,
//        Tail = Tail.Add(item)
//      };
//    }

//    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
//    {
//      if (this is EmptyList<T>) yield break;
//      else
//      {
//        yield return Head;
//        foreach (var elem in Tail)
//          yield return elem;
//      }
//    }
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
//  }

//  public sealed class EmptyList<T> : ImmutableList<T>
//  {

//  }

//  public static class ImmutableListExtensions
//  {
//    public static ImmutableList<T> ToImmutableList<T>(this System.Collections.Generic.IEnumerable<T> sequence)
//    {
//      return ImmutableList<T>.WithItems(sequence);
//    }
//  }
//}

//using System.Collections;
//using System.Collections.Generic;

//namespace Flux.Collections.Immutable
//{
//  public interface IList<T>
//    : System.Collections.Generic.IEnumerable<T>
//  {
//    bool Contains(T value);
//    bool IsEmpty { get; }
//    IList<T> Add(T value);
//    IList<T> Remove(T value);
//    //T Value { get; }
//  }

//  public sealed class List<T>
//    : IList<T>
//    where T : System.IComparable<T>
//  {
//    public static readonly IList<T> Empty = new EmptyList<T>();

//    internal readonly T m_value;
//    internal readonly IList<T> m_tail;

//    public T Value => m_value;

//    protected List(T value, IList<T> tail)
//    {
//      m_value = value;
//      m_tail = tail;
//    }

//    public IList<T> Add(T value) => IsEmpty ? new List<T>(value, Empty) : new List<T>(m_value, m_tail.Add(value));
//    public bool Contains(T value) => IsEmpty ? false : (m_value.Equals(value) ? true : m_tail.Contains(value));
//    public bool IsEmpty => false;
//    public IList<T> Remove(T value) => IsEmpty ? this : (value.Equals(m_value) ? m_tail : m_tail.Remove(value));
//    //public T Value => m_value;

//    public System.Collections.Generic.IEnumerator<T> GetEnumerator()
//    {
//      if (IsEmpty) yield break;

//      var node = (IList<T>)this;

//      while (!node.m_tail.IsEmpty)
//      {
//        yield return node.m_value;

//        node = node.m_tail;
//      }
//    }
//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

//    private sealed class EmptyList<T>
//      : IList<T>
//      where T : System.IComparable<T>
//    {
//      public IList<T> Add(T value) => new List<T>(value, this);
//      public bool Contains(T value) => throw new System.NotImplementedException();
//      public bool IsEmpty => true;
//      public IList<T> Remove(T value) => throw new System.NotImplementedException();

//      public System.Collections.Generic.IEnumerator<T> GetEnumerator() { yield break; }
//      System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
//    }
//  }
//}
