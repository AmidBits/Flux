using System.Linq;

namespace Flux
{
  public static partial class ExtensionMethodsStringBuilder
  {
    /// <summary>Returns the first index of any of the specified characters within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<char> targets, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (targets is null) throw new System.ArgumentNullException(nameof(targets));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;
      var targetsCount = targets.Count;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        if (source[sourceIndex] is var sourceChar)
          for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
            if (equalityComparer.Equals(sourceChar, targets[targetsIndex]))
              return sourceIndex;

      return -1;
    }

    /// <summary>Reports the first index of any of the specified strings within the string builder, or -1 if none were found. Uses the specified comparer.</summary>
    public static int IndexOfAny(this System.Text.StringBuilder source, System.Collections.Generic.IList<string> targets, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (targets is null) throw new System.ArgumentNullException(nameof(targets));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var sourceLength = source.Length;
      var targetsCount = targets.Count;

      for (var sourceIndex = 0; sourceIndex < sourceLength; sourceIndex++)
        for (var targetsIndex = 0; targetsIndex < targetsCount; targetsIndex++) // Favor targets in list order.
          if (EqualsAt(source, sourceIndex, targets[targetsIndex], equalityComparer))
            return sourceIndex;

      return -1;
    }
  }
}
