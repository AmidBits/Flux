namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <span>Extends the content of the <paramref name="source"/> by repeating the content <paramref name="count"/> times.</span>
    /// </summary>
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
