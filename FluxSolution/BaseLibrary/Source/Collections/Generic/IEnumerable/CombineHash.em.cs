using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Combines the hash codes for the elements in the sequence. This is the 'boost' version from C++.</summary>
    public static int CombineHashBoost<T>(this System.Collections.Generic.IEnumerable<T> source, int seed = 0)
      => ThrowOnNull(source).Aggregate(seed, (hash, e) => unchecked(hash ^ ((e?.GetHashCode() ?? 0) + (int)0x9e3779b9 + (hash << 6) + (hash >> 2))), (hash) => hash);

    /// <summary>Combines the hash codes for the elements in the sequence, using the .NET core hash combiner.</summary>
    public static int CombineHashCore<T>(this System.Collections.Generic.IEnumerable<T> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var hash = new System.HashCode();
      foreach (var item in source)
        hash.Add(item);
      return hash.ToHashCode();
    }

    /// <summary>Combines the hash codes for the elements in the sequence, using a custom simplicity by Special Sauce (see link).</summary>
    /// <see cref="https://stackoverflow.com/a/34006336/3178666"/>
    public static int CombineHashCustom<T>(this System.Collections.Generic.IEnumerable<T> source, int seed = 1009, int factor = 9176)
      => ThrowOnNull(source).Aggregate(seed, (hash, e) => unchecked(hash * factor + (e?.GetHashCode() ?? 0)), (hash) => hash);
  }
}
