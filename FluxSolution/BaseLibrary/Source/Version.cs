using System.Linq;

namespace Flux
{
  /// <summary>Represents a general version implementation, similar to the built-in Version, but can handle more parts.</summary>
  public readonly partial record struct Version
    : IComparable<Version>, IEquatable<Version>
  {
    [System.Text.RegularExpressions.GeneratedRegex("[^0-9]+")]
    private static partial System.Text.RegularExpressions.Regex m_reSplit();

    private readonly int[] m_parts;

    public Version(int countOfParts) => m_parts = new int[countOfParts];
    public Version(params int[] parts) => m_parts = parts ?? throw new ArgumentNullException(nameof(parts));
    public Version(System.Version version)
      => m_parts = version is null
      ? throw new ArgumentNullException(nameof(version))
      : new int[] { version.Major, version.Minor, version.Build, version.Revision };

    public int this[int index]
    {
      get => index >= 0 && index < m_parts.Length ? m_parts[index] : throw new ArgumentOutOfRangeException(nameof(index));
      set => m_parts[index] = index >= 0 && index < m_parts.Length ? value : throw new ArgumentOutOfRangeException(nameof(index));
    }

    public int Count => m_parts.Length;

    public System.Version ToVersion()
      => Count switch
      {
        2 => new System.Version(m_parts[0], m_parts[1]),
        3 => new System.Version(m_parts[0], m_parts[1], m_parts[2]),
        4 => new System.Version(m_parts[0], m_parts[1], m_parts[2], m_parts[3]),
        _ => throw new NotSupportedException($"The .NET version does not support {m_parts.Length} parts.")
      };

    #region Static methods

    public static Version Parse(string version)
      => version is null
      ? throw new ArgumentNullException(nameof(version))
      : new(m_reSplit().Split(version).Where(e => !string.IsNullOrWhiteSpace(e)).Select(part => int.Parse(part, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture)).ToArray());

    public static bool TryParse(string version, out Version result)
    {
      try
      {
        result = Parse(version);
        return true;
      }
      catch { }

      result = default;
      return false;
    }

    #endregion Static methods

    #region Overloaded operators

    public static bool operator <(Version a, Version b) => a.CompareTo(b) < 0;
    public static bool operator <=(Version a, Version b) => a.CompareTo(b) <= 0;
    public static bool operator >(Version a, Version b) => a.CompareTo(b) > 0;
    public static bool operator >=(Version a, Version b) => a.CompareTo(b) >= 0;

    #endregion Overloaded operators

    #region Implemented interfaces

    // IComparable
    public int CompareTo(Version other)
    {
      var sl = m_parts.Length;
      var tl = other.m_parts.Length;

      var minLength = int.Min(sl, tl);

      for (var index = 0; index < minLength; index++)
        if (m_parts[index] is var sv && other.m_parts[index] is var tv && sv != tv)
          return sv > tv ? 1 : -1;

      return sl == tl ? 0 : (sl > tl ? 1 : -1);
    }

    #endregion Implemented interfaces

    public override string? ToString() => string.Join('.', m_parts);
  }
}