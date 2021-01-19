//using System.Linq;

//namespace Flux.Resources.ProjectGutenberg
//{
//	/// <summary>Get all the book title/author and number available in the Gutenberg project (from gutenberg.com</summary>
//	public class TableOfContents
//		: ResourceFactory
//	{
//		public static System.Uri LocalUri
//			=> new System.Uri(@"file://\Resources\ProjectGutenberg\GUTINDEX.ALL");
//		public static System.Uri SourceUri
//			=> new System.Uri(@"http://www.gutenberg.org/dirs/GUTINDEX.ALL");

//		public override System.Collections.Generic.IList<string> FieldNames
//			=> new string[] { @"Ebook", @"Number" };
//		public override System.Collections.Generic.IList<System.Type> FieldTypes
//			=> new System.Type[] { typeof(string), typeof(string) };

//		public override System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
//			=> uri.GetStream().ReadLines(System.Text.Encoding.UTF8).Where(s => s.Length == 78 && System.Text.RegularExpressions.Regex.IsMatch(s, @"^[\p{L}\p{N}\p{Zs}\p{P}]+\s{2,}\d+$")).Select(s => System.Text.RegularExpressions.Regex.Split(s, @"(?<=^.+)\s{2,}(?=\d+$)")).Prepend((string[])FieldNames);
//	}
//}

////System.Console.WriteLine(nameof(Flux.Resources.ProjectGutenberg.TableOfContents));
////foreach (var strings in new Flux.Resources.ProjectGutenberg.TableOfContents().GetStrings(Flux.Resources.ProjectGutenberg.TableOfContents.SourceUri))
////  System.Console.WriteLine(string.Join('|', strings));
