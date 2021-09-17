//using System.Linq;

//namespace Flux.Model
//{
//  //public interface IMatrix<TValue>
//  //  where TValue : notnull
//  //{
//  //  TValue this[int x, int y, int z] { get; set; }
//  //}

//  public class MatrixFixed<TValue>
//    : System.ICloneable, System.IEquatable<MatrixFixed<TValue>>
//    where TValue : notnull
//  {
//    protected TValue[] m_values;
//    public System.Collections.Generic.IReadOnlyList<TValue> Values => m_values.ToList();
//    public int LengthX { get; private set; }
//    public int LengthY { get; private set; }
//    private int LengthXY { get; set; }
//    public int LengthZ { get; private set; }

//    public TValue this[int index]
//    {
//      get => m_values[index];
//      set => m_values[index] = value;
//    }
//    public TValue this[int x, int y, int z = 0]
//    {
//      get => m_values[CartesianToIndex(x, y, z)];
//      set => m_values[CartesianToIndex(x, y, z)] = value;
//    }
//    public TValue this[int x, int y]
//    {
//      get => m_values[CartesianToIndex(x, y, 0)];
//      set => m_values[CartesianToIndex(x, y, 0)] = value;
//    }

//    public MatrixFixed(int x, int y, int z = 1)
//    {
//      LengthX = x;
//      LengthY = y;
//      LengthXY = x * y;
//      LengthZ = z;

//      m_values = new TValue[x * y * z];
//    }

//    /// <summary>Creates a sequence of adjacent slots, relative to the specified (by index) slot.</summary>
//    public System.Collections.Generic.IEnumerable<int> Adjacent(int index)
//    {
//      var (x, y, z) = IndexToCartesian(index);

//      for (int zi = (z > 0 ? z - 1 : z), zmax = (z < LengthZ - 1 ? z + 1 : z); zi <= zmax; zi++)
//        for (int yi = (y > 0 ? y - 1 : y), ymax = (y < LengthY - 1 ? y + 1 : y); yi <= ymax; yi++)
//          for (int xi = (x > 0 ? x - 1 : x), xmax = (x < LengthX - 1 ? x + 1 : x); xi <= xmax; xi++)
//            if (CartesianToIndex(xi, yi, zi) is var ixyz && ixyz != index)
//              yield return ixyz;
//    }
//    /// <summary>Creates a sequence of adjacent slots, relative to the specified (by x, y and z) slot.</summary>
//    public System.Collections.Generic.IEnumerable<int> Adjacent(int x, int y, int z)
//      => Adjacent(CartesianToIndex(x, y, z));

//    /// <summary>Converts an integer cartesian coordinate to a linear index, based on the x, y, z lengths.</summary>
//    public int CartesianToIndex(int x, int y, int z)
//      => x + y * LengthX + z * LengthXY;
//    /// <summary>Converts a linear index to an integer cartesian coordinate, based on the x, y, z lengths.</summary>
//    public (int x, int y, int z) IndexToCartesian(int index)
//    {
//      var irxy = index % LengthXY;

//      return (irxy % LengthX, irxy / LengthX, index / (LengthXY));
//    }

//    public void Initialize(TValue value)
//      => System.Array.Fill(m_values, value);

//    public object Clone()
//    {
//      var clone = new MatrixFixed<TValue>(LengthX, LengthY, LengthZ);
//      System.Array.Copy(m_values, clone.m_values, m_values.Length);
//      return clone;
//    }

//    // Operators
//    public static bool operator ==(MatrixFixed<TValue> a, MatrixFixed<TValue> b)
//      => !(a is null) && a.Equals(b);
//    public static bool operator !=(MatrixFixed<TValue> a, MatrixFixed<TValue> b)
//      => !(a is null) && !a.Equals(b);
//    // IEquatable
//    public bool Equals([System.Diagnostics.CodeAnalysis.AllowNull] MatrixFixed<TValue> other)
//    {
//      if (other is null || other.m_values.Length != m_values.Length) return false;

//      for (var index = 0; index < m_values.Length; index++)
//      {
//        var value = m_values[index];
//        var otherValue = other.m_values[index];

//        if (!(ReferenceEquals(value, otherValue) || value.Equals(otherValue))) return false;
//      }

//      return true;
//    }
//    // Object (overrides)
//    public override bool Equals(object? obj)
//      => obj is MatrixFixed<TValue> && Equals(obj);
//    public override int GetHashCode()
//      => Flux.HashCode.Combine(m_values);
//    public override string? ToString()
//    {
//      return $"<{this.GetType().Name}, X={LengthX}, Y={LengthY}, Z={LengthZ}>";
//    }
//  }
//}
