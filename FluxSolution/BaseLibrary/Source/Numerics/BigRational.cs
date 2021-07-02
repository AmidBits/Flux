////   Copyright (c) Microsoft Corporation.  All rights reserved.
///*============================================================
//** Class: BigRational
//**
//** Purpose: 
//** --------
//** This class is used to represent an arbitrary precision
//** BigRational number
//**
//** A rational number (commonly called a fraction) is a ratio
//** between two integers.  For example (3/6) = (2/4) = (1/2)
//**
//** Arithmetic
//** ----------
//** a/b = c/d, iff ad = bc
//** a/b + c/d  == (ad + bc)/bd
//** a/b - c/d  == (ad - bc)/bd
//** a/b % c/d  == (ad % bc)/bd
//** a/b * c/d  == (ac)/(bd)
//** a/b / c/d  == (ad)/(bc)
//** -(a/b)     == (-a)/b
//** (a/b)^(-1) == b/a, if a != 0
//**
//** Reduction Algorithm
//** ------------------------
//** Euclid's algorithm is used to simplify the fraction.
//** Calculating the greatest common divisor of two n-digit
//** numbers can be found in
//**
//** O(n(log n)^5 (log log n)) steps as n -> +infinity
//============================================================*/

//namespace Flux.Numerics
//{
//	//using System;
//	//using System.Globalization;
//	//using System.Numerics;
//	//using System.Runtime.InteropServices;
//	//using System.Runtime.Serialization;
//	//using System.Security.Permissions;
//	//using System.Text;

//	public struct BigRational
//		: System.IComparable, System.IComparable<BigRational>, /*System.IConvertible, */System.IEquatable<BigRational>/*, System.IFormattable*/
//	{
//		private static readonly BigRational Zero = new BigRational(System.Numerics.BigInteger.Zero);
//		private static readonly BigRational One = new BigRational(System.Numerics.BigInteger.One);
//		private static readonly BigRational MinusOne = new BigRational(System.Numerics.BigInteger.MinusOne);

//		// ---- SECTION:  members supporting exposed properties -------------*
//		private System.Numerics.BigInteger m_numerator;
//		private System.Numerics.BigInteger m_denominator;

//		// ---- SECTION:  members for internal support ---------*
//		#region Members for Internal Support
//		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
//		internal struct DoubleUlong
//		{
//			[System.Runtime.InteropServices.FieldOffset(0)]
//			public double dbl;
//			[System.Runtime.InteropServices.FieldOffset(0)]
//			public ulong uu;
//		}
//		private const int DoubleMaxScale = 308;
//		private static readonly System.Numerics.BigInteger s_bnDoublePrecision = System.Numerics.BigInteger.Pow(10, DoubleMaxScale);
//		private static readonly System.Numerics.BigInteger s_bnDoubleMaxValue = (System.Numerics.BigInteger)double.MaxValue;
//		private static readonly System.Numerics.BigInteger s_bnDoubleMinValue = (System.Numerics.BigInteger)double.MinValue;

//		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
//		internal struct DecimalUInt32
//		{
//			[System.Runtime.InteropServices.FieldOffset(0)]
//			public decimal dec;
//			[System.Runtime.InteropServices.FieldOffset(0)]
//			public int flags;
//		}
//		private const int DecimalScaleMask = 0x00FF0000;
//		private const int DecimalSignMask = unchecked((int)0x80000000);
//		private const int DecimalMaxScale = 28;
//		private static readonly System.Numerics.BigInteger s_bnDecimalPrecision = System.Numerics.BigInteger.Pow(10, DecimalMaxScale);
//		private static readonly System.Numerics.BigInteger s_bnDecimalMaxValue = (System.Numerics.BigInteger)decimal.MaxValue;
//		private static readonly System.Numerics.BigInteger s_bnDecimalMinValue = (System.Numerics.BigInteger)decimal.MinValue;

//		private const string c_solidus = @"\u002F";
//		#endregion Members for Internal Support

//		public System.Numerics.BigInteger Denominator
//			=> m_denominator;
//		public System.Numerics.BigInteger Numerator
//			=> m_numerator;
//		public int Sign
//			=> m_numerator.Sign;

//		// ---- SECTION: public instance methods --------------*
//		#region Public Instance Methods

