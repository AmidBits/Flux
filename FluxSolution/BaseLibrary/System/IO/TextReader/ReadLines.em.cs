namespace Flux
{
  public static partial class Reflection
  {
    public static System.Collections.Generic.IEnumerable<TResult> ReadLines<TResult>(this System.IO.TextReader source, System.Func<string, bool> predicate, System.Func<string, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(predicate);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      while (source.ReadLine() is var line && line is not null)
        if (predicate(line))
          yield return resultSelector(line);
    }

    //public static System.Collections.Generic.IEnumerable<string> ReadLines(this System.IO.TextReader source, bool keepEmptyLines)
    //  => source.ReadLines(s => s.Length > 0 || keepEmptyLines, s => s);
  }
}
