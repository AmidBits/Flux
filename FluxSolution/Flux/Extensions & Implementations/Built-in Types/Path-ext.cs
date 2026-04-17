namespace Flux
{
  public static partial class PathExtensions
  {
    extension(System.IO.Path)
    {
      /// <summary>
      /// <para>The most common used directory separator characters '/' and '\'.</para>
      /// </summary>
      public static char[] DirectorySeparatorCharacters => ['/', '\\'];

      /// <summary>
      /// <para>Finds all directory separators used, if any, in order of most frequent, and if tied, in order of appearance.</para>
      /// </summary>
      /// <param name="path"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<char> FindDirectorySeparatorChars(string path)
      {
        var countFrequent = new Dictionary<char, int>();
        var firstAppeared = new Dictionary<char, int>();

        for (var i = 0; i < path.Length; i++)
        {
          var c = path[i];

          if (System.IO.Path.DirectorySeparatorCharacters.Contains(c))
          {
            if (!countFrequent.TryGetValue(c, out int v))
            {
              countFrequent[c] = 1;
              firstAppeared[c] = i;
            }
            else
              countFrequent[c] = ++v;
          }
        }

        return countFrequent
          .OrderByDescending(kvp => kvp.Value) // Highest frequency first.
          .ThenBy(kvp => firstAppeared[kvp.Key]) // Tie-break by first appearance.
          .Select(kvp => kvp.Key)
          .DefaultIfEmpty(System.IO.Path.DirectorySeparatorChar)
          .ToList();
      }

      /// <summary>
      /// <para>Gets the common path from the provided paths using the specified string comparison.</para>
      /// </summary>
      /// <param name="stringComparison"></param>
      /// <param name="paths"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<string> GetCommonPathPrefix(System.StringComparison stringComparison, params string[] paths)
      {
        var pathRanges = paths.Select(path => path.AsSpan().SplitRanges(null, '/', '\\')).ToList();

        var minimumSegments = pathRanges.Min(l => l.Count);

        var commonCount = -1;

        var allEqual = true;

        for (var i = 0; i < minimumSegments; i++) // Enumerate segments.
        {
          var a = paths[0][pathRanges[0][i]];

          for (var k = paths.Length - 1; k >= 1; k--) // Enumerate paths.
          {
            var b = paths[k][pathRanges[k][i]];

            allEqual = a.Equals(b, stringComparison);

            if (!allEqual)
              break;
          }

          if (!allEqual)
            break;

          commonCount = i + 1;
        }

        var list = new System.Collections.Generic.List<string>();

        var sb = new System.Text.StringBuilder();

        for (var i = 0; i < paths.Length; i++)
        {
          var path = paths[i];
          var ranges = pathRanges[i];

          var directorySeparatorChar = FindDirectorySeparatorChars(path).FirstOrValue(System.IO.Path.DirectorySeparatorChar).Item;

          sb.Clear();
          sb.Append(path.AsSpan().JoinRanges(ranges, commonCount, directorySeparatorChar));

          if (commonCount < ranges.Count)
            sb.Append(directorySeparatorChar);

          var indexMap = path.AsSpan().CreateIndexMap(c => c);

          foreach (var slash in System.IO.Path.DirectorySeparatorCharacters)
            if (indexMap.TryGetValue(slash, out var slashList))
              foreach (var index in slashList)
                if (index < sb.Length)
                  sb[index] = slash;

          list.Add(sb.ToString());
        }

        return list;
      }

      /// <summary>
      /// <para>Replaces '/' and '\' with the <see cref="System.IO.Path.DirectorySeparatorChar"/>, or if <paramref name="useAltDirectorySeparatorChar"/> then <see cref="System.IO.Path.AltDirectorySeparatorChar"/>.</para>
      /// </summary>
      /// <param name="path"></param>
      /// <param name="directorySeparatorChar"></param>
      public static void SetDirectorySeparatorChar(System.Span<char> path, bool useAltDirectorySeparatorChar = false)
        => path.Replace((e, i) => System.IO.Path.DirectorySeparatorCharacters.Contains(e) ? (useAltDirectorySeparatorChar ? System.IO.Path.AltDirectorySeparatorChar : System.IO.Path.DirectorySeparatorChar) : e);
    }
  }
}