//		// GetWholePart() and GetFractionPart()
//		// 
//		// BigRational == Whole, Fraction
//		//  0/2        ==     0,  0/2
//		//  1/2        ==     0,  1/2
//		// -1/2        ==     0, -1/2
//		//  1/1        ==     1,  0/1
//		// -1/1        ==    -1,  0/1
//		// -3/2        ==    -1, -1/2
//		//  3/2        ==     1,  1/2
//		public System.Numerics.BigInteger GetWholePart()
//			=> System.Numerics.BigInteger.Divide(m_numerator, m_denominator);

//		public BigRational GetFractionPart()
//			=> new BigRational(System.Numerics.BigInteger.Remainder(m_numerator, m_denominator), m_denominator);


//		// IComparable
//		int System.IComparable.CompareTo(object? obj)
//			=> obj is BigRational o
//			? Compare(this, o)
//			: obj is null
//			? throw new System.ArgumentException("Argument must be of type BigRational.", nameof(obj))
//			: 1;

//		// IComparable<BigRational>
//		public int CompareTo(BigRational other)
//			=> Compare(this, other);

//		// Object Overrides
//		public override bool Equals(object? obj)
//			=> obj is BigRational o
//			? Equals(o)
//			: false;
//		public override int GetHashCode()
//			=> (m_numerator / m_denominator).GetHashCode();
//		public override string ToString()
//		{
//			System.Text.StringBuilder sb = new System.Text.StringBuilder();
//			sb.Append(m_numerator.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
//			sb.Append(c_solidus);
//			sb.Append(m_denominator.ToString("R", System.Globalization.CultureInfo.InvariantCulture));
//			return sb.ToString();
//		}

//		// IEquatable<BigRational>
//		// a/b = c/d, iff ad = bc
//		public System.Boolean Equals(BigRational other)
//			=> this.Denominator == other.Denominator
//			? m_numerator == other.m_numerator
//			: (m_numerator * other.Denominator) == (Denominator * other.m_numerator);

//		#endregion Public Instance Methods

//		#region Constructors
//		public BigRational(System.Numerics.BigInteger numerator)
//		{
//			m_numerator = numerator;
//			m_denominator = System.Numerics.BigInteger.One;
//		}
//		// BigRational(Double)
//		public BigRational(System.Double value)
//		{
//			if (System.Double.IsNaN(value))
//				throw new System.ArgumentException("Argument is not a number", "value");
//			else if (System.Double.IsInfinity(value))
//				throw new System.ArgumentException("Argument is infinity", "value");

//			bool isFinite;
//			int sign;
//			int exponent;
//			ulong significand;

//			SplitDoubleIntoParts(value, out sign, out exponent, out significand, out isFinite);

//			if (significand == 0)
//			{
//				this = BigRational.Zero;
//				return;
//			}

//			m_numerator = significand;
//			m_denominator = 1 << 52;

//			if (exponent > 0)
//				m_numerator = System.Numerics.BigInteger.Pow(m_numerator, exponent);
//			else if (exponent < 0)
//				m_denominator = System.Numerics.BigInteger.Pow(m_denominator, -exponent);

//			if (sign < 0)
//				m_numerator = System.Numerics.BigInteger.Negate(m_numerator);

//			Simplify();
//		}
//		// BigRational(Decimal) - The Decimal type represents floating point numbers exactly, with no rounding error.
//		// Values such as "0.1" in Decimal are actually representable, and convert cleanly to BigRational as "11/10"
//		public BigRational(System.Decimal value)
//		{
//			int[] bits = System.Decimal.GetBits(value);

//			if (bits == null || bits.Length != 4 || (bits[3] & ~(DecimalSignMask | DecimalScaleMask)) != 0 || (bits[3] & DecimalScaleMask) > (28 << 16))
//				throw new System.ArgumentException("Invalid Decimal", nameof(value));

//			if (value == System.Decimal.Zero)
//			{
//				this = BigRational.Zero;
//				return;
//			}

//			// build up the numerator
//			ulong ul = (((ulong)(uint)bits[2]) << 32) | ((ulong)(uint)bits[1]);   // (hi    << 32) | (mid)
//			m_numerator = (new System.Numerics.BigInteger(ul) << 32) | (uint)bits[0];             // (hiMid << 32) | (low)

