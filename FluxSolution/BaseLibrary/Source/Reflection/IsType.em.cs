namespace Flux
{
	public static partial class Reflect
	{
		/// <summary>Returns whether the source type is a primitive floating point numeric data type (i.e. <see cref="System.Decimal"/>, <see cref="System.Double"/> or <see cref="System.Single"/>).</summary>
		public static bool IsTypeFloatingPoint(object source)
			=> source is decimal || source is double || source is float;

		/// <summary>Returns whether the source type is a primitive integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
		public static bool IsTypeInteger(object source, bool considerBigInteger = true)
			=> source is byte || source is short || source is int || source is long || source is sbyte || source is ushort || source is uint || source is ulong || (considerBigInteger && source is System.Numerics.BigInteger);

		/// <summary>Returns whether the source type is any kind of numeric data type.</summary>
		public static bool IsTypeNumeric(object source)
			=> IsTypeFloatingPoint(source) || IsTypeInteger(source);
	}
}
