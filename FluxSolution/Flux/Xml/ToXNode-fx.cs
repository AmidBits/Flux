namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates a new <see cref="System.Xml.Linq.XNode"/> from an existing <see cref="System.Xml.XmlNode"/>.</summary>
    /// <param name="source">The source <see cref="System.Xml.XmlNode"/>.</param>
    /// <returns>A new <see cref="System.Xml.Linq.XNode"/> containing the XML from a <see cref="System.Xml.XmlNode"/>.</returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.NullReferenceException"></exception>
    public static System.Xml.Linq.XNode ToXNode(this System.Xml.XmlNode source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var reader = source.CreateNavigator()?.ReadSubtree() ?? throw new System.NullReferenceException(nameof(source.CreateNavigator));

      return System.Xml.Linq.XNode.ReadFrom(reader);
    }
  }
}
