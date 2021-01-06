namespace Flux
{
  public static partial class SystemTextEm
  {
    /// <summary>Reverse all characters in-place.</summary>
    public static System.Text.StringBuilder Reverse(this System.Text.StringBuilder source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (int indexL = 0, indexR = source.Length - 1; indexL < indexR; indexL++, indexR--)
      {
        var tmp = source[indexL];
        source[indexL] = source[indexR];
        source[indexR] = tmp;
      }

      return source;
    }
  }
}
