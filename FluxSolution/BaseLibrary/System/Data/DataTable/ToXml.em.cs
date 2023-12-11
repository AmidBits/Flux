namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new XDocument with the data from the DataTable.</summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var document = new System.Xml.Linq.XDocument();
      using (var writer = document.CreateWriter())
        source.WriteXml(writer);
      return document;
    }

    /// <summary>Creates a new XmlDocument with the data from the DataTable.</summary>
    public static System.Xml.XmlDocument ToXmlDocument(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var document = new System.Xml.XmlDocument();
      using (var writer = document.CreateNavigator()?.AppendChild())
        source.WriteXml(writer);
      return document;
    }
  }
}
