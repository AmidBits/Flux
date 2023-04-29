using System.ComponentModel.Design;
using System.Linq;

namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<TSource, int>> Mode<TSource>(this System.Collections.Generic.IEnumerable<TSource> source, out TSource mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<TSource, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }

#else

    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<double, int>> Mode(this System.Collections.Generic.IEnumerable<double> source, out double mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<double, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }

    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, int>> Mode(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source, out System.Numerics.BigInteger mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<System.Numerics.BigInteger, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }

    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<int, int>> Mode(this System.Collections.Generic.IEnumerable<int> source, out int mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<int, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }

    /// <summary>Creates a new sequence consisting of 'moded' (i.e. sorted by the most frequent or common) elements in <paramref name="source"/>.</summary>
    /// <see cref="http://en.wikipedia.org/wiki/Mode"/>
    public static System.Linq.IOrderedEnumerable<System.Collections.Generic.KeyValuePair<long, int>> Mode(this System.Collections.Generic.IEnumerable<long> source, out long mode, out int count)
    {
      var sequence = source.GroupBy(t => t).Select(g => new System.Collections.Generic.KeyValuePair<long, int>(g.Key, g.Count())).OrderByDescending(kvp => kvp.Value);

      (mode, count) = sequence.First();

      return sequence;
    }

#endif
  }
}
