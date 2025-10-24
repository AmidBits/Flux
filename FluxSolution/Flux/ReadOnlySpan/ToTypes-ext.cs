namespace Flux
{
  public static partial class XtensionReadOnlySpan
  {
    public static string ToUrgfString(this System.ReadOnlySpan<string> source)
      => string.Join((char)UnicodeInformationSeparator.UnitSeparator, source);

    extension<T>(System.ReadOnlySpan<T> source)
    {
      /// <summary>
      /// <para>Creates a new <see cref="System.Array"/> with all elements from <paramref name="source"/>, and a <paramref name="preLength"/> and a <paramref name="postLength"/> number of default slots.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="preLength">The number of array slots to add before the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
      /// <param name="postLength">The number of array slots to add after the <paramref name="source"/> elements in the new <see cref="System.Array"/>.</param>
      /// <returns></returns>
      public T[] ToArray(int preLength, int postLength)
      {
        if (preLength < 0) throw new System.ArgumentOutOfRangeException(nameof(preLength));
        if (postLength < 0) throw new System.ArgumentOutOfRangeException(nameof(postLength));

        var target = new T[preLength + source.Length + postLength];
        source.CopyTo(new System.Span<T>(target, preLength, source.Length));
        return target;
      }

      /// <summary>
      /// <para>Creates a CSV (tabular data) string from the <see cref="ReadOnlySpan{T}"/>.</para>
      /// </summary>
      /// <param name="writer">The <see cref="System.IO.TextWriter"/>.</param>
      /// <param name="data">The CSV data.</param>
      /// <param name="delimiter">The delimiter that separates the values.</param>
      /// <param name="alwaysEncloseInDoubleQuotes">Whether to always add double quotes (true) or only add then when needed (false).</param>
      /// <param name="trimWhiteSpace">Indicates whether leading and trailing white space should be trimmed from field values.</param>
      public string ToCsvString(string delimiter = ",", bool trimWhiteSpace = false, bool alwaysEncloseInDoubleQuotes = true)
      {
        var sb = new System.Text.StringBuilder();

        for (var i = 0; i < source.Length; i++)
        {
          if (i > 0) sb.Append(delimiter);

          var s = $"{source[i]}";

          if (trimWhiteSpace)
            s = s.Trim();

          if (!alwaysEncloseInDoubleQuotes || s.Contains(delimiter) || s.Contains(System.Environment.NewLine))
            s = $"\"{s.Replace("\"", "\"\"")}\"";

          sb.Append(s);
        }

        return sb.ToString();
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.HashSet{T}"/> with all elements from <paramref name="source"/> and the specified <paramref name="equalityComparer"/> (or default if null).</para>
      /// </summary>
      public System.Collections.Generic.HashSet<T> ToHashSet(System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null, int additionalCapacity = 0)
      {
        var target = new System.Collections.Generic.HashSet<T>(source.Length + additionalCapacity, equalityComparer ?? System.Collections.Generic.EqualityComparer<T>.Default);
        target.AddSpan(source);
        return target;
      }

      /// <summary>
      /// <para>Creates a new <see cref="System.Collections.Generic.List{T}"/> with all elements from <paramref name="source"/>.</para>
      /// </summary>
      public System.Collections.Generic.List<T> ToList(int additionalCapacity = 0)
      {
        var target = new System.Collections.Generic.List<T>(source.Length + additionalCapacity);
        target.AddRange(source);
        return target;
      }

      /// <summary>
      /// <para>Creates a new <see cref="SpanMaker{T}"/> with all elements from <paramref name="source"/>.</para>
      /// </summary>
      public SpanMaker<T> ToSpanMaker()
        => new(source);

      /// <summary>
      /// <para>Creates an URGF (Unicode tabular data) string from the <paramref name="source"/> data.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      public string ToUrgfString()
      {
        var sb = new System.Text.StringBuilder();

        for (var index = 0; index < source.Length; index++)
        {
          if (index > 0) sb.Append((char)UnicodeInformationSeparator.UnitSeparator);

          sb.Append(source[index]);
        }

        return sb.ToString();
      }
    }
  }
}
