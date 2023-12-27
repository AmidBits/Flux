namespace Flux
{
  public static partial class Reflection
  {
    public static System.Text.StringBuilder ReplaceIfEqualAt(this System.Text.StringBuilder source, int startAt, System.ReadOnlySpan<char> key, System.ReadOnlySpan<char> value, System.Collections.Generic.IEqualityComparer<char>? equalityComparer = null)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (EqualsAt(source, startAt, key, equalityComparer ?? System.Collections.Generic.EqualityComparer<char>.Default))
      {
        source.Remove(startAt, key.Length);
        source.Insert(startAt, value);
      }

      return source;
    }
  }
}
