namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Returns the last indices of any specified characters found within the string builder. Uses the specified comparer.</summary>
    public static System.Collections.Generic.IDictionary<char, int> LastIndicesOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(targets);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var indices = new System.Collections.Generic.Dictionary<char, int>();

      var targetsCount = targets.Count;

      for (var sourceIndex = source.Length - 1; sourceIndex >= 0; sourceIndex--)
        if (source[sourceIndex] is var sourceChar && !indices.ContainsKey(sourceChar))
          for (var targetIndex = 0; targetIndex < targetsCount; targetIndex++)
            if (equalityComparer.Equals(sourceChar, targets[targetIndex]))
              indices.Add(sourceChar, sourceIndex);

      return indices;
    }
  }
}
