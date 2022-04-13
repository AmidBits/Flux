//using System.Linq;

namespace Flux
{
  /// <summary>Represents a general version implementation, similar to the built-in Version, but can handle more parts.</summary>
  public struct VersionEx
    : System.IComparable<VersionEx>, System.IEquatable<VersionEx>
  {
    public static readonly VersionEx Empty;

    private readonly int[] m_parts;

    public VersionEx(int numberOfParts)
    {
      if (numberOfParts < 1 || numberOfParts > 9) throw new System.ArgumentOutOfRangeException(nameof(numberOfParts));

      m_parts = new int[numberOfParts];
    }
    public VersionEx(params int[] parts)
      : this((parts ?? throw new System.ArgumentNullException(nameof(parts))).Length)
      => System.Array.Copy(parts, m_parts, parts.Length);
    public VersionEx(string version)
    {
      if (!TryParse(version, out var vex)) throw new System.ArgumentException(@"Could not parse version string.", nameof(version));

      m_parts = vex.m_parts;
    }

    public int this[int index]
    {
      get => index >= 0 && index < m_parts.Length ? m_parts[index] : throw new System.ArgumentOutOfRangeException(nameof(index));
      set => m_parts[index] = index >= 0 && index < m_parts.Length ? value : throw new System.ArgumentOutOfRangeException(nameof(index));
    }

    [System.Diagnostics.Contracts.Pure]
    public int Count
      => m_parts.Length;

    [System.Diagnostics.Contracts.Pure]
    public System.Version ToVersion()
      => Count switch
      {
        2 => new System.Version(m_parts[0], m_parts[1]),
        3 => new System.Version(m_parts[0], m_parts[1], m_parts[2]),
        4 => new System.Version(m_parts[0], m_parts[1], m_parts[2], m_parts[3]),
        _ => throw new System.NotSupportedException()
      };

    #region Static methods
    [System.Diagnostics.Contracts.Pure]
    public static VersionEx FromVersion(System.Version version)
      => version is null ? throw new System.ArgumentNullException(nameof(version)) : new VersionEx(version.Major, version.Minor, version.Build, version.Revision);
    [System.Diagnostics.Contracts.Pure]
    public static VersionEx Parse(string version)
    {
      if (version is null) throw new System.ArgumentNullException(nameof(version));

      var array = System.Text.RegularExpressions.Regex.Split(version, "[^0-9]+");

      return new(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select(System.Linq.Enumerable.Where(array, e => !string.IsNullOrWhiteSpace(e)), part => int.Parse(part, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture))));
    }
    //=> new(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Select((version ?? throw new System.ArgumentNullException(nameof(version))).Split(m_separatorsArray), part => int.Parse(part, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture))));
    [System.Diagnostics.Contracts.Pure]
    public static bool TryParse(string version, out VersionEx result)
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
    [System.Diagnostics.Contracts.Pure]
    public static bool operator <(VersionEx a, VersionEx b)
      => a.CompareTo(b) < 0;
    [System.Diagnostics.Contracts.Pure]
    public static bool operator <=(VersionEx a, VersionEx b)
      => a.CompareTo(b) <= 0;
    [System.Diagnostics.Contracts.Pure]
    public static bool operator >(VersionEx a, VersionEx b)
      => a.CompareTo(b) > 0;
    [System.Diagnostics.Contracts.Pure]
    public static bool operator >=(VersionEx a, VersionEx b)
      => a.CompareTo(b) >= 0;

    [System.Diagnostics.Contracts.Pure]
    public static bool operator ==(VersionEx a, VersionEx b)
      => a.Equals(b);
    [System.Diagnostics.Contracts.Pure]
    public static bool operator !=(VersionEx a, VersionEx b)
      => !a.Equals(b);
    #endregion Overloaded operators

    #region Implemented interfaces
    // IComparable
    [System.Diagnostics.Contracts.Pure]
    public int CompareTo(VersionEx other)
    {
      if (m_parts.Length is var pl && other.m_parts.Length is var opl && pl != opl)
        return pl > opl ? 1 : -1;

      for (var index = 0; index < pl; index++)
        if (m_parts[index] is var pi && other.m_parts[index] is var opi && pi != opi)
          return pi > opi ? 1 : -1;

      return 0;
    }

    // IEquatable
    [System.Diagnostics.Contracts.Pure]
    public bool Equals(VersionEx other)
    {
      if (m_parts.Length != other.m_parts.Length)
        return false;

      for (var index = 0; index < m_parts.Length; index++)
        if (m_parts[index] != other.m_parts[index])
          return false;

      return true;
    }
    #endregion Implemented interfaces

    #region Object overrides
    [System.Diagnostics.Contracts.Pure]
    public override bool Equals(object? obj)
      => obj is VersionEx o && Equals(o);
    [System.Diagnostics.Contracts.Pure]
    public override int GetHashCode()
      => m_parts.CombineHashCodes();
    [System.Diagnostics.Contracts.Pure]
    public override string? ToString()
      => string.Join('.', m_parts);
    #endregion Object overrides
  }
}
