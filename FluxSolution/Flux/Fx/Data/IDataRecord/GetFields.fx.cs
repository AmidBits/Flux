namespace Flux
{
  public static partial class Fx
  {
    public static System.Type[] GetFieldTypes(this System.Data.IDataRecord source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var names = new System.Type[source.FieldCount];

      for (var index = names.Length - 1; index >= 0; index--)
        names[index] = source.GetFieldType(index);

      return names;
    }

    //    public static TResult[] GetFields<TResult>(this System.Data.IDataRecord source, System.Func<System.Data.IDataRecord, int, TResult> resultSelector)
    //    {
    //      System.ArgumentNullException.ThrowIfNull(source);
    //      System.ArgumentNullException.ThrowIfNull(resultSelector);

    //      var fields = new TResult[source.FieldCount];

    //      for (var index = 0; index < source.FieldCount; index++)
    //        fields[index] = resultSelector(source, index);

    //      return fields;
    //    }
  }
}
