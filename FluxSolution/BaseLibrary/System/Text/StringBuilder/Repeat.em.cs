namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Duplicates this string builder a specified number of times.</summary>
    public static System.Text.StringBuilder Repeat(this System.Text.StringBuilder source, int count)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (source.Length == 0) throw new System.ArgumentOutOfRangeException(nameof(source));

      var original = source.ToString();

      while (count-- > 0)
        source.Append(original);

      return source;
    }
  }
}
