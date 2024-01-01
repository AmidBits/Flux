namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Creates a new <see cref="System.Xml.Linq.XDocument"/> with the data from the <see cref="System.Data.DataTable"/>.</para>
    /// </summary>
    public static System.Xml.Linq.XDocument ToXDocument(this System.Data.DataTable source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var document = new System.Xml.Linq.XDocument();
      using (var writer = document.CreateWriter())
        source.WriteXml(writer);
      return document;
    }

    /// <summary>
    /// <para>Creates a new <see cref="System.Xml.XmlDocument"/> with the data from the <see cref="System.Data.DataTable"/>.</para>
    /// </summary>
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
