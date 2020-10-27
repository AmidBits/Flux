namespace Flux
{
  /// <summary></summary>
  public static partial class Convert
  {
    public static System.Collections.Generic.IEnumerable<object[]> ToTypedObjects<T>(this System.Collections.Generic.IEnumerable<T[]> source, System.Func<T, int, object> typeConverter)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (typeConverter is null) throw new System.ArgumentNullException(nameof(typeConverter));

      var lineCount = 0;

      foreach (var sl in source)
      {
        lineCount++;

        var ol = new object[sl.Length];
        for (var index = 0; index < sl.Length; index++)
          ol[index] = typeConverter(sl[index], index);
        yield return ol;
      }
    }
  }
}