//			bool isNegative = (bits[3] & DecimalSignMask) != 0;
//			if (isNegative)
//			{
//				m_numerator = System.Numerics.BigInteger.Negate(m_numerator);
//			}

//			// build up the denominator
//			int scale = (bits[3] & DecimalScaleMask) >> 16;     // 0-28, power of 10 to divide numerator by
//			m_denominator = System.Numerics.BigInteger.Pow(10, scale);

//			Simplify();
//		}
//		public BigRational(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
//		{
//			if (denominator.Sign == 0) throw new System.DivideByZeroException();
//			else if (numerator.Sign == 0)
//			{
//				// 0/m -> 0/1
//				m_numerator = System.Numerics.BigInteger.Zero;
//				m_denominator = System.Numerics.BigInteger.One;
//			}
//			else if (denominator.Sign < 0)
//			{
//				m_numerator = System.Numerics.BigInteger.Negate(numerator);
//				m_denominator = System.Numerics.BigInteger.Negate(denominator);
//			}
//			else
//			{
//				m_numerator = numerator;
//				m_denominator = denominator;
//			}

//			Simplify();
//		}
//		public BigRational(System.Numerics.BigInteger whole, System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
//		{
//			if (denominator.Sign == 0) throw new System.DivideByZeroException();
//			else if (numerator.Sign == 0 && whole.Sign == 0)
//			{
//				m_numerator = System.Numerics.BigInteger.Zero;
//				m_denominator = System.Numerics.BigInteger.One;
//			}
//			else if (denominator.Sign < 0)
//			{
//				m_denominator = System.Numerics.BigInteger.Negate(denominator);
//				m_numerator = (System.Numerics.BigInteger.Negate(whole) * m_denominator) + System.Numerics.BigInteger.Negate(numerator);
//			}
//			else
//			{
//				m_denominator = denominator;
//				m_numerator = (whole * denominator) + numerator;
//			}

//			Simplify();
//		}
//		#endregion Constructors

//		// -------- SECTION: public static methods -----------------*
//		#region Public Static Methods

//		public static BigRational Abs(BigRational r)
//			=> r.m_numerator.Sign < 0 ? new BigRational(System.Numerics.BigInteger.Abs(r.m_numerator), r.m_denominator) : r;
//		public static BigRational Add(BigRational x, BigRational y)
//			=> x + y;
//		public static int Compare(BigRational r1, BigRational r2) // a/b = c/d, iff ad = bc
//			=> System.Numerics.BigInteger.Compare(r1.m_numerator * r2.Denominator, r2.m_numerator * r1.Denominator);
//		public static BigRational Divide(BigRational dividend, BigRational divisor)
//			=> dividend / divisor;
//		public static BigRational DivRem(BigRational dividend, BigRational divisor, out BigRational remainder)
//		{
//			// a/b / c/d  == (ad)/(bc)
//			// a/b % c/d  == (ad % bc)/bd

//			// (ad) and (bc) need to be calculated for both the division and the remainder operations.
//			System.Numerics.BigInteger ad = dividend.m_numerator * divisor.Denominator;
//			System.Numerics.BigInteger bc = dividend.Denominator * divisor.m_numerator;
//			System.Numerics.BigInteger bd = dividend.Denominator * divisor.Denominator;

//			remainder = new BigRational(ad % bc, bd);
//			return new BigRational(ad, bc);
//		}
//		public static BigRational Negate(BigRational r)
//			=> new BigRational(System.Numerics.BigInteger.Negate(r.m_numerator), r.m_denominator);
//		public static BigRational Invert(BigRational r)
//			=> new BigRational(r.m_denominator, r.m_numerator);
//		// Least Common Denominator (LCD)
//		//
//		// The LCD is the least common multiple of the two denominators.  For instance, the LCD of
//		// {1/2, 1/4} is 4 because the least common multiple of 2 and 4 is 4.  Likewise, the LCD
//		// of {1/2, 1/3} is 6.
//		//       
//		// To find the LCD:
//		//
//		// 1) Find the Greatest Common Divisor (GCD) of the denominators
//		// 2) Multiply the denominators together
//		// 3) Divide the product of the denominators by the GCD
//		public static System.Numerics.BigInteger LeastCommonDenominator(BigRational x, BigRational y) // LCD( a/b, c/d ) == (bd) / gcd(b,d)
//			=> (x.Denominator * y.Denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(x.Denominator, y.Denominator);
//		public static BigRational Multiply(BigRational x, BigRational y)
//			=> x * y;
//		public static BigRational Remainder(BigRational dividend, BigRational divisor)
//			=> dividend % divisor;
//		public static BigRational Subtract(BigRational x, BigRational y)
//			=> x - y;
//		public static BigRational Pow(BigRational baseValue, System.Numerics.BigInteger exponent)
//		{
//			if (exponent.Sign == 0) // 0^0 -> 1, n^0 -> 1
//				return BigRational.One;
//			else if (exponent.Sign < 0)
//			{
//				if (baseValue == BigRational.Zero)
//					throw new System.ArgumentException("Cannot raise zero to a negative power.", nameof(baseValue));

