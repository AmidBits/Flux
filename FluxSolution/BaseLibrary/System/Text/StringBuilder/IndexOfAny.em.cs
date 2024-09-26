namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.ReadOnlySpan<char> targets, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
        if (source[sourceIndex] is var sourceChar)
          for (var targetsIndex = 0; targetsIndex < targets.Length; targetsIndex++) // Favor targets in order.
            if (equalityComparer.Equals(sourceChar, targets[targetsIndex]))
              return sourceIndex;

      return -1;
    }

    /// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.ReadOnlySpan<string> targets, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      for (var sourceIndex = 0; sourceIndex < source.Length; sourceIndex++)
        for (var targetsIndex = 0; targetsIndex < targets.Length; targetsIndex++) // Favor targets in order.
          if (EqualsAt(source, sourceIndex, targets[targetsIndex], equalityComparer))
            return sourceIndex;

      return -1;
    }
  }
}
