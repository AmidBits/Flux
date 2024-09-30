namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Join CamelCase of words separated by the specified predicate. The first character</summary>
    public static System.Text.StringBuilder UnprespaceCapWords(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      for (var index = source.Length - 2; index > 0; index--)
        if (source[index] == ' ')
          source.Remove(index, 1);

      return source;
    }
  }
}
