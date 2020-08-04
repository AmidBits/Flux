//namespace Flux.Numerics.NBerardi
//{
//	//https://gist.github.com/nberardi/2667136
//	// MISSING +-*/ etc.

//	public struct BigDecimal : System.IConvertible, System.IFormattable, System.IComparable, System.IComparable<BigDecimal>, System.IEquatable<BigDecimal>
//	{
//		public static readonly BigDecimal MinusOne = new BigDecimal(System.Numerics.BigInteger.MinusOne, 0);
//		public static readonly BigDecimal Zero = new BigDecimal(System.Numerics.BigInteger.Zero, 0);
//		public static readonly BigDecimal One = new BigDecimal(System.Numerics.BigInteger.One, 0);

//		private readonly System.Numerics.BigInteger _unscaledValue;
//		private readonly int _scale;

//		public BigDecimal(double value)
//			: this((decimal)value) { }

//		public BigDecimal(float value)
//			: this((decimal)value) { }

//		public BigDecimal(decimal value)
//		{
//			var bytes = FromDecimal(value);

//			var unscaledValueBytes = new byte[12];
//			System.Array.Copy(bytes, unscaledValueBytes, unscaledValueBytes.Length);

//			var unscaledValue = new System.Numerics.BigInteger(unscaledValueBytes);
//			var scale = bytes[14];

//			if(bytes[15] == 128)
//				unscaledValue *= System.Numerics.BigInteger.MinusOne;

//			_unscaledValue = unscaledValue;
//			_scale = scale;
//		}

//		public BigDecimal(int value)
//			: this(new System.Numerics.BigInteger(value), 0) { }

//		public BigDecimal(long value)
//			: this(new System.Numerics.BigInteger(value), 0) { }

//		public BigDecimal(uint value)
//			: this(new System.Numerics.BigInteger(value), 0) { }

//		public BigDecimal(ulong value)
//			: this(new System.Numerics.BigInteger(value), 0) { }

//		public BigDecimal(System.Numerics.BigInteger unscaledValue, int scale)
//		{
//			_unscaledValue = unscaledValue;
//			_scale = scale;
//		}

//		public BigDecimal(byte[] value)
//		{
//			byte[] number = new byte[value.Length - 4];
//			byte[] flags = new byte[4];

//			System.Array.Copy(value, 0, number, 0, number.Length);
//			System.Array.Copy(value, value.Length - 4, flags, 0, 4);

//			_unscaledValue = new System.Numerics.BigInteger(number);
//			_scale = System.BitConverter.ToInt32(flags, 0);
//		}

//		public bool IsEven { get { return _unscaledValue.IsEven; } }
//		public bool IsOne { get { return _unscaledValue.IsOne; } }
//		public bool IsPowerOfTwo { get { return _unscaledValue.IsPowerOfTwo; } }
//		public bool IsZero { get { return _unscaledValue.IsZero; } }
//		public int Sign { get { return _unscaledValue.Sign; } }

//		public override string ToString()
//		{
//			var number = new System.Text.StringBuilder(_unscaledValue.ToString("G"));

//			if(_scale > 0)
//			{
//				var negativeDisplacement = number[0] == '-' ? 1 : 0;

//				var positiveLength = number.Length - negativeDisplacement;

//				number.Insert(negativeDisplacement, $"{(positiveLength <= _scale ? "0" : string.Empty)}.{new string('0', _scale - positiveLength)}");
//			}

//			return number.ToString();
//		}

//		public byte[] ToByteArray()
//		{
//			var unscaledValue = _unscaledValue.ToByteArray();
//			var scale = System.BitConverter.GetBytes(_scale);

//			var bytes = new byte[unscaledValue.Length + scale.Length];
//			System.Array.Copy(unscaledValue, 0, bytes, 0, unscaledValue.Length);
//			System.Array.Copy(scale, 0, bytes, unscaledValue.Length, scale.Length);

//			return bytes;
//		}

//		private static byte[] FromDecimal(decimal d)
//		{
//			byte[] bytes = new byte[16];

//			int[] bits = decimal.GetBits(d);
//			int lo = bits[0];
//			int mid = bits[1];
//			int hi = bits[2];
//			int flags = bits[3];

//			bytes[0] = (byte)lo;
//			bytes[1] = (byte)(lo >> 8);
//			bytes[2] = (byte)(lo >> 0x10);
//			bytes[3] = (byte)(lo >> 0x18);
//			bytes[4] = (byte)mid;
//			bytes[5] = (byte)(mid >> 8);
//			bytes[6] = (byte)(mid >> 0x10);
//			bytes[7] = (byte)(mid >> 0x18);
//			bytes[8] = (byte)hi;
//			bytes[9] = (byte)(hi >> 8);
//			bytes[10] = (byte)(hi >> 0x10);
//			bytes[11] = (byte)(hi >> 0x18);
//			bytes[12] = (byte)flags;
//			bytes[13] = (byte)(flags >> 8);
//			bytes[14] = (byte)(flags >> 0x10);
//			bytes[15] = (byte)(flags >> 0x18);

