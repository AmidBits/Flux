namespace Flux
{
	public static partial class TextReaderEm
	{
		public static System.Collections.Generic.IEnumerable<System.Text.Rune> EnumerateRunes(this System.IO.TextReader source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var buffer = new char[128];
			var offset = 0;
			var length = 0;

			while (true)
			{
				if (length <= 1)
				{
					System.Array.Copy(buffer, offset, buffer, 0, length);
					offset = 0;

					var actual = source.Read(buffer, length, buffer.Length - length);
					length += actual;
				}

				if (length >= 1)
				{
					System.Text.Rune.DecodeFromUtf16(buffer, out var rune, out var charsConsumed);
					offset += charsConsumed;
					length -= charsConsumed;

					yield return rune;
				}
				else break;
			}
		}
	}
}
