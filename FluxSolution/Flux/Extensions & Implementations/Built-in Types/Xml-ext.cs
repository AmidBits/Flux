namespace Flux
{
  public static partial class XmlExtensions
  {
    extension(System.Xml.Linq.XNode source)
    {
      /// <summary>Creates a new <see cref="System.Xml.XmlNode"/> from an existing <see cref="System.Xml.Linq.XNode"/>.</summary>
      /// <param name="source">The source <see cref="System.Xml.Linq.XNode"/>.</param>
      /// <returns>A new <see cref="System.Xml.XmlNode"/> containing the XML from a <see cref="System.Xml.Linq.XNode"/>.</returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.NullReferenceException"></exception>
      public System.Xml.XmlNode ToXmlNode()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var reader = source.CreateReader();

        return new System.Xml.XmlDocument().ReadNode(reader) ?? throw new System.NullReferenceException(nameof(System.Xml.XmlDocument.ReadNode));
      }
    }

    extension(System.Xml.XmlNode source)
    {
      /// <summary>Creates a new <see cref="System.Xml.Linq.XNode"/> from an existing <see cref="System.Xml.XmlNode"/>.</summary>
      /// <param name="source">The source <see cref="System.Xml.XmlNode"/>.</param>
      /// <returns>A new <see cref="System.Xml.Linq.XNode"/> containing the XML from a <see cref="System.Xml.XmlNode"/>.</returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.NullReferenceException"></exception>
      public System.Xml.Linq.XNode ToXNode()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        using var reader = source.CreateNavigator()?.ReadSubtree() ?? throw new System.NullReferenceException(nameof(source.CreateNavigator));

        return System.Xml.Linq.XNode.ReadFrom(reader);
      }
    }

    #region XML entities

    [System.Text.RegularExpressions.GeneratedRegex(@"[\u0026\u0027\u0022\u003e\u003c]")]
    private static partial System.Text.RegularExpressions.Regex RegexXmlEntityCharacters(); // Characters that needs to be escaped.

    [System.Text.RegularExpressions.GeneratedRegex(@"&(amp|apos|quot|gt|lt);")]
    private static partial System.Text.RegularExpressions.Regex RegexXmlEntities(); // Characters that needs to be escaped.

    [System.Text.RegularExpressions.GeneratedRegex(@"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]")]
    private static partial System.Text.RegularExpressions.Regex RegexXmlInvalidUnescapedCharacters(); // Characters that needs to be escaped.

    [System.Text.RegularExpressions.GeneratedRegex(@"#0x\d{4}#")]
    private static partial System.Text.RegularExpressions.Regex RegexXmlInvalidEscapedCharacters();

    public const char XmlEntityCharAmp = '\u0026'; // &
    public const char XmlEntityCharApos = '\u0027'; // '
    public const char XmlEntityCharQuot = '\u0022'; // "
    public const char XmlEntityCharGt = '\u003e'; // >
    public const char XmlEntityCharLt = '\u003c'; // <

    public const string XmlEntityAmp = @"&amp;";
    public const string XmlEntityApos = @"&apos;";
    public const string XmlEntityQuot = @"&quot;";
    public const string XmlEntityGt = @"&gt;";
    public const string XmlEntityLt = @"&lt;";

    extension(char source)
    {
      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references#Predefined_entities_in_XML"/>
      public bool IsXmlEntityCharacter()
        => source switch
        {
          XmlEntityCharQuot => true,
          XmlEntityCharGt => true,
          XmlEntityCharLt => true,
          XmlEntityCharApos => true,
          XmlEntityCharAmp => true,
          _ => false,
        };
    }

    extension(string source)
    {
      /// <summary>Returns an XML escaped string (basically replacing all entity characters with their respective entity.</summary>
      public string EscapeXml()
        => new System.Text.StringBuilder(source).ReplaceAll(c => true, c => c switch
        {
          XmlEntityCharAmp => XmlEntityAmp,
          XmlEntityCharApos => XmlEntityApos,
          XmlEntityCharQuot => XmlEntityQuot,
          XmlEntityCharGt => XmlEntityGt,
          XmlEntityCharLt => XmlEntityLt,
          _ => string.Empty
        }).ToString();

      /// <summary>Returns an string where all invalid XML characters have been escaped to .</summary>
      public string EscapeInvalidXmlCharacters()
        => RegexXmlInvalidUnescapedCharacters().Replace(source, (m) => $"#0x{((int)m.Value[0]).ToString(@"X4", System.Globalization.CultureInfo.CurrentCulture)}#");

      /// <summary>Returns whether the string contains any XML entities.</summary>
      public bool HasXmlEntities()
        => source is not null
        && (
          source.Contains(XmlEntityAmp, System.StringComparison.Ordinal)
          || source.Contains(XmlEntityApos, System.StringComparison.Ordinal)
          || source.Contains(XmlEntityQuot, System.StringComparison.Ordinal)
          || source.Contains(XmlEntityGt, System.StringComparison.Ordinal)
          || source.Contains(XmlEntityLt, System.StringComparison.Ordinal)
        );

      /// <summary>Returns whether the string contains any XML entity character.</summary>
      public bool HasXmlEntityCharacter()
        => source.Any(c => IsXmlEntityCharacter(c));

      /// <summary></summary>
      public System.Xml.Linq.XElement ToPropertyXmlAsName(string propertyValue, string propertyElementName = @"Property")
        => new(propertyElementName, new System.Xml.Linq.XAttribute(@"Name", source), new System.Xml.Linq.XAttribute(@"Value", propertyValue));
      /// <summary></summary>
      public System.Xml.Linq.XElement ToPropertyXmlAsValue(string propertyName, string propertyElementName = @"Property")
        => new(propertyElementName, new System.Xml.Linq.XAttribute(@"Name", propertyName), new System.Xml.Linq.XAttribute(@"Value", source));

      /// <summary>Returns an unescaped XML string (basically replacing all entities with their respective character.</summary>
      public string UnescapeXml()
        => source?.Replace(XmlEntityLt, XmlEntityCharLt.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityGt, XmlEntityCharGt.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityQuot, XmlEntityCharQuot.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityApos, XmlEntityCharApos.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityAmp, XmlEntityCharAmp.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal) ?? throw new System.ArgumentNullException(nameof(source));

      /// <summary>Returns an string where all escaped invalid characters have been restored to invalid XML characters.</summary>
      public string UnescapeInvalidXmlCharacters()
        => RegexXmlInvalidEscapedCharacters().Replace(source, (m) => $"{(char)int.Parse(m.Value.AsSpan(3, 4), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture)}");
    }

    #endregion
  }
}
