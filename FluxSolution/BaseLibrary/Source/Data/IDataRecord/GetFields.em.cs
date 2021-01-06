namespace Flux
{
  public static partial class SystemDataEm
  {
    public static System.Collections.Generic.IEnumerable<TResult> GetFields<TResult>(this System.Data.IDataRecord source, System.Func<System.Data.IDataRecord, int, TResult> resultSelector)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (resultSelector is null) throw new System.ArgumentNullException(nameof(resultSelector));

      for (var index = 0; index < source.FieldCount; index++)
        yield return resultSelector(source, index);
    }
  }
}
