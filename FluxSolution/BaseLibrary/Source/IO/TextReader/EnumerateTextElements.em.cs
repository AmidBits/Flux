namespace Flux
{
	public static partial class SystemTextEm
	{
		public static System.Collections.Generic.IEnumerable<string> EnumerateTextElements(this System.IO.TextReader source)
		{
			if (source is null) throw new System.ArgumentNullException(nameof(source));

			var buffer = new char[128];
			var offset = 0;
			var length = source.Read(buffer, 0, buffer.Length);

			var stringInfo = new System.Globalization.StringInfo();

			while (length > 0)
			{
				stringInfo.String = new string(buffer, offset, length);

				for (int index = 0, count = stringInfo.LengthInTextElements; index < count; index++)
				{
					var textElement = stringInfo.SubstringByTextElements(index, 1);
					offset += textElement.Length;
					length -= textElement.Length;

					yield return textElement;
				}

				if (length > 0)
					System.Array.Copy(buffer, offset, buffer, 0, length);

				offset = 0;
				length += source.Read(buffer, length, buffer.Length - length);
			}

			//while (length > 0)
			//{
			//	stringInfo.String = new string(buffer, offset, length);

			//	var textElement = stringInfo.SubstringByTextElements(0, 1);
			//	offset += textElement.Length;
			//	length -= textElement.Length;

			//	yield return textElement;

			//	if (length <= 3)
			//	{
			//		System.Array.Copy(buffer, offset, buffer, 0, length);
			//		offset = 0;
			//		length += source.Read(buffer, length, buffer.Length - length);
			//	}
			//}
		}
	}
}
