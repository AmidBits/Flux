namespace Flux
{
	public static partial class TextReaderEm
	{
		//public static System.Collections.Generic.IEnumerable<string> EnumerateTextElements2(this System.IO.TextReader source)
		//{
		//	if (source is null) throw new System.ArgumentNullException(nameof(source));

		//	var buffer = new char[12];
		//	var offset = 0;
		//	var length = source.Read(buffer, 0, buffer.Length);

		//	var stringInfo = new System.Globalization.StringInfo();

		//	while (length > 0)
		//	{
		//		stringInfo.String = new string(buffer, offset, length);

		//		var textElement = stringInfo.SubstringByTextElements(0, 1);
		//		offset += textElement.Length;
		//		length -= textElement.Length;

		//		yield return textElement;

		//		if (length <= 3)
		//		{
		//			System.Array.Copy(buffer, offset, buffer, 0, length);
		//			offset = 0;
		//			length += source.Read(buffer, length, buffer.Length - length);
		//		}
		//	}
		//}

		public static System.Collections.Generic.IEnumerable<string> GetTextElements(this System.IO.TextReader source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var buffer = new char[4];
			var length = 0;

			var stringInfo = new System.Globalization.StringInfo();

			while (source.Read() is var read && read != -1)
			{
				buffer[length++] = (char)read;

				if (length == buffer.Length)
				{
					yield return getGrapheme();
				}
			}

			while (length > 0)
			{
				yield return getGrapheme();
			}

			string getGrapheme()
			{
				stringInfo.String = new string(buffer, 0, length);

				var grapheme = stringInfo.SubstringByTextElements(0, 1);

				length -= grapheme.Length;

				System.Array.Copy(buffer, grapheme.Length, buffer, 0, length);

				//for(int sourceIndex = grapheme.Length, targetIndex = 0, count = 0; count < length; count++)
				//{
				//  buffer[targetIndex] = buffer[sourceIndex];
				//}

				return grapheme;
			}
		}
	}

	//  private class StreamTextElementIterator
	//    : System.Collections.Generic.IEnumerator<System.Text.Rune>
	//  {
	//    private System.Text.Rune m_current;
	//    private readonly System.IO.StreamReader m_source;

	//    private char[] charArray;
	//    private int charIndex;
	//    private int charCount;

	//    public StreamTextElementIterator(System.IO.StreamReader source, int bufferSize = 8192)
	//    {
	//      m_source = source;
	//      m_current = default!;

	//      charArray = new char[bufferSize];
	//      charIndex = 0;
	//      charCount = 0;
	//    }

	//    public System.Text.Rune Current
	//      => m_current;
	//    object System.Collections.IEnumerator.Current
	//      => m_current!;

	//    public void Dispose()
	//      => m_source.Dispose();

	//    public bool MoveNext()
	//    {
	//      var difference = charCount - charIndex;

	//      if (difference <= 4)
	//      {
	//        if (difference > 0)
	//          System.Array.Copy(charArray, charIndex, charArray, 0, difference);

	//        charIndex = 0;
	//        charCount = difference;
	//      }

	//      if (charIndex == 0 && charCount < charArray.Length)
	//      {
	//        charCount += m_source.Read(charArray, charCount, charArray.Length - charCount);
	//      }

	//      if (System.Text.Rune.DecodeFromUtf16(charArray.AsSpan(charIndex, charCount - charIndex), out var rune, out var count) is var or && or == System.Buffers.OperationStatus.Done)
	//      {
	//        charIndex += count;

	//        m_current = rune;

	//        return true;
	//      }

	//      return false;
	//    }

	//    public void Reset()
	//      => throw new System.InvalidOperationException();
	//  }

	//  public static System.Collections.Generic.IEnumerable<string> GetTextElements(this System.IO.Stream stream, System.Text.Encoding encoding)
	//  {
	//    using var sr = new System.IO.StreamReader(stream, encoding ?? System.Text.Encoding.UTF8);

	//    foreach (var textElement in sr.GetTextElements())
	//    {
	//      yield return textElement;
	//    }
	//  }
	//}
}
