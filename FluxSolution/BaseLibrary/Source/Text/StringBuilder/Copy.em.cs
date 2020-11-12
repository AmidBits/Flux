namespace Flux
{
  public static partial class Xtensions
  {
    public static System.Text.StringBuilder Copy(this System.Text.StringBuilder source, int fromStartIndex, int count, int toStartIndex)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (fromStartIndex < 0 || fromStartIndex > source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(fromStartIndex));
      if (toStartIndex < 0 || toStartIndex > source.Length - 1) throw new System.ArgumentOutOfRangeException(nameof(fromStartIndex));
      if (count < 0 || fromStartIndex + count > source.Length || toStartIndex + count > source.Length) throw new System.ArgumentOutOfRangeException(nameof(count));

      for (; count > 0; count--)
        source[toStartIndex++] = source[fromStartIndex++];

      return source;
    }
  }
}
