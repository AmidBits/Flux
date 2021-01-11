using System.Linq;

namespace Flux.Resources.Ucd
{
	/// <summary>The Unicode block database.</summary>
	/// <see cref="https://www.unicode.org/"/>
	/// <seealso cref="https://unicode.org/Public/"/>
	/// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
	// Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
	public class Blocks
		: ResourceFactory
	{
		public static System.Uri LocalUri
			=> new System.Uri(@"file://\Resources\Ucd\Blocks.txt");
		public static System.Uri SourceUri
			=> new System.Uri(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

		public override System.Collections.Generic.IList<string> FieldNames
			=> new string[] { "StartCode", "EndCode", "BlockName" };
		public override System.Collections.Generic.IList<System.Type> FieldTypes
			=> new System.Type[] { typeof(int), typeof(int), typeof(string) };

		private static System.Text.RegularExpressions.Regex m_reSplitter = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

		public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
			=> uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplitter.Split(line));

		public override System.Collections.Generic.IEnumerable<object[]> GetObjects(System.Uri uri)
			=> GetStrings(uri).ToTypedObjects((value, index) => index switch
			{
				0 => int.Parse(value, System.Globalization.NumberStyles.HexNumber, null),
				1 => int.Parse(value, System.Globalization.NumberStyles.HexNumber, null),
				_ => value
			});

		//public static System.Collections.Generic.IEnumerable<string[]> GetDataStrings(System.Uri uri, params string[] fieldNames)
		//{uri.GetStream()
		//	yield return fieldNames;

		//	foreach (var itemArray in uri.ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplitter.Split(line)))
		//		yield return itemArray;
		//}
		//public static System.Collections.Generic.IEnumerable<string[]> GetRawStrings()
		//	=> LocalUri.ReadLines(System.Text.Encoding.UTF8).Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplitter.Split(line)))

		//public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.IO.Stream stream)
		//{
		//	var reSplitter = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

		//	using var streamReader = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);

		//	//streamReader.ReadLine
		//}

	}
}

//System.Console.WriteLine(nameof(Flux.Resources.Ucd.Blocks));
//foreach (var strings in new Flux.Resources.Ucd.Blocks().GetStrings(Flux.Resources.Ucd.Blocks.SourceUri))
//  System.Console.WriteLine(string.Join('|', strings));
