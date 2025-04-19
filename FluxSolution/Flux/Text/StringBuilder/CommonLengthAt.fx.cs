namespace Flux
{
  public static partial class StringBuilders
  {
    /// <summary>
    /// <para>Yields the number of characters that the source and the target have in common from the specified respective indices. Uses the specified comparer.</para>
    /// </summary>
    public static int CommonLengthAt(this System.Text.StringBuilder source, int sourceStartIndex, System.ReadOnlySpan<char> target, int targetStartIndex, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (sourceStartIndex < 0 || sourceStartIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
      if (targetStartIndex < 0 || targetStartIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<char>.Default;

      var minLength = int.Min(source.Length - sourceStartIndex, target.Length - targetStartIndex);

      var length = 0;
      while (length < minLength && equalityComparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
        length++;
      return length;
    }
  }
}
