using System.Linq;

namespace Flux.Numerics
{
	public struct BigDecimal
		: System.IConvertible, System.IFormattable, System.IComparable, System.IComparable<BigDecimal>, System.IEquatable<BigDecimal>
	{
		public static readonly BigDecimal MinusOne = new BigDecimal(System.Numerics.BigInteger.MinusOne, 0);
		public static readonly BigDecimal Zero = new BigDecimal(System.Numerics.BigInteger.Zero, 0);
		public static readonly BigDecimal One = new BigDecimal(System.Numerics.BigInteger.One, 0);

		private readonly System.Numerics.BigInteger m_unscaledValue;
		private readonly int m_scale;

		public BigDecimal(double value)
			: this((decimal)value) { }

		public BigDecimal(float value)
			: this((decimal)value) { }

		public BigDecimal(decimal value)
		{
			var bytes = FromDecimal(value);

			var unscaledValueBytes = new byte[12];
			System.Array.Copy(bytes, unscaledValueBytes, unscaledValueBytes.Length);

			var unscaledValue = new System.Numerics.BigInteger(unscaledValueBytes);
			var scale = bytes[14];

			if (bytes[15] == 128)
				unscaledValue *= System.Numerics.BigInteger.MinusOne;

			m_unscaledValue = unscaledValue;
			m_scale = scale;
		}

		public BigDecimal(int value)
			: this(new System.Numerics.BigInteger(value), 0)
		{ }
		public BigDecimal(long value)
			: this(new System.Numerics.BigInteger(value), 0)
		{ }
		[System.CLSCompliant(false)]
		public BigDecimal(uint value)
			: this(new System.Numerics.BigInteger(value), 0)
		{ }
		[System.CLSCompliant(false)]
		public BigDecimal(ulong value)
			: this(new System.Numerics.BigInteger(value), 0)
		{ }

		public BigDecimal(System.Numerics.BigInteger unscaledValue, int scale)
		{
			m_unscaledValue = unscaledValue;
			m_scale = scale;
		}

		public BigDecimal(byte[] value)
		{
			byte[] number = new byte[value.Length - 4];
			byte[] flags = new byte[4];

			System.Array.Copy(value, 0, number, 0, number.Length);
			System.Array.Copy(value, value.Length - 4, flags, 0, 4);

			m_unscaledValue = new System.Numerics.BigInteger(number);
			m_scale = System.BitConverter.ToInt32(flags, 0);
		}

		public bool IsEven => m_unscaledValue.IsEven;
		public bool IsOne => m_unscaledValue.IsOne;
		public bool IsPowerOfTwo => m_unscaledValue.IsPowerOfTwo;
		public bool IsZero => m_unscaledValue.IsZero;
		public int Sign => m_unscaledValue.Sign;

		public override string ToString()
		{
			var number = m_unscaledValue.ToString("G");

			if (m_scale > 0)
				return number.Insert(number.Length - m_scale, ".");

			return number;
		}

		public byte[] ToByteArray()
		{
			var unscaledValue = m_unscaledValue.ToByteArray();
			var scale = System.BitConverter.GetBytes(m_scale);

			var bytes = new byte[unscaledValue.Length + scale.Length];
			System.Array.Copy(unscaledValue, 0, bytes, 0, unscaledValue.Length);
			System.Array.Copy(scale, 0, bytes, unscaledValue.Length, scale.Length);

			return bytes;
		}

		private static byte[] FromDecimal(decimal d)
		{
			byte[] bytes = new byte[16];

			int[] bits = decimal.GetBits(d);
			int lo = bits[0];
			int mid = bits[1];
			int hi = bits[2];
			int flags = bits[3];

			bytes[0] = (byte)lo;
			bytes[1] = (byte)(lo >> 8);
			bytes[2] = (byte)(lo >> 0x10);
			bytes[3] = (byte)(lo >> 0x18);
			bytes[4] = (byte)mid;
			bytes[5] = (byte)(mid >> 8);
			bytes[6] = (byte)(mid >> 0x10);
			bytes[7] = (byte)(mid >> 0x18);
			bytes[8] = (byte)hi;
			bytes[9] = (byte)(hi >> 8);
			bytes[10] = (byte)(hi >> 0x10);
			bytes[11] = (byte)(hi >> 0x18);
			bytes[12] = (byte)flags;
			bytes[13] = (byte)(flags >> 8);
			bytes[14] = (byte)(flags >> 0x10);
			bytes[15] = (byte)(flags >> 0x18);

			return bytes;
		}

		#region Operators

		public static bool operator ==(BigDecimal left, BigDecimal right) => left.Equals(right);
		public static bool operator !=(BigDecimal left, BigDecimal right) => !left.Equals(right);
		public static bool operator >(BigDecimal left, BigDecimal right) => left.CompareTo(right) > 0;
		public static bool operator >=(BigDecimal left, BigDecimal right) => (left.CompareTo(right) >= 0);
		public static bool operator <(BigDecimal left, BigDecimal right) => left.CompareTo(right) < 0;
		public static bool operator <=(BigDecimal left, BigDecimal right) => left.CompareTo(right) <= 0;
		public static bool operator ==(BigDecimal left, decimal right) => left.Equals(right);
		public static bool operator !=(BigDecimal left, decimal right) => !left.Equals(right);
		public static bool operator >(BigDecimal left, decimal right) => left.CompareTo(right) > 0;
		public static bool operator >=(BigDecimal left, decimal right) => left.CompareTo(right) >= 0;
		public static bool operator <(BigDecimal left, decimal right) => left.CompareTo(right) < 0;
		public static bool operator <=(BigDecimal left, decimal right) => left.CompareTo(right) <= 0;
		public static bool operator ==(decimal left, BigDecimal right) => left.Equals(right);
		public static bool operator !=(decimal left, BigDecimal right) => !left.Equals(right);
		public static bool operator >(decimal left, BigDecimal right) => left.CompareTo(right) > 0;
		public static bool operator >=(decimal left, BigDecimal right) => left.CompareTo(right) >= 0;
		public static bool operator <(decimal left, BigDecimal right) => left.CompareTo(right) < 0;
		public static bool operator <=(decimal left, BigDecimal right) => left.CompareTo(right) <= 0;

		#endregion

		#region Explicity and Implicit Casts

		public static explicit operator byte(BigDecimal value) => value.ToType<byte>();
		[System.CLSCompliant(false)]
		public static explicit operator sbyte(BigDecimal value) => value.ToType<sbyte>();
		public static explicit operator short(BigDecimal value) => value.ToType<short>();
		public static explicit operator int(BigDecimal value) => value.ToType<int>();
		public static explicit operator long(BigDecimal value) => value.ToType<long>();
		[System.CLSCompliant(false)]
		public static explicit operator ushort(BigDecimal value) => value.ToType<ushort>();
		[System.CLSCompliant(false)]
		public static explicit operator uint(BigDecimal value) => value.ToType<uint>();
		[System.CLSCompliant(false)]
		public static explicit operator ulong(BigDecimal value) => value.ToType<ulong>();
		public static explicit operator float(BigDecimal value) => value.ToType<float>();
		public static explicit operator double(BigDecimal value) => value.ToType<double>();
		public static explicit operator decimal(BigDecimal value) => value.ToType<decimal>();
		public static explicit operator System.Numerics.BigInteger(BigDecimal value)
		{
			var scaleDivisor = System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), value.m_scale);
			var scaledValue = System.Numerics.BigInteger.Divide(value.m_unscaledValue, scaleDivisor);
			return scaledValue;
		}

		public static implicit operator BigDecimal(byte value) => new BigDecimal(value);
		[System.CLSCompliant(false)]
		public static implicit operator BigDecimal(sbyte value) => new BigDecimal(value);
		public static implicit operator BigDecimal(short value) => new BigDecimal(value);
		public static implicit operator BigDecimal(int value) => new BigDecimal(value);
		public static implicit operator BigDecimal(long value) => new BigDecimal(value);
		[System.CLSCompliant(false)]
		public static implicit operator BigDecimal(ushort value) => new BigDecimal(value);
		[System.CLSCompliant(false)]
		public static implicit operator BigDecimal(uint value) => new BigDecimal(value);
		[System.CLSCompliant(false)]
		public static implicit operator BigDecimal(ulong value) => new BigDecimal(value);
		public static implicit operator BigDecimal(float value) => new BigDecimal(value);
		public static implicit operator BigDecimal(double value) => new BigDecimal(value);
		public static implicit operator BigDecimal(decimal value) => new BigDecimal(value);
		public static implicit operator BigDecimal(System.Numerics.BigInteger value) => new BigDecimal(value, 0);

		#endregion

		public T ToType<T>()
			where T : struct
			=> (T)((System.IConvertible)this).ToType(typeof(T), null);

		object System.IConvertible.ToType(System.Type conversionType, System.IFormatProvider? provider)
		{
			var scaleDivisor = System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), this.m_scale);
			var remainder = System.Numerics.BigInteger.Remainder(this.m_unscaledValue, scaleDivisor);
			var scaledValue = System.Numerics.BigInteger.Divide(this.m_unscaledValue, scaleDivisor);

			if (scaledValue > new System.Numerics.BigInteger(System.Decimal.MaxValue))
				throw new System.ArgumentOutOfRangeException("value", "The value " + this.m_unscaledValue + " cannot fit into " + conversionType.Name + ".");

			var leftOfDecimal = (decimal)scaledValue;
			var rightOfDecimal = ((decimal)remainder) / ((decimal)scaleDivisor);

			var value = leftOfDecimal + rightOfDecimal;
			return System.Convert.ChangeType(value, conversionType);
		}

		public override bool Equals(object? obj)
			=> (obj is BigDecimal o) && Equals(o);
		public override int GetHashCode()
			=> m_unscaledValue.GetHashCode() ^ m_scale.GetHashCode();

		#region IConvertible Members

		System.TypeCode System.IConvertible.GetTypeCode()
			=> System.TypeCode.Object;

		bool System.IConvertible.ToBoolean(System.IFormatProvider? provider) => System.Convert.ToBoolean(this);
		byte System.IConvertible.ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(this);
		char System.IConvertible.ToChar(System.IFormatProvider? provider) => throw new System.InvalidCastException("Cannot cast BigDecimal to Char");
		System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider? provider) => throw new System.InvalidCastException("Cannot cast BigDecimal to DateTime");
		decimal System.IConvertible.ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(this);
		double System.IConvertible.ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(this);
		short System.IConvertible.ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(this);
		int System.IConvertible.ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(this);
		long System.IConvertible.ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(this);
		sbyte System.IConvertible.ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(this);
		float System.IConvertible.ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(this);
		string System.IConvertible.ToString(System.IFormatProvider? provider) => System.Convert.ToString(this);
		[System.CLSCompliant(false)]
		ushort System.IConvertible.ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(this);
		[System.CLSCompliant(false)]
		uint System.IConvertible.ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(this);
		[System.CLSCompliant(false)]
		ulong System.IConvertible.ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(this);

		#endregion

		#region IFormattable Members

		public string ToString(string? format, System.IFormatProvider? formatProvider) => throw new System.NotImplementedException();

		#endregion

		// IComparable
		public int CompareTo(object? obj)
			=> obj is null
			? 1
			: obj is BigDecimal o
			? CompareTo(o)
			: throw new System.ArgumentException("Compare to object must be a BigDecimal", nameof(obj));

		// IComparable<BigDecimal>
		public int CompareTo(BigDecimal other)
		{
			var unscaledValueCompare = this.m_unscaledValue.CompareTo(other.m_unscaledValue);
			var scaleCompare = this.m_scale.CompareTo(other.m_scale);

			// if both are the same value, return the value
			if (unscaledValueCompare == scaleCompare)
				return unscaledValueCompare;

			// if the scales are both the same return unscaled value
			if (scaleCompare == 0)
				return unscaledValueCompare;

			var scaledValue = System.Numerics.BigInteger.Divide(this.m_unscaledValue, System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), this.m_scale));
			var otherScaledValue = System.Numerics.BigInteger.Divide(other.m_unscaledValue, System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), other.m_scale));

			return scaledValue.CompareTo(otherScaledValue);
		}

		#region IEquatable<BigDecimal> Members

		public bool Equals(BigDecimal other)
			=> m_scale == other.m_scale && m_unscaledValue == other.m_unscaledValue;

		#endregion
	}
}