using System.Linq;

namespace Flux.Resources.ProjectGutenberg
{
	public class TableOfContents
		: ITabularDataAcquirer
	{
		public static System.Uri UriLocal
			=> new System.Uri(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
		public static System.Uri UriSource
			=> new System.Uri(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

		public System.Uri Uri { get; private set; }

		public TableOfContents(System.Uri uri)
			=> Uri = uri;

		/// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
		public System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
		{
			yield return new string[] { @"Ebook", @"Number" };

			var reMatch = new System.Text.RegularExpressions.Regex(@"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$", System.Text.RegularExpressions.RegexOptions.Compiled);
			var reSplit = new System.Text.RegularExpressions.Regex(@"(?<=^.+)\s{2,}(?=\d+$)", System.Text.RegularExpressions.RegexOptions.Compiled);

			foreach (var item in Uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(s => s.Length == 78 && reMatch.IsMatch(s)).Select(s => reSplit.Split(s)))
				yield return item;
		}
	}
}
