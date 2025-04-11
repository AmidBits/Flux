namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Xml.XmlNode"/> from an existing <see cref="System.Xml.Linq.XNode"/>.</summary>
    /// <param name="source">The source <see cref="System.Xml.Linq.XNode"/>.</param>
    /// <returns>A new <see cref="System.Xml.XmlNode"/> containing the XML from a <see cref="System.Xml.Linq.XNode"/>.</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.NullReferenceException"></exception>
    public static System.Xml.XmlNode ToXmlNode(this System.Xml.Linq.XNode source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var reader = source.CreateReader();

      return new System.Xml.XmlDocument().ReadNode(reader) ?? throw new System.NullReferenceException(nameof(System.Xml.XmlDocument.ReadNode));
    }
  }
}
