namespace Flux
{
  public static partial class PathExtensions
  {
    internal static readonly char[] m_pathSeparators = ['/', '\\'];

    extension(System.IO.Path)
    {
      public static System.Span<char> GetCommonPathPrefix(char separator, System.ReadOnlySpan<string> paths)
      {
        var items = new System.Collections.Generic.List<System.Collections.Generic.List<Range>>();

        var minimumSegments = int.MaxValue;

        foreach (var p in paths)
        {
          if (string.IsNullOrWhiteSpace(p))
            continue;

          var s = p.TrimEnd(m_pathSeparators);

          var splitRanges = s.AsSpan().SplitRanges(null, m_pathSeparators);

          if (splitRanges.Count < minimumSegments)
            minimumSegments = splitRanges.Count;

          items.Add(splitRanges);
        }

        if (items.Count == 0)
          return [];

        var commonCount = 0;

        for (var seg = 0; seg < minimumSegments; seg++)
        {
          var first = paths[0].AsSpan(items[0][seg]);

          var allMatch = true;

          for (var i = 1; i < items.Count; i++)
          {
            var other = paths[i].AsSpan(items[i][seg]);

            if (!first.Equals(other, StringComparison.OrdinalIgnoreCase))
            {
              allMatch = false;
              break;
            }
          }

          if (!allMatch)
            break;

          commonCount++;
        }

        if (commonCount == 0)
          return [];

        return paths[0].AsSpan().JoinRanges(items[0], commonCount, separator);
      }
    }
  }
}