//			return bytes;
//		}

//		#region Operators

//		public static bool operator ==(BigDecimal left, BigDecimal right)
//		{
//			return left.Equals(right);
//		}

//		public static bool operator !=(BigDecimal left, BigDecimal right)
//		{
//			return !left.Equals(right);
//		}

//		public static bool operator >(BigDecimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) > 0);
//		}

//		public static bool operator >=(BigDecimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) >= 0);
//		}

//		public static bool operator <(BigDecimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) < 0);
//		}

//		public static bool operator <=(BigDecimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) <= 0);
//		}

//		private static BigDecimal Multiply(BigDecimal left, BigDecimal right)
//		{
//			return new BigDecimal(left._unscaledValue * right._unscaledValue, left._scale + right._scale);
//		}
//		public static BigDecimal operator *(BigDecimal left, BigDecimal right)
//		{
//			return Multiply(left, right);
//		}


//		public static bool operator ==(BigDecimal left, decimal right)
//		{
//			return left.Equals(right);
//		}

//		public static bool operator !=(BigDecimal left, decimal right)
//		{
//			return !left.Equals(right);
//		}

//		public static bool operator >(BigDecimal left, decimal right)
//		{
//			return (left.CompareTo(right) > 0);
//		}

//		public static bool operator >=(BigDecimal left, decimal right)
//		{
//			return (left.CompareTo(right) >= 0);
//		}

//		public static bool operator <(BigDecimal left, decimal right)
//		{
//			return (left.CompareTo(right) < 0);
//		}

//		public static bool operator <=(BigDecimal left, decimal right)
//		{
//			return (left.CompareTo(right) <= 0);
//		}

//		public static bool operator ==(decimal left, BigDecimal right)
//		{
//			return left.Equals(right);
//		}

//		public static bool operator !=(decimal left, BigDecimal right)
//		{
//			return !left.Equals(right);
//		}

//		public static bool operator >(decimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) > 0);
//		}

//		public static bool operator >=(decimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) >= 0);
//		}

//		public static bool operator <(decimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) < 0);
//		}

//		public static bool operator <=(decimal left, BigDecimal right)
//		{
//			return (left.CompareTo(right) <= 0);
//		}

//		#endregion

//		/// <summary>
//		/// Returns the mantissa of value, aligned to the exponent of reference.
//		/// Assumes the exponent of value is larger than of reference.
//		/// </summary>
//		private static System.Numerics.BigInteger AlignExponent(BigDecimal value, BigDecimal reference)
//		{
//			return value._unscaledValue * System.Numerics.BigInteger.Pow(10, value._scale - reference._scale);
//		}


//		#region Explicity and Implicit Casts

//		public static explicit operator byte(BigDecimal value) { return value.ToType<byte>(); }
//		public static explicit operator sbyte(BigDecimal value) { return value.ToType<sbyte>(); }
//		public static explicit operator short(BigDecimal value) { return value.ToType<short>(); }
//		public static explicit operator int(BigDecimal value) { return value.ToType<int>(); }
//		public static explicit operator long(BigDecimal value) { return value.ToType<long>(); }
//		public static explicit operator ushort(BigDecimal value) { return value.ToType<ushort>(); }
//		public static explicit operator uint(BigDecimal value) { return value.ToType<uint>(); }
//		public static explicit operator ulong(BigDecimal value) { return value.ToType<ulong>(); }
//		public static explicit operator float(BigDecimal value) { return value.ToType<float>(); }
//		public static explicit operator double(BigDecimal value) { return value.ToType<double>(); }
//		public static explicit operator decimal(BigDecimal value) { return value.ToType<decimal>(); }
//		public static explicit operator System.Numerics.BigInteger(BigDecimal value)
//		{
//			var scaleDivisor = System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), value._scale);
//			var scaledValue = System.Numerics.BigInteger.Divide(value._unscaledValue, scaleDivisor);
//			return scaledValue;
//		}

//		public static implicit operator BigDecimal(byte value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(sbyte value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(short value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(int value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(long value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(ushort value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(uint value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(ulong value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(float value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(double value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(decimal value) { return new BigDecimal(value); }
//		public static implicit operator BigDecimal(System.Numerics.BigInteger value) { return new BigDecimal(value, 0); }

//		#endregion

//		public T ToType<T>() where T : struct
//		{
//			return (T)((System.IConvertible)this).ToType(typeof(T), null);
//		}

//		object System.IConvertible.ToType(System.Type conversionType, System.IFormatProvider provider)
//		{
//			var scaleDivisor = System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), this._scale);
//			var remainder = System.Numerics.BigInteger.Remainder(this._unscaledValue, scaleDivisor);
//			var scaledValue = System.Numerics.BigInteger.Divide(this._unscaledValue, scaleDivisor);

