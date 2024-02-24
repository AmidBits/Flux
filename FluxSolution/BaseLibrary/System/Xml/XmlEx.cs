namespace Flux.Text
{
  public static partial class XmlEx
  {
    public static readonly System.Collections.Generic.Dictionary<char, string> XmlEntityMap = new()
    {
      { '\u0026' /* & */, "&amp;" },
      { '\u0027' /* ' */, "&apos;" },
      { '\u0022' /* " */, "&quot;" },
      { '\u003e' /* > */, "&gt;" },
      { '\u003c' /* < */, "&lt;" },
    };

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

    /// <summary>Returns an XML escaped string (basically replacing all entity characters with their respective entity.</summary>
    public static string Escape(string source)
      => new System.Text.StringBuilder(source).ReplaceAll(c => c switch
      {
        XmlEntityCharAmp => XmlEntityAmp,
        XmlEntityCharApos => XmlEntityApos,
        XmlEntityCharQuot => XmlEntityQuot,
        XmlEntityCharGt => XmlEntityGt,
        XmlEntityCharLt => XmlEntityLt,
        _ => string.Empty
      }).ToString();
    /// <summary>Returns an string where all invalid XML characters have been escaped to .</summary>
    public static string EscapeInvalidCharacters(string source)
      => System.Text.RegularExpressions.Regex.Replace(source, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", (m) => $"#0x{((int)m.Value[0]).ToString(@"X4", System.Globalization.CultureInfo.CurrentCulture)}#");

    /// <summary>Returns whether the string contains any XML entities.</summary>
    public static bool HasXmlEntities(string source)
      => source is not null && (source.Contains(XmlEntityAmp, System.StringComparison.Ordinal) || source.Contains(XmlEntityApos, System.StringComparison.Ordinal) || source.Contains(XmlEntityQuot, System.StringComparison.Ordinal) || source.Contains(XmlEntityGt, System.StringComparison.Ordinal) || source.Contains(XmlEntityLt, System.StringComparison.Ordinal));

    /// <summary>Returns whether the string contains any XML entity character.</summary>
    public static bool HasXmlEntityCharacter(string source)
      => source.Any(c => IsXmlEntityCharacter(c));

    /// <summary></summary>
    /// <see href="https://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references#Predefined_entities_in_XML"/>
    public static bool IsXmlEntityCharacter(char source)
      => source switch
      {
        XmlEntityCharQuot => true,
        XmlEntityCharGt => true,
        XmlEntityCharLt => true,
        XmlEntityCharApos => true,
        XmlEntityCharAmp => true,
        _ => false,
      };

    /// <summary></summary>
    public static System.Xml.Linq.XElement ToPropertyXmlAsName(string source, string propertyValue, string propertyElementName = @"Property")
      => new(propertyElementName, new System.Xml.Linq.XAttribute(@"Name", source), new System.Xml.Linq.XAttribute(@"Value", propertyValue));
    /// <summary></summary>
    public static System.Xml.Linq.XElement ToPropertyXmlAsValue(string source, string propertyName, string propertyElementName = @"Property")
      => new(propertyElementName, new System.Xml.Linq.XAttribute(@"Name", propertyName), new System.Xml.Linq.XAttribute(@"Value", source));

    /// <summary>Returns an unescaped XML string (basically replacing all entities with their respective character.</summary>
    public static string Unescape(string source)
      => source?.Replace(XmlEntityLt, XmlEntityCharLt.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityGt, XmlEntityCharGt.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityQuot, XmlEntityCharQuot.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityApos, XmlEntityCharApos.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal).Replace(XmlEntityAmp, XmlEntityCharAmp.ToString(System.Globalization.CultureInfo.CurrentCulture), System.StringComparison.Ordinal) ?? throw new System.ArgumentNullException(nameof(source));
    /// <summary>Returns an string where all escaped invalid characters have been restored to invalid XML characters.</summary>
    public static string UnescapeInvalidCharacters(string source)
      => System.Text.RegularExpressions.Regex.Replace(source, @"#0x\d{4}#", (m) => $"{(char)int.Parse(m.Value.AsSpan(3, 4), System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture)}");
  }
}
