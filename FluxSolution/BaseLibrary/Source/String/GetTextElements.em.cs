namespace Flux
{
  public static partial class XtensionsString
  {
    /// <summary>Enumerates graphemes, i.e. text elements, with their respective index in the original string, and creates a sequence based on the specified result selector. The process starts at the optionally specified index, i.e. it skips the specified amount of characters.</summary>
    public static System.Collections.Generic.IEnumerable<TResult> GetTextElements<TResult>(this string source, System.Func<string, int, TResult> resultSelector, int startAt = 0)
    {
      var tee = System.Globalization.StringInfo.GetTextElementEnumerator(source ?? throw new System.ArgumentNullException(nameof(source)), startAt);

      while (tee.MoveNext())
      {
        yield return resultSelector(tee.GetTextElement(), tee.ElementIndex) ?? throw new System.ArgumentNullException(nameof(resultSelector));
      }
    }
  }
}
