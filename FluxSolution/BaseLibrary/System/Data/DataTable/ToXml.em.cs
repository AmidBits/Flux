namespace Flux
{
  public static partial class ExtensionMethodsDataTable
  {
    /// <summary>Creates a new XDocument with the data from the DataTable.</summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.Linq.XDocument();
      using (var xw = xd.CreateWriter())
        source.WriteXml(xw);
      return xd;
    }

    /// <summary>Creates a new XmlDocument with the data from the DataTable.</summary>
    public static System.Xml.XmlDocument ToXmlDocument(this System.Data.DataTable source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var xd = new System.Xml.XmlDocument();
      using (var writer = xd.CreateNavigator()?.AppendChild())
        source.WriteXml(writer);
      return xd;
    }
  }
}