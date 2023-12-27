namespace Flux
{
  public static partial class Reflection
  {
    public static System.Collections.Generic.IEnumerable<TResult> GetFields<TResult>(this System.Data.IDataRecord source, System.Func<System.Data.IDataRecord, int, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      for (var index = 0; index < source.FieldCount; index++)
        yield return resultSelector(source, index);
    }
  }
}
