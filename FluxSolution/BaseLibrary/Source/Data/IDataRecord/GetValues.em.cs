namespace Flux
{
  public static partial class XtensionsData
  {
    /// <summary>Results in an object array of all column values in the current row.</summary>
    public static object[] GetValues(this System.Data.IDataRecord source)
    {
      var values = new object[source.FieldCount];
      source.GetValues(values);
      return values;
    }
  }
}