//				// n^(-e) -> (1/n)^e
//				baseValue = BigRational.Invert(baseValue);
//				exponent = System.Numerics.BigInteger.Negate(exponent);
//			}

//			var result = baseValue;
//			while (exponent > System.Numerics.BigInteger.One)
//			{
//				result *= baseValue;
//				exponent--;
//			}
//			return result;
//		}
//		#endregion Public Static Methods

//		#region Operator Overloads
//		public static bool operator ==(BigRational x, BigRational y) => Compare(x, y) == 0;
//		public static bool operator !=(BigRational x, BigRational y) => Compare(x, y) != 0;
//		public static bool operator <(BigRational x, BigRational y) => Compare(x, y) < 0;
//		public static bool operator <=(BigRational x, BigRational y) => Compare(x, y) <= 0;
//		public static bool operator >(BigRational x, BigRational y) => Compare(x, y) > 0;
//		public static bool operator >=(BigRational x, BigRational y) => Compare(x, y) >= 0;
//		public static BigRational operator +(BigRational r)
//			=> r;
//		public static BigRational operator -(BigRational r)
//			=> new BigRational(-r.m_numerator, r.Denominator);
//		public static BigRational operator ++(BigRational r)
//			=> r + BigRational.One;
//		public static BigRational operator --(BigRational r)
//			=> r - BigRational.One;
//		public static BigRational operator +(BigRational r1, BigRational r2) // a/b + c/d  == (ad + bc)/bd
//			=> new BigRational((r1.m_numerator * r2.Denominator) + (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
//		public static BigRational operator -(BigRational r1, BigRational r2)  // a/b - c/d  == (ad - bc)/bd
//			=> new BigRational((r1.m_numerator * r2.Denominator) - (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
//		public static BigRational operator *(BigRational r1, BigRational r2) // a/b * c/d  == (ac)/(bd)
//			=> new BigRational((r1.m_numerator * r2.m_numerator), (r1.Denominator * r2.Denominator));
//		public static BigRational operator /(BigRational r1, BigRational r2) // a/b / c/d  == (ad)/(bc)
//			=> new BigRational((r1.m_numerator * r2.Denominator), (r1.Denominator * r2.m_numerator));
//		public static BigRational operator %(BigRational r1, BigRational r2) // a/b % c/d  == (ad % bc)/bd
//			=> new BigRational((r1.m_numerator * r2.Denominator) % (r1.Denominator * r2.m_numerator), (r1.Denominator * r2.Denominator));
//		#endregion Operator Overloads

//		#region Explicit conversions from BigRational to numeric base types.
//		[System.CLSCompliant(false)]
//		public static explicit operator sbyte(BigRational value) => (System.SByte)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		[System.CLSCompliant(false)]
//		public static explicit operator ushort(BigRational value) => (System.UInt16)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		[System.CLSCompliant(false)]
//		public static explicit operator uint(BigRational value) => (System.UInt32)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		[System.CLSCompliant(false)]
//		public static explicit operator ulong(BigRational value) => (System.UInt64)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		public static explicit operator byte(BigRational value) => (System.Byte)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		public static explicit operator short(BigRational value) => (System.Int16)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		public static explicit operator int(BigRational value) => (System.Int32)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		public static explicit operator long(BigRational value) => (System.Int64)(System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator));
//		public static explicit operator System.Numerics.BigInteger(BigRational value) => System.Numerics.BigInteger.Divide(value.m_numerator, value.m_denominator);
//		// The Single value type represents a single-precision 32-bit number with values ranging from negative 3.402823e38 to positive 3.402823e38 values that do not fit into this range are returned as Infinity.
//		public static explicit operator float(BigRational value) => (System.Single)((System.Double)value);
//		// The Double value type represents a double-precision 64-bit number with values ranging from -1.79769313486232e308 to +1.79769313486232e308 values that do not fit into this range are returned as +/-Infinity.
//		public static explicit operator double(BigRational value)
//		{
//			if (SafeCastToDouble(value.m_numerator) && SafeCastToDouble(value.m_denominator))
//				return (System.Double)value.m_numerator / (System.Double)value.m_denominator;

