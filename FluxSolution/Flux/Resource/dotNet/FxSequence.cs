namespace Flux
{
  public static partial class Resource
  {
    #region .NET Sequence

    public static System.Collections.Generic.IEnumerable<object[]> GetDotNetSequence(this System.Collections.IEnumerable enumerable)
    {
      using var e = enumerable.Cast<object>().GetEnumerator();

      if (e.MoveNext())
      {
        var pis = e.Current.GetPropertyInfos();

        yield return pis.Select(pi => pi.Name).ToArray(); // Return column names once.

        do
        {
          yield return pis.Select((pi, i) => pi.GetValue(e.Current)!).ToArray();
        }
        while (e.MoveNext());
      }
    }

    #endregion // .NET Sequence
  }
}
