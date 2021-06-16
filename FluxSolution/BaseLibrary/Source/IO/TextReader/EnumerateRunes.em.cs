namespace Flux
{
	public static partial class ExtensionMethods
	{
		public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this System.IO.TextReader source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var buffer = new char[1024];
			var offset = 0;
			var length = source.Read(buffer, 0, buffer.Length);

			while (length > 0)
			{
				System.Text.Rune.DecodeFromUtf16(new System.ReadOnlySpan<char>(buffer, offset, length), out var rune, out var charsConsumed);
				offset += charsConsumed;
				length -= charsConsumed;

				yield return rune;

				if (length <= 1)
				{
					System.Array.Copy(buffer, offset, buffer, 0, length);
					offset = 0;
					length += source.Read(buffer, length, buffer.Length - length);
				}
			}
		}
	}
}

/*
      using (var sr = new System.IO.StreamReader(@"C:\Test\Xml.xml"))
        foreach (var rune in sr.EnumerateRunes())
          System.Console.Write(rune.ToString());
 */
