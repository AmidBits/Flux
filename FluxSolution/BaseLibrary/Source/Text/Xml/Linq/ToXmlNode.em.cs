namespace Flux
{
	public static partial class SystemXmlEm
	{
		/// <summary>Convert an XElement into an.XmlElement.</summary>
		/// <param name="source">The XElement to convert.</param>
		/// <returns>An XmlElement containing the XML from the XElement.</returns>
		public static System.Xml.XmlNode ToXmlNode(this System.Xml.Linq.XNode source)
			=> new System.Xml.XmlDocument().ReadNode((source ?? throw new System.ArgumentNullException(nameof(source))).CreateReader()) ?? throw new System.NullReferenceException(@"ReadNode");
	}
}
