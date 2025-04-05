namespace Flux
{
  public static partial class Resources
  {
    #region .NET Sequence

    public static System.Collections.Generic.IEnumerable<object[]> GetDotNetSequence(this System.Collections.IEnumerable enumerable)
    {
      using var e = enumerable.Cast<object>().GetEnumerator();

      if (e.MoveNext())
      {
        var pis = e.Current.GetType().GetMemberDictionary(e.Current).Where(kvp => kvp.Key.MemberType == System.Reflection.MemberTypes.Property).Cast<System.Collections.Generic.KeyValuePair<System.Reflection.PropertyInfo, object?>>().Select(kvp => kvp.Key);

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
