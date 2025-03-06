namespace Flux
{
  public static partial class UriEncoding
  {
    /// <summary>
    /// <para>The longest UTF-8 encoded byte sequence.</para>
    /// </summary>
    private const int Utf8MaxEncodedSequenceLength = 4;

    /// <summary>
    /// <para>The URI percent-encoded (%xx) octet length.</para>
    /// </summary>
    private const int UriPercentEncodedOctetLength = 3;

    [System.Text.RegularExpressions.GeneratedRegexAttribute(@"(?<Prefix>%)(?<Octet>[0-9A-Fa-f]{2})", System.Text.RegularExpressions.RegexOptions.Compiled)]
    public static partial System.Text.RegularExpressions.Regex RegexUriEncodedOctetMatch();

    /// <summary>
    /// <para>URIs include components and subcomponents that are delimited by characters in the "reserved" set. These characters are called "reserved" because they may(or may not) be defined as delimiters by the generic syntax, by each scheme-specific syntax, or by the implementation-specific syntax of a URI's dereferencing algorithm.</para>
    /// <para>If data for a URI component would conflict with a reserved character's purpose as a delimiter, then the conflicting data must be percent-encoded before the URI is formed.</para>
    /// <para>reserved = <c>gen-delims</c> and <c>sub-delims</c></para>
    /// <para>gen-delims = "<c>:/?#[]@</c>"</para>
    /// <para>sub-delims = "<c>!$&amp;&apos;()*+,;=</c>"</para>
    /// <para>The purpose of reserved characters is to provide a set of delimiting characters that are distinguishable from other data within a URI.</para>
    /// <para>URIs that differ in the replacement of a reserved character with its corresponding percent-encoded octet are not equivalent. Percent-encoding a reserved character, or decoding a percent-encoded octet that corresponds to a reserved character, will change how the URI is interpreted by most applications.Thus, characters in the reserved set are protected from normalization and are therefore safe to be used by scheme-specific and producer-specific algorithms for delimiting data subcomponents within a URI.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUriReserved(this char source)
      => ":/?#[]@!$&`()*+,;=".Contains(source);

    /// <summary>
    /// <para>URIs include components and subcomponents that are delimited by characters in the "reserved" set. These characters are called "reserved" because they may(or may not) be defined as delimiters by the generic syntax, by each scheme-specific syntax, or by the implementation-specific syntax of a URI's dereferencing algorithm.</para>
    /// <para>If data for a URI component would conflict with a reserved character's purpose as a delimiter, then the conflicting data must be percent-encoded before the URI is formed.</para>
    /// <para>reserved = <c>gen-delims</c> and <c>sub-delims</c></para>
    /// <para>gen-delims = "<c>:/?#[]@</c>"</para>
    /// <para>sub-delims = "<c>!$&amp;&apos;()*+,;=</c>"</para>
    /// <para>The purpose of reserved characters is to provide a set of delimiting characters that are distinguishable from other data within a URI.</para>
    /// <para>URIs that differ in the replacement of a reserved character with its corresponding percent-encoded octet are not equivalent. Percent-encoding a reserved character, or decoding a percent-encoded octet that corresponds to a reserved character, will change how the URI is interpreted by most applications.Thus, characters in the reserved set are protected from normalization and are therefore safe to be used by scheme-specific and producer-specific algorithms for delimiting data subcomponents within a URI.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUriReserved(this System.Text.Rune source)
      => source.IsAscii && IsUriReserved((char)source.Value);

    /// <summary>
    /// <para>Returns whether the character sequence is a valid URI scheme component.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUriScheme(this System.ReadOnlySpan<char> source)
    {
      if (!char.IsAsciiLetter(source[0]))
        return false;

      for (var i = source.Length - 1; i > 0; i--)
        if (source[i] is var c && !(char.IsAsciiLetterOrDigit(c) && @"+-.".Contains(c)))
          return false;

      return true;
    }

    /// <summary>
    /// <para>Characters that are allowed in a URI but do not have a reserved purpose are called unreserved. These include uppercase and lowercase letters, decimal digits, hyphen, period, underscore, and tilde.</para>
    /// <para>unreserved = <c>ALPHA</c>, <c>DIGIT</c> or "<c>-._~</c>"</para>
    /// <para>URIs that differ in the replacement of an unreserved character with its corresponding percent-encoded US-ASCII octet are equivalent: they identify the same resource. However, URI comparison implementations not always perform normalization prior to comparison (see Section 6).</para>
    /// <para>For consistency, percent-encoded octets in the ranges of ALPHA (%41-%5A and %61-%7A), DIGIT (%30-%39), hyphen (%2D), period (%2E), underscore (%5F), or tilde (%7E) should not be created by URI producers and, when found in a URI, should be decoded to their corresponding unreserved characters by URI normalizers.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUriUnreserved(this char source)
      => char.IsAsciiLetterOrDigit(source) || @"-._~".Contains(source);

    /// <summary>
    /// <para>Characters that are allowed in a URI but do not have a reserved purpose are called unreserved. These include uppercase and lowercase letters, decimal digits, hyphen, period, underscore, and tilde.</para>
    /// <para>unreserved = <c>ALPHA</c>, <c>DIGIT</c> or "<c>-._~</c>"</para>
    /// <para>URIs that differ in the replacement of an unreserved character with its corresponding percent-encoded US-ASCII octet are equivalent: they identify the same resource. However, URI comparison implementations not always perform normalization prior to comparison (see Section 6).</para>
    /// <para>For consistency, percent-encoded octets in the ranges of ALPHA (%41-%5A and %61-%7A), DIGIT (%30-%39), hyphen (%2D), period (%2E), underscore (%5F), or tilde (%7E) should not be created by URI producers and, when found in a URI, should be decoded to their corresponding unreserved characters by URI normalizers.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsUriUnreserved(this System.Text.Rune source)
      => source.IsAscii && IsUriUnreserved((char)source.Value);

    /// <summary>
    /// <para>Decodes all percent-encoded entities to characters of a URI component.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementSelector"></param>
    /// <returns></returns>
    public static SpanMaker<char> UriDecode(this System.ReadOnlySpan<char> source)
    {
      var sm = new SpanMaker<char>();

      var evm = RegexUriEncodedOctetMatch().EnumerateMatches(source);

      var listBytes = new System.Collections.Generic.List<byte>();

      var lastEnd = 0;

      foreach (var vm in evm)
      {
        if (lastEnd != vm.Index)
        {
          if (listBytes.Count > 0)
            ParseBytes(ref sm);

          sm.Append(source.Slice(lastEnd, vm.Index - lastEnd));
        }

        var b = int.Parse(source.Slice(vm.Index + 1, vm.Length - 1), System.Globalization.NumberStyles.HexNumber);

        listBytes.Add((byte)b);

        lastEnd = vm.Index + vm.Length;
      }

      if (listBytes.Count > 0)
        ParseBytes(ref sm);

      if (lastEnd < source.Length)
        sm.Append(source.Slice(lastEnd));

      return sm;

      void ParseBytes(ref SpanMaker<char> sm)
      {
        var spanBytes = listBytes.AsSpan();

        for (var index = 0; index < spanBytes.Length;)
        {
          System.Text.Rune.DecodeFromUtf8(spanBytes.Slice(index), out var rune, out var bytesConsumed);

          sm.Append(rune.ToString());

          index += bytesConsumed;
        }

        listBytes.Clear();
      }
    }

    /// <summary>
    /// <para></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    private static string UriEncode(byte source)
      => source is > 0 and <= 127 ? $"%{source:X2}" : throw new System.ArgumentOutOfRangeException();

    /// <summary>
    /// <para>Return the source URI encoded or percent-encoded using the specified <paramref name="byteBuffer"/> and <paramref name="charBuffer"/>. Use this version if encoding multiple runes.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="byteBuffer"></param>
    /// <param name="charBuffer"></param>
    /// <returns></returns>
    public static string UriEncode(this System.Text.Rune source, System.Span<byte> byteBuffer, System.Span<char> charBuffer)
    {
      if (source.Value is var utf32 && utf32 <= 0x7F)
        return UriEncode((byte)utf32);

      var encodedLength = source.EncodeToUtf8(byteBuffer);

      for (var byteIndex = 0; byteIndex < encodedLength; byteIndex++)
        UriEncode((byte)byteBuffer[byteIndex]).CopyTo(charBuffer.Slice(byteIndex * UriPercentEncodedOctetLength, UriPercentEncodedOctetLength));

      return charBuffer[..(encodedLength * UriPercentEncodedOctetLength)].ToString();
    }

    /// <summary>
    /// <para>Return the source URI encoded or percent-encoded.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string UriEncode(this System.Text.Rune source)
    {
      if (source.Value is var utf32 && utf32 <= 0x7F)
        return UriEncode((byte)utf32);

      var byteBuffer = System.Buffers.ArrayPool<byte>.Shared.Rent(Utf8MaxEncodedSequenceLength);
      var charBuffer = System.Buffers.ArrayPool<char>.Shared.Rent(Utf8MaxEncodedSequenceLength * UriPercentEncodedOctetLength);

      return source.UriEncode(byteBuffer, charBuffer);
    }

    /// <summary>
    /// <para>Return the source URI encoded or percent-encoded.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string UriEncode(this char source)
      => ((System.Text.Rune)source).UriEncode();

    /// <summary>
    /// <para>Encodes characters to percent-encoded entities of a URI component, unless a character is unreserved.</para>
    /// <para><see href="https://www.rfc-editor.org/rfc/rfc3986"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="replacementSelector"></param>
    /// <param name="skipUriUnreserved"></param>
    /// <returns></returns>
    public static SpanMaker<char> UriEncode(this System.ReadOnlySpan<char> source)
    {
      var sm = new SpanMaker<char>();

      var byteBuffer = System.Buffers.ArrayPool<byte>.Shared.Rent(Utf8MaxEncodedSequenceLength);
      var charBuffer = System.Buffers.ArrayPool<char>.Shared.Rent(Utf8MaxEncodedSequenceLength * UriPercentEncodedOctetLength);

      foreach (var rune in source.EnumerateRunes())
        sm = sm.Append(rune.IsUriUnreserved() ? rune.ToString() : rune.UriEncode(byteBuffer, charBuffer));

      return sm;
    }
  }
}