//			if(scaledValue > new System.Numerics.BigInteger(System.Decimal.MaxValue))
//				throw new System.ArgumentOutOfRangeException("value", "The value " + this._unscaledValue + " cannot fit into " + conversionType.Name + ".");

//			var leftOfDecimal = (decimal)scaledValue;
//			var rightOfDecimal = ((decimal)remainder) / ((decimal)scaleDivisor);

//			var value = leftOfDecimal + rightOfDecimal;
//			return System.Convert.ChangeType(value, conversionType);
//		}

//		public override bool Equals(object obj)
//		{
//			return ((obj is BigDecimal) && Equals((BigDecimal)obj));
//		}

//		public override int GetHashCode()
//		{
//			return _unscaledValue.GetHashCode() ^ _scale.GetHashCode();
//		}

//		#region IConvertible Members

//		System.TypeCode System.IConvertible.GetTypeCode()
//		{
//			return System.TypeCode.Object;
//		}

//		bool System.IConvertible.ToBoolean(System.IFormatProvider provider)
//		{
//			return System.Convert.ToBoolean(this);
//		}

//		byte System.IConvertible.ToByte(System.IFormatProvider provider)
//		{
//			return System.Convert.ToByte(this);
//		}

//		char System.IConvertible.ToChar(System.IFormatProvider provider)
//		{
//			throw new System.InvalidCastException("Cannot cast BigDecimal to Char");
//		}

//		System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider provider)
//		{
//			throw new System.InvalidCastException("Cannot cast BigDecimal to DateTime");
//		}

//		decimal System.IConvertible.ToDecimal(System.IFormatProvider provider)
//		{
//			return System.Convert.ToDecimal(this);
//		}

//		double System.IConvertible.ToDouble(System.IFormatProvider provider)
//		{
//			return System.Convert.ToDouble(this);
//		}

//		short System.IConvertible.ToInt16(System.IFormatProvider provider)
//		{
//			return System.Convert.ToInt16(this);
//		}

//		int System.IConvertible.ToInt32(System.IFormatProvider provider)
//		{
//			return System.Convert.ToInt32(this);
//		}

//		long System.IConvertible.ToInt64(System.IFormatProvider provider)
//		{
//			return System.Convert.ToInt64(this);
//		}

//		sbyte System.IConvertible.ToSByte(System.IFormatProvider provider)
//		{
//			return System.Convert.ToSByte(this);
//		}

//		float System.IConvertible.ToSingle(System.IFormatProvider provider)
//		{
//			return System.Convert.ToSingle(this);
//		}

//		string System.IConvertible.ToString(System.IFormatProvider provider)
//		{
//			return System.Convert.ToString(this);
//		}

//		ushort System.IConvertible.ToUInt16(System.IFormatProvider provider)
//		{
//			return System.Convert.ToUInt16(this);
//		}

//		uint System.IConvertible.ToUInt32(System.IFormatProvider provider)
//		{
//			return System.Convert.ToUInt32(this);
//		}

//		ulong System.IConvertible.ToUInt64(System.IFormatProvider provider)
//		{
//			return System.Convert.ToUInt64(this);
//		}

//		#endregion

//		#region IFormattable Members

//		public string ToString(string format, System.IFormatProvider formatProvider)
//		{
//			throw new System.NotImplementedException();
//		}

//		#endregion

//		#region IComparable Members

//		public int CompareTo(object obj)
//		{
//			if(obj == null)
//				return 1;

//			if(!(obj is BigDecimal))
//				throw new System.ArgumentException("Compare to object must be a BigDecimal", "obj");

//			return CompareTo((BigDecimal)obj);
//		}

//		#endregion

//		#region IComparable<BigDecimal> Members

//		public int CompareTo(BigDecimal other)
//		{
//			var unscaledValueCompare = this._unscaledValue.CompareTo(other._unscaledValue);
//			var scaleCompare = this._scale.CompareTo(other._scale);

//			// if both are the same value, return the value
//			if(unscaledValueCompare == scaleCompare)
//				return unscaledValueCompare;

//			// if the scales are both the same return unscaled value
//			if(scaleCompare == 0)
//				return unscaledValueCompare;

//			var scaledValue = System.Numerics.BigInteger.Divide(this._unscaledValue, System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), this._scale));
//			var otherScaledValue = System.Numerics.BigInteger.Divide(other._unscaledValue, System.Numerics.BigInteger.Pow(new System.Numerics.BigInteger(10), other._scale));

//			return scaledValue.CompareTo(otherScaledValue);
//		}

//		#endregion

//		#region IEquatable<BigDecimal> Members

//		public bool Equals(BigDecimal other)
//		{
//			return this._scale == other._scale && this._unscaledValue == other._unscaledValue;
//		}

//		#endregion
//	}
//}
