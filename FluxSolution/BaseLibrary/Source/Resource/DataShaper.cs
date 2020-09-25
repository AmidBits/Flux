namespace Flux.Resources
{
  /// <summary></summary>
  public abstract class DataShaper
  {
    public abstract System.Collections.Generic.IList<string> FieldNames { get; }
    public abstract System.Collections.Generic.IList<System.Type> FieldTypes { get; }

    public abstract System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri);

    public virtual object ConvertStringToObject(int index, string value)
      => value;

    public virtual System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
    {
      foreach (var sl in GetStrings(uri))
      {
        var ol = new object[sl.Length];
        for (var index = 0; index < sl.Length; index++)
          ol[index] = ConvertStringToObject(index, sl[index]);
        yield return ol;
      }
    }

    public Flux.Data.EnumerableDataReader<object[]> GetDataReader(System.Uri uri)
      => new Flux.Data.EnumerableDataReader<object[]>(GetObjects(uri), dr => dr, FieldNames, FieldTypes);

    public System.Data.DataTable GetDataTable(System.Uri uri)
    {
      var dt = new System.Data.DataTable(GetType().Name);
      for (var index = 0; index < FieldNames.Count; index++)
        dt.Columns.Add(FieldNames[index], FieldTypes[index]);
      foreach (var values in GetObjects(uri))
        dt.Rows.Add(values);
      return dt;
    }
  }
}
