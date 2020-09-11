namespace Flux
{
  public static class HashCode
  {
    /// <summary>Combines hash codes FOR multiple objects. Useful for combining hash codes of composite types.</summary>
    public static int Combine(System.Collections.Generic.IEnumerable<object> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      unchecked
      {
        var hashCode = 0;
        foreach (var o in source )
          hashCode = ((hashCode << 5) + hashCode) ^ o.GetHashCode();
        return hashCode;
      }
    }
    /// <summary>Combines hash codes FOR multiple objects. Useful for combining hash codes of composite types.</summary>
    internal static int Combine(params object[] objects)
      => Combine(objects);

    public static int CombineBoost(System.Collections.Generic.IEnumerable<object> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      unchecked
      {
        var hashCode = 0L;
        foreach (var o in source )
          hashCode ^= o.GetHashCode() + 0x9e3779b9 + (hashCode << 6) + (hashCode >> 2);
        return (int)hashCode;
      }
    }
    /// <summary>Combines hash codes FOR multiple objects. Useful for combining hash codes of composite types.</summary>
    internal static int CombineBoost(params object[] objects)
      => CombineBoost(objects);

    public static int CombineCore(System.Collections.Generic.IEnumerable<object> source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var hash = new System.HashCode();
      foreach (var o in source)
        hash.Add(o);
      return hash.ToHashCode();
    }
    /// <summary>Combines hash codes FOR multiple objects. Useful for combining hash codes of composite types.</summary>
    internal static int CombineCore(params object[] objects)
      => CombineCore(objects);
  }
}
