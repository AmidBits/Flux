namespace Flux
{
  public static partial class Types
  {
    private static bool TryReconstructGenericName(string? name, out string reconstructedName)
    {
      var sb = new System.Text.StringBuilder();

      if (name is not null)
      {
        var names = name.Split('`');

        sb.Append(names[0]);

        if (names.Length == 2)
        {
          if (int.TryParse(names[1], System.Globalization.NumberStyles.Integer, null, out var count))
          {
            if (count > 1)
              sb.AppendJoin(Static.CommaSpace, System.Linq.Enumerable.Range(1, count).Select(i => $"T{i}"));
            else
              sb.Append('T');
          }
        }
      }

      reconstructedName = sb.ToString();
      return reconstructedName != name;
    }

    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string GetFullNameEx(this System.Type source)
      => TryReconstructGenericName(source?.FullName, out var reconstructedName) ? reconstructedName : throw new System.ArgumentNullException(nameof(source));

    /// <summary>Returns the name with various extended functionalities, e.g. "<T1, T2, T3>" for generics instead of "`3".</summary>
    public static string GetNameEx(this System.Type source)
      => TryReconstructGenericName(source?.Name, out var reconstructedName) ? reconstructedName : throw new System.ArgumentNullException(nameof(source));
  }
}
