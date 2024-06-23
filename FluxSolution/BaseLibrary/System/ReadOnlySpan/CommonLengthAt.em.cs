namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Yields the number of values that the <paramref name="source"/> and the <paramref name="target"/> have in common from the specified <paramref name="sourceStartIndex"/> and <paramref name="targetStartIndex"/> respectively. Uses the specified <paramref name="equalityComparer"/>, or default if null.</para>
    /// </summary>
    public static int CommonLengthAt<T>(this System.ReadOnlySpan<T> source, int sourceStartIndex, System.ReadOnlySpan<T> target, int targetStartIndex, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      if (sourceStartIndex < 0 || sourceStartIndex >= source.Length) throw new System.ArgumentOutOfRangeException(nameof(sourceStartIndex));
      if (targetStartIndex < 0 || targetStartIndex >= target.Length) throw new System.ArgumentOutOfRangeException(nameof(targetStartIndex));

      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var minLength = System.Math.Min(source.Length - sourceStartIndex, target.Length - targetStartIndex);

      var count = 0;
      while (count < minLength && equalityComparer.Equals(source[sourceStartIndex++], target[targetStartIndex++]))
        count++;
      return count;
    }
  }
}
