using System.Linq;

namespace Flux.Resources.Ucd
{
	public static class Blocks
	{
		public static System.Uri UriLocal
			=> new System.Uri(@"file://\Resources\Ucd\Blocks.txt");
		public static System.Uri UriSource
			=> new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

		/// <summary>The Unicode block database.</summary>
		/// <see cref="https://www.unicode.org/"/>
		/// <seealso cref="https://unicode.org/Public/"/>
		/// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
		// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
		public static System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
		{
			yield return new string[] { "StartCode", "EndCode", "BlockName" };

			if (uri is null) throw new System.ArgumentNullException(nameof(uri));

			var m_reSplit = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

			foreach (var stringArray in uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplit.Split(line)))
				yield return stringArray;
		}

		public static System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
		{
			using var e = GetStrings(uri).GetEnumerator();

			if (e.MoveNext())
			{
				yield return e.Current;

				while (e.MoveNext())
				{
					var objects = new object[e.Current.Length];

					for (var index = objects.Length - 1; index >= 0; index--)
					{
						objects[index] = index switch
						{
							0 or 1 => int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var value) ? value : System.DBNull.Value,
							_ => e.Current[index],
						};
					}

					yield return objects;
				}
			}
		}
	}
}
