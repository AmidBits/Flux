namespace Flux
{
  public static partial class Reflection
  {
    /// <summary>Creates an <see cref="System.Xml.XmlNode"/> from an existing <see cref="System.Xml.Linq.XNode"/>.</summary>
    /// <param name="source">The source <see cref="System.Xml.Linq.XNode"/>.</param>
    /// <returns>An <see cref="System.Xml.XmlNode"/> containing the XML from an <see cref="System.Xml.Linq.XNode"/>.</returns>
    public static System.Xml.XmlNode ToXmlNode(this System.Xml.Linq.XNode source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var reader = source.CreateReader();

      return new System.Xml.XmlDocument().ReadNode(reader) ?? throw new System.NullReferenceException(@"ReadNode");
    }
  }
}
