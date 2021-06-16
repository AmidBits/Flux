namespace Flux
{
  public static partial class ExtensionMethods
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
      catch { }

      result = default!;
      return false;
    }
  }
}
