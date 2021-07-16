namespace Flux
{
	/// <summary>This static class was created to not bloat every single object with extension methods for this functionality.</summary>
	public static partial class Object
	{
		/// <summary>Returns whether the source type is a primitive numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
		public static bool IsNumeric(object source, bool considerBigInteger = true)
			=> IsNumericFloatingPoint(source) || IsNumericInteger(source, considerBigInteger);

		/// <summary>Returns whether the source type is a primitive floating point numeric data type (i.e. <see cref="System.Decimal"/>, <see cref="System.Double"/> or <see cref="System.Single"/>).</summary>
		public static bool IsNumericFloatingPoint(object source)
			=> source is decimal || source is double || source is float;

		/// <summary>Returns whether the source type is a primitive integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
		public static bool IsNumericInteger(object source, bool considerBigInteger = true)
			=> IsNumericIntegerSigned(source, considerBigInteger) || IsNumericIntegerUnsigned(source);

		/// <summary>Returns whether the source type is a primitive signed integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
		public static bool IsNumericIntegerSigned(object source, bool considerBigInteger = true)
			=> source is short || source is int || source is long || source is sbyte || (considerBigInteger && source is System.Numerics.BigInteger);
		/// <summary>Returns whether the source type is a primitive unsigned integer numeric data type (e.g. <see cref="System.Int32"/> or <see cref="System.Int64"/>) and whether to consider <see cref="System.Numerics.BigInteger"/>.</summary>
		public static bool IsNumericIntegerUnsigned(object source)
			=> source is byte || source is ushort || source is uint || source is ulong;
	}
}
