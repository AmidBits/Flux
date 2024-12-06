namespace Flux
{
  public static partial class Fx
  {
    public static TResult[] GetFields<TResult>(this System.Data.IDataRecord source, System.Func<System.Data.IDataRecord, int, TResult> resultSelector)
    {
      System.ArgumentNullException.ThrowIfNull(source);
      System.ArgumentNullException.ThrowIfNull(resultSelector);

      var fields = new TResult[source.FieldCount];

      for (var index = 0; index < source.FieldCount; index++)
        fields[index] = resultSelector(source, index);

      return fields;
    }
  }
}
