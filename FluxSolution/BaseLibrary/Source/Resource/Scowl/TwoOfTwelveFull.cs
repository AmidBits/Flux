using System.Linq;

namespace Flux.Resources.Scowl
{
	public sealed class TwoOfTwelveFull
		: ATabularDataAcquirer
	{
		public static string LocalFile
			=> @"file://\Resources\Scowl\2of12full.txt";
		public static System.Uri UriSource
			=> new(@"https://raw.githubusercontent.com/en-wl/wordlist/master/alt12dicts/2of12full.txt");

		public System.Uri Uri { get; private set; }

		public TwoOfTwelveFull(System.Uri uri)
			=> Uri = uri;

		/// <summary>The records from 2Of12Full word list.</summary>
		/// <see cref="https://github.com/en-wl/wordlist"/>
		// Download URL: https://github.com/en-wl/wordlist/raw/master/alt12dicts/2of12full.txt
		/// <seealso cref="http://wordlist.aspell.net/"/>
		/// <seealso cref="https://github.com/en-wl/wordlist/blob/master/"/>
		public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
		{
			yield return new string[] { "DictionaryCount", "NonVariant", "Variant", "NonAmerican", "SecondClass", "Word" };

			var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=[\-0-9]+[:#&=]?)\s+", System.Text.RegularExpressions.RegexOptions.Compiled);

			using var sr = new System.IO.StreamReader(Uri.GetStream(), System.Text.Encoding.UTF8);

			foreach (var item in sr.ReadLines(false).Select(s => reSplit.Split(s.Trim())))
				yield return item;
		}
	}
}
