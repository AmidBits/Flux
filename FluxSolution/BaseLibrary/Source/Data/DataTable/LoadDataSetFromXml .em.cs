namespace Flux
{
  public static partial class XtensionsData
  {
    public static System.Data.DataSet LoadDataSetFromXml(System.Xml.XmlReader xmlReader)
    {
      var ds = new System.Data.DataSet();

      ds.ReadXml(xmlReader);

      return ds;
    }
    public static bool TryLoadDataSetFromXml(System.Xml.XmlReader xmlReader, out System.Data.DataSet result)
    {
      try
      {
        result = LoadDataSetFromXml(xmlReader);
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types
      catch { }
#pragma warning restore CA1031 // Do not catch general exception types

      result = default!;
      return false;
    }
  }
}
