namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Creates an <see cref="System.Xml.Linq.XNode"/> from an existing <see cref="System.Xml.XmlNode"/>.</summary>
    /// <param name="source">The source <see cref="System.Xml.XmlNode"/>.</param>
    /// <returns>An <see cref="System.Xml.Linq.XNode"/> containing the XML from an <see cref="System.Xml.XmlNode"/>.</returns>
    public static System.Xml.Linq.XNode ToXNode(this System.Xml.XmlNode source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var reader = source.CreateNavigator()?.ReadSubtree() ?? throw new System.NullReferenceException(nameof(source.CreateNavigator));

      return System.Xml.Linq.XNode.ReadFrom(reader);
    }
  }
}
