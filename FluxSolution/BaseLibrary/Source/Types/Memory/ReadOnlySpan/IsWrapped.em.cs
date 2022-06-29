namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Indicates whether the source is already wrapped in the specified characters. E.g. in SQL brackets, or parenthesis.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, T left, T right)
      where T : notnull
      => source.Length >= 2 && left.Equals(source[0]) && right.Equals(source[^1]);
    /// <summary>Indicates whether the source is already wrapped in the strings.</summary>
    public static bool IsWrapped<T>(this System.ReadOnlySpan<T> source, System.ReadOnlySpan<T> left, System.ReadOnlySpan<T> right)
      => source.Length >= (left.Length + right.Length) && source.StartsWithEx(left) && source.EndsWithEx(right);
  }
}
