namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Returns the <paramref name="value"/> (of integer type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => System.Numerics.BigInteger.CreateChecked(value);

    /// <summary>Returns the <paramref name="value"/> (of floating-point type <typeparamref name="TSelf"/>) as a <see cref="System.Numerics.BigInteger"/> using the rounding <paramref name="mode"/>.</summary>
    public static System.Numerics.BigInteger ToBigInteger<TSelf>(this TSelf value, RoundingMode mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => System.Numerics.BigInteger.CreateChecked(value.Round(mode));

#else

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this byte source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this decimal source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this double source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this short source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this int source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this long source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this sbyte source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this float source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this ushort source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this uint source)
      => new System.Numerics.BigInteger(source);

    /// <summary>Convert the source to a BigInteger.</summary>
    [System.CLSCompliant(false)]
    public static System.Numerics.BigInteger ToBigInteger(this ulong source)
      => new System.Numerics.BigInteger(source);

#endif
  }
}
