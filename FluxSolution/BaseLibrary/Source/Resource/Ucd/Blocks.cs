using System.Linq;

namespace Flux.Resources.Ucd
{
  public sealed class Blocks
    : ATabularDataAcquirer
  {
    public static string LocalFile
      => @"file://\Resources\Ucd\Blocks.txt";
    public static System.Uri UriSource
      => new(@"https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt");

    public System.Uri Uri { get; private set; }

    public Blocks(System.Uri uri)
      => Uri = uri;

    /// <summary>The Unicode block database.</summary>
    /// <see cref="https://www.unicode.org/"/>
    /// <seealso cref="https://unicode.org/Public/"/>
    /// <seealso cref="https://www.unicode.org/Public/UCD/latest/ucd"/>
    // Download URL: https://www.unicode.org/Public/UCD/latest/ucd/Blocks.txt
    public override System.Collections.Generic.IEnumerable<object[]> AcquireTabularData()
    {
      using var e = GetStrings(Uri).GetEnumerator();

      if (e.MoveNext())
      {
        yield return e.Current;

        while (e.MoveNext())
        {
          var objects = new object[e.Current.Length];

          for (var index = objects.Length - 1; index >= 0; index--)
          {
            switch (index)
            {
              case 0:
              case 1:
                if (int.TryParse(e.Current[index], System.Globalization.NumberStyles.HexNumber, null, out var value))
                  objects[index] = value;
                else
                  objects[index] = System.DBNull.Value;
                break;
              default:
                objects[index] = e.Current[index];
                break;
            }
          }

          yield return objects;
        }
      }

      static System.Collections.Generic.IEnumerable<string[]> GetStrings(System.Uri uri)
      {
        yield return new string[] { "StartCode", "EndCode", "BlockName" };

        if (uri is null) throw new System.ArgumentNullException(nameof(uri));

        var m_reSplit = new System.Text.RegularExpressions.Regex(@"(\.\.|; )", System.Text.RegularExpressions.RegexOptions.ExplicitCapture);

        using var sr = new System.IO.StreamReader(uri.GetStream(), System.Text.Encoding.UTF8);

        foreach (var stringArray in sr.ReadLines().Where(line => line.Length > 0 && !line.StartsWith('#')).Select(line => m_reSplit.Split(line)))
          yield return stringArray;
      }
    }
  }
}