//			// scale the numerator to preseve the fraction part through the integer division
//			System.Numerics.BigInteger denormalized = (value.m_numerator * s_bnDoublePrecision) / value.m_denominator;
//			if (denormalized.IsZero)
//				return (value.Sign < 0) ? System.BitConverter.Int64BitsToDouble(unchecked((long)0x8000000000000000)) : 0d; // underflow to -+0

//			System.Double result = 0;
//			bool isDouble = false;
//			int scale = DoubleMaxScale;

//			while (scale > 0)
//			{
//				if (!isDouble)
//				{
//					if (SafeCastToDouble(denormalized))
//					{
//						result = (System.Double)denormalized;
//						isDouble = true;
//					}
//					else
//					{
//						denormalized = denormalized / 10;
//					}
//				}
//				result = result / 10;
//				scale--;
//			}

//			if (!isDouble)
//				return (value.Sign < 0) ? System.Double.NegativeInfinity : System.Double.PositiveInfinity;
//			else
//				return result;
//		}
//		public static explicit operator decimal(BigRational value)
//		{
//			// The Decimal value type represents decimal numbers ranging
//			// from +79,228,162,514,264,337,593,543,950,335 to -79,228,162,514,264,337,593,543,950,335
//			// the binary representation of a Decimal value is of the form, ((-2^96 to 2^96) / 10^(0 to 28))
//			if (SafeCastToDecimal(value.m_numerator) && SafeCastToDecimal(value.m_denominator))
//			{
//				return (System.Decimal)value.m_numerator / (System.Decimal)value.m_denominator;
//			}

//			// scale the numerator to preseve the fraction part through the integer division
//			System.Numerics.BigInteger denormalized = (value.m_numerator * s_bnDecimalPrecision) / value.m_denominator;

//			if (denormalized.IsZero)
//				return System.Decimal.Zero; // underflow - fraction is too small to fit in a decimal

//			for (int scale = DecimalMaxScale; scale >= 0; scale--)
//			{
//				if (!SafeCastToDecimal(denormalized))
//				{
//					denormalized = denormalized / 10;
//				}
//				else
//				{
//					DecimalUInt32 dec = new DecimalUInt32();
//					dec.dec = (System.Decimal)denormalized;
//					dec.flags = (dec.flags & ~DecimalScaleMask) | (scale << 16);
//					return dec.dec;
//				}
//			}

//			throw new System.OverflowException("Value was either too large or too small for a Decimal.");
//		}
//		#endregion Explicit conversions from BigRational to numeric base types.

//		#region Implicit conversions from numeric base types to BigRational.
//		[System.CLSCompliant(false)]
//		public static implicit operator BigRational(sbyte value) => new BigRational((System.Numerics.BigInteger)value);
//		[System.CLSCompliant(false)]
//		public static implicit operator BigRational(ushort value) => new BigRational((System.Numerics.BigInteger)value);
//		[System.CLSCompliant(false)]
//		public static implicit operator BigRational(uint value) => new BigRational((System.Numerics.BigInteger)value);
//		[System.CLSCompliant(false)]
//		public static implicit operator BigRational(ulong value) => new BigRational((System.Numerics.BigInteger)value);
//		public static implicit operator BigRational(byte value) => new BigRational((System.Numerics.BigInteger)value);
//		public static implicit operator BigRational(short value) => new BigRational((System.Numerics.BigInteger)value);
//		public static implicit operator BigRational(int value) => new BigRational((System.Numerics.BigInteger)value);
//		public static implicit operator BigRational(long value) => new BigRational((System.Numerics.BigInteger)value);
//		public static implicit operator BigRational(System.Numerics.BigInteger value) => new BigRational(value);
//		public static implicit operator BigRational(float value) => new BigRational((double)value);
//		public static implicit operator BigRational(double value) => new BigRational(value);
//		public static implicit operator BigRational(decimal value) => new BigRational(value);
//		#endregion Implicit conversions from numeric base types to BigRational.

