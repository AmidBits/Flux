namespace Flux
{
  public static partial class XtendText
  {
    /// <summary>Convert an XmlElement into an XElement.</summary>
    /// <param name="source">The XmlElement to convert.</param>
    /// <returns>An XElement containing the XML from the XmlElement.</returns>
    public static System.Xml.Linq.XNode ToXNode(this System.Xml.XmlNode source)
      => (System.Xml.Linq.XNode)System.Xml.Linq.XNode.ReadFrom((source ?? throw new System.ArgumentNullException(nameof(source))).CreateNavigator().ReadSubtree());
  }
}
