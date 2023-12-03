namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Makes CamelCase of words separated by the specified predicate. The first character</summary>
    public static System.Text.StringBuilder MakeCamelCase(this System.Text.StringBuilder source, System.Func<char, bool> predicate)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      for (var index = 0; index < source.Length; index++)
        if (predicate(source[index]))
        {
          do { source.Remove(index, 1); }
          while (predicate(source[index]));

          if (index < source.Length)
            ToUpper(index);
        }

      return source;

      void ToUpper(int index)
        => source[index] = char.ToUpper(source[index]);
    }
    public static System.Text.StringBuilder MakeCamelCase(this System.Text.StringBuilder source)
      => MakeCamelCase(source, char.IsWhiteSpace);
  }
}
