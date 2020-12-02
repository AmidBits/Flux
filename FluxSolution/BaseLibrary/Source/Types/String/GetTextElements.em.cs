using System.Linq;

namespace Flux
{
  public static partial class StringEm
  {
    /// <summary>Enumerates graphemes, i.e. text elements, with their respective index in the original string, and creates a sequence based on the specified result selector. The process starts at the optionally specified index, i.e. it skips the specified amount of characters.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> GetTextElements<TResult>(this string source, System.Func<string, int, TResult> resultSelector, int startAt = 0)
    {
      using var sr = new System.IO.StringReader(source);
      using var trtee = new Text.TextReaderTextElementEnumerator(sr);

      return trtee.Where((e, i) => i >= startAt).Select((e, i) => (resultSelector ?? throw new System.ArgumentNullException(nameof(resultSelector))).Invoke(e, i));
    }
  }
}
