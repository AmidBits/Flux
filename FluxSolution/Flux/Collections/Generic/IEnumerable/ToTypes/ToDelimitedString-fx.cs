namespace Flux
{
  public static partial class Fx
  {
    public static string ToDelimitedString<T>(this System.Collections.Generic.IEnumerable<T> source, System.Func<T, string> itemStringSelector, string separator = " ")
    {
      System.ArgumentNullException.ThrowIfNull(itemStringSelector);

      return string.Join(separator, source.Select(b => itemStringSelector(b)));
    }
  }
}
