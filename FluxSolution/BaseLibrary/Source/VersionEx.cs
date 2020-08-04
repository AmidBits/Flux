using System.Linq;

namespace Flux
{
  public struct VersionEx
    : System.IComparable<VersionEx>, System.IEquatable<VersionEx>, System.IFormattable
  {
    private static readonly char[] m_separatorsArray = new char[] { '.' };
    private readonly int[] m_parts;

    public int Count
      => m_parts.Length;

    public int this[int index]
      => index >= 0 && index < m_parts.Length ? m_parts[index] : throw new System.ArgumentOutOfRangeException(nameof(index));

    public int Major { get => m_parts.Length > 0 ? m_parts[0] : throw new System.NotSupportedException(); set => m_parts[0] = m_parts.Length > 0 ? value : throw new System.NotSupportedException(); }
    public int Minor { get => m_parts.Length > 1 ? m_parts[1] : throw new System.NotSupportedException(); set => m_parts[1] = m_parts.Length > 1 ? value : throw new System.NotSupportedException(); }
    public int Build { get => m_parts.Length > 2 ? m_parts[2] : throw new System.NotSupportedException(); set => m_parts[2] = m_parts.Length > 2 ? value : throw new System.NotSupportedException(); }
    public int Revision { get => m_parts.Length > 3 ? m_parts[3] : throw new System.NotSupportedException(); set => m_parts[3] = m_parts.Length > 3 ? value : throw new System.NotSupportedException(); }

    public System.Version ToVersion()
      => Count switch
      {
        2 => new System.Version(m_parts[0], m_parts[1]),
        3 => new System.Version(m_parts[0], m_parts[1], m_parts[2]),
        4 => new System.Version(m_parts[0], m_parts[1], m_parts[2], m_parts[3]),
        _ => throw new System.NotSupportedException()
      };

    public VersionEx(params int[] parts)
    {
      if (parts.Length < 1) throw new System.ArgumentOutOfRangeException(nameof(parts));

      m_parts = (int[])parts.Clone();
    }
    public VersionEx(string version)
    {
      if (!TryParse(version, out var result)) throw new System.ArgumentException(@"Failed to parse.", nameof(version));

      m_parts = result.m_parts;
    }

    public static VersionEx FromVersion(System.Version version)
      => version is null ? throw new System.ArgumentNullException(nameof(version)) : new VersionEx(version.Major, version.Minor, version.Build, version.Revision);
    public static VersionEx Parse(string version)
      => new VersionEx((version ?? throw new System.ArgumentNullException(nameof(version))).Split(m_separatorsArray).Select(part => int.Parse(part, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture)).ToArray());
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

    // Operators
    public static bool operator ==(VersionEx a, VersionEx b)
      => a.Equals(b);
    public static bool operator !=(VersionEx a, VersionEx b)
      => !a.Equals(b);
    public static bool operator <(VersionEx a, VersionEx b)
      => a.CompareTo(b) < 0;
    public static bool operator <=(VersionEx a, VersionEx b)
      => a.CompareTo(b) <= 0;
    public static bool operator >(VersionEx a, VersionEx b)
      => a.CompareTo(b) < 0;
    public static bool operator >=(VersionEx a, VersionEx b)
      => a.CompareTo(b) <= 0;
    // IComparable
    public int CompareTo(VersionEx other)
    {
      if (m_parts.Length != other.m_parts.Length)
        return m_parts.Length > other.m_parts.Length ? 1 : -1;

      for (var index = 0; index < m_parts.Length; index++)
        if (m_parts[index] != other.m_parts[index])
          return m_parts[index] > other.m_parts[index] ? 1 : -1;

      return 0;
    }
    // IEquatable
    public bool Equals(VersionEx other)
    {
      if (m_parts.Length != other.m_parts.Length) return false;

      for (var index = 0; index < m_parts.Length; index++)
        if (m_parts[index] != other.m_parts[index])
          return false;

      return true;
    }
    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => string.Join(m_separatorsArray[0].ToString(formatProvider), m_parts.Select(i => i.ToString(formatProvider)));
    // Object (overrides)
    public override bool Equals(object? obj)
      => obj is VersionEx ? Equals((VersionEx)obj) : false;
    public override int GetHashCode()
      => Flux.HashCode.Combine(m_parts.Cast<object>().AsEnumerable());
    public override string? ToString()
      => base.ToString();
  }
}