//		#region Helper Methods
//		private void Simplify()
//		{
//			// * if the numerator is {0, +1, -1} then the fraction is already reduced
//			// * if the denominator is {+1} then the fraction is already reduced
//			if (m_numerator == System.Numerics.BigInteger.Zero)
//				m_denominator = System.Numerics.BigInteger.One;

//			System.Numerics.BigInteger gcd = System.Numerics.BigInteger.GreatestCommonDivisor(m_numerator, m_denominator);

//			if (gcd > System.Numerics.BigInteger.One)
//			{
//				m_numerator = m_numerator / gcd;
//				m_denominator = Denominator / gcd;
//			}
//		}

//		private static bool SafeCastToDecimal(System.Numerics.BigInteger value)
//			=> s_bnDecimalMinValue <= value && value <= s_bnDecimalMaxValue;
//		private static bool SafeCastToDouble(System.Numerics.BigInteger value)
//			=> s_bnDoubleMinValue <= value && value <= s_bnDoubleMaxValue;
//		private static void SplitDoubleIntoParts(double dbl, out int sign, out int exp, out ulong man, out bool isFinite)
//		{
//			DoubleUlong du;
//			du.uu = 0;
//			du.dbl = dbl;

//			sign = 1 - ((int)(du.uu >> 62) & 2);
//			man = du.uu & 0x000FFFFFFFFFFFFF;
//			exp = (int)(du.uu >> 52) & 0x7FF;
//			if (exp == 0)
//			{
//				// Denormalized number.
//				isFinite = true;
//				if (man != 0)
//					exp = -1074;
//			}
//			else if (exp == 0x7FF)
//			{
//				// NaN or Infinite.
//				isFinite = false;
//				exp = System.Int32.MaxValue;
//			}
//			else
//			{
//				isFinite = true;
//				man |= 0x0010000000000000; // mask in the implied leading 53rd significand bit
//				exp -= 1075;
//			}
//		}

//		private static double GetDoubleFromParts(int sign, int exp, ulong man)
//		{
//			DoubleUlong du;

//			du.dbl = 0;

//			if (man == 0)
//				du.uu = 0;
//			else
//			{
//				// Normalize so that 0x0010 0000 0000 0000 is the highest bit set
//				int cbitShift = CbitHighZero(man) - 11;

//				if (cbitShift < 0)
//					man >>= -cbitShift;
//				else
//					man <<= cbitShift;

//				// Move the point to just behind the leading 1: 0x001.0 0000 0000 0000 (52 bits) and skew the exponent (by 0x3FF == 1023)
//				exp += 1075;

//				if (exp >= 0x7FF) // Infinity
//					du.uu = 0x7FF0000000000000;
//				else if (exp <= 0)
//				{
//					// Denormalized
//					exp--;

//					du.uu = exp < -52 ? 0 : man >> -exp;
//				}
//				else // Mask off the implicit high bit
//					du.uu = (man & 0x000FFFFFFFFFFFFF) | ((ulong)exp << 52);
//			}

//			if (sign < 0)
//				du.uu |= 0x8000000000000000;

//			return du.dbl;
//		}

//		private static int CbitHighZero(ulong uu)
//		{
//			if ((uu & 0xFFFFFFFF00000000) == 0)
//				return 32 + CbitHighZero((uint)uu);

//			return CbitHighZero((uint)(uu >> 32));
//		}

//		private static int CbitHighZero(uint u)
//		{
//			if (u == 0)
//				return 32;

//			int cbit = 0;

//			if ((u & 0xFFFF0000) == 0)
//			{
//				cbit += 16;
//				u <<= 16;
//			}

//			if ((u & 0xFF000000) == 0)
//			{
//				cbit += 8;
//				u <<= 8;
//			}

//			if ((u & 0xF0000000) == 0)
//			{
//				cbit += 4;
//				u <<= 4;
//			}

//			if ((u & 0xC0000000) == 0)
//			{
//				cbit += 2;
//				u <<= 2;
//			}

//			if ((u & 0x80000000) == 0)
//				cbit += 1;

//			return cbit;
//		}

//		#endregion Helper Methods
//	}
//}
