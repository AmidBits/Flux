namespace Flux.Text
{
	public ref struct Utf8JsonStreamReader
	{
		private readonly System.IO.Stream m_stream;
		// note: buffers will often be bigger than this - do not ever use this number for calculations.
		private readonly int m_bufferSize;

		private SequenceSegment? m_firstSegment;
		private int m_firstSegmentStartIndex;
		private SequenceSegment? m_lastSegment;
		private int m_lastSegmentEndIndex;

		private System.Text.Json.Utf8JsonReader m_jsonReader;
		private bool m_keepBuffers;
		private bool m_isFinalBlock;

		public Utf8JsonStreamReader(System.IO.Stream stream, int bufferSize)
		{
			m_stream = stream;
			m_bufferSize = bufferSize;

			m_firstSegment = null;
			m_firstSegmentStartIndex = 0;
			m_lastSegment = null;
			m_lastSegmentEndIndex = -1;

			m_jsonReader = default;
			m_keepBuffers = false;
			m_isFinalBlock = false;
		}

		public bool Read()
		{
			while (!m_jsonReader.Read()) // Read could be unsuccessful due to insufficient bufer size, retrying in loop with additional buffer segments.
			{
				if (m_isFinalBlock)
					return false;

				MoveNext();
			}

			return true;
		}

		public bool ReadTo(System.Text.Json.JsonTokenType tokenType)
		{
			while (Read())
				if (TokenType == tokenType)
					return true;

			return false;
		}

		private void MoveNext()
		{
			m_firstSegmentStartIndex += (int)m_jsonReader.BytesConsumed;

			// Release previous segments if possible.
			while (m_firstSegmentStartIndex > 0 && m_firstSegment?.Memory.Length <= m_firstSegmentStartIndex)
			{
				var currentFirstSegment = m_firstSegment;

				m_firstSegmentStartIndex -= m_firstSegment.Memory.Length;
				m_firstSegment = (SequenceSegment?)m_firstSegment.Next;

				if (!m_keepBuffers)
					currentFirstSegment.Dispose();
			}

			var newSegment = new SequenceSegment(m_bufferSize, m_lastSegment); // Add new segment.
			m_lastSegment?.SetNext(newSegment);
			m_lastSegment = newSegment;

			if (m_firstSegment == null)
			{
				m_firstSegment = newSegment;
				m_firstSegmentStartIndex = 0;
			}

			// read data from stream
			m_lastSegmentEndIndex = 0;
			int bytesRead;

			do
			{
				bytesRead = m_stream.Read(newSegment.Buffer.Memory.Span.Slice(m_lastSegmentEndIndex));

				m_lastSegmentEndIndex += bytesRead;
			}
			while (bytesRead > 0 && m_lastSegmentEndIndex < newSegment.Buffer.Memory.Length);

			m_isFinalBlock = m_lastSegmentEndIndex < newSegment.Buffer.Memory.Length;

			var data = new System.Buffers.ReadOnlySequence<byte>(m_firstSegment, m_firstSegmentStartIndex, m_lastSegment, m_lastSegmentEndIndex);

			m_jsonReader = new System.Text.Json.Utf8JsonReader(data, m_isFinalBlock, m_jsonReader.CurrentState);
		}

		private void DeserializePost()
		{
			// release memory if possible
			var firstSegment = m_firstSegment;
			var firstSegmentStartIndex = m_firstSegmentStartIndex + (int)m_jsonReader.BytesConsumed;

			while (firstSegment?.Memory.Length < firstSegmentStartIndex)
			{
				firstSegmentStartIndex -= firstSegment.Memory.Length;
				firstSegment.Dispose();
				firstSegment = (SequenceSegment?)firstSegment.Next;
			}

			if (firstSegment != m_firstSegment)
			{
				m_firstSegment = firstSegment;
				m_firstSegmentStartIndex = firstSegmentStartIndex;
				var data = new System.Buffers.ReadOnlySequence<byte>(m_firstSegment!, m_firstSegmentStartIndex, m_lastSegment!, m_lastSegmentEndIndex);
				m_jsonReader = new System.Text.Json.Utf8JsonReader(data, m_isFinalBlock, m_jsonReader.CurrentState);
			}
		}

		private long DeserializePre(out SequenceSegment? firstSegment, out int firstSegmentStartIndex)
		{
			// JsonSerializer.Deserialize can read only a single object. We have to extract the object to be deserialized into separate Utf8JsonReader. This incurs one additional pass through data (but data is only passed, not parsed).
			var tokenStartIndex = m_jsonReader.TokenStartIndex;
			firstSegment = m_firstSegment;
			firstSegmentStartIndex = m_firstSegmentStartIndex;

			// loop through data until end of object is found
			m_keepBuffers = true;
			int depth = 0;

			if (TokenType == System.Text.Json.JsonTokenType.StartObject || TokenType == System.Text.Json.JsonTokenType.StartArray)
				depth++;

			while (depth > 0 && Read())
			{
				if (TokenType == System.Text.Json.JsonTokenType.StartObject || TokenType == System.Text.Json.JsonTokenType.StartArray)
					depth++;
				else if (TokenType == System.Text.Json.JsonTokenType.EndObject || TokenType == System.Text.Json.JsonTokenType.EndArray)
					depth--;
			}

			m_keepBuffers = false;
			return tokenStartIndex;
		}

		public T Deserialize<T>(System.Text.Json.JsonSerializerOptions? options = null)
		{
			var tokenStartIndex = DeserializePre(out var firstSegment, out var firstSegmentStartIndex);

			var newJsonReader = new System.Text.Json.Utf8JsonReader(new System.Buffers.ReadOnlySequence<byte>(firstSegment!, firstSegmentStartIndex, m_lastSegment!, m_lastSegmentEndIndex).Slice(tokenStartIndex, m_jsonReader.Position), true, default);

			var result = System.Text.Json.JsonSerializer.Deserialize<T>(ref newJsonReader, options);
			DeserializePost();
			return result!;
		}

		public System.Text.Json.JsonDocument GetJsonDocument()
		{
			var tokenStartIndex = DeserializePre(out var firstSegment, out var firstSegmentStartIndex);

			var newJsonReader = new System.Text.Json.Utf8JsonReader(new System.Buffers.ReadOnlySequence<byte>(firstSegment!, firstSegmentStartIndex, m_lastSegment!, m_lastSegmentEndIndex).Slice(tokenStartIndex, m_jsonReader.Position), true, default);

			var result = System.Text.Json.JsonDocument.ParseValue(ref newJsonReader);
			DeserializePost();
			return result;
		}

		public void Dispose()
		{
			m_lastSegment?.Dispose();
			m_firstSegment?.Dispose();

			m_stream?.Dispose();
		}

		public int CurrentDepth => m_jsonReader.CurrentDepth;
		public bool HasValueSequence => m_jsonReader.HasValueSequence;
		public long TokenStartIndex => m_jsonReader.TokenStartIndex;
		public System.Text.Json.JsonTokenType TokenType => m_jsonReader.TokenType;
		public System.Buffers.ReadOnlySequence<byte> ValueSequence
			=> m_jsonReader.ValueSequence;
		public System.ReadOnlySpan<byte> ValueSpan
			=> m_jsonReader.ValueSpan;

		public bool GetBoolean()
			=> m_jsonReader.GetBoolean();
		public byte GetByte()
			=> m_jsonReader.GetByte();
		public byte[] GetBytesFromBase64()
			=> m_jsonReader.GetBytesFromBase64();
		public string GetComment()
			=> m_jsonReader.GetComment();
		public System.DateTime GetDateTime()
			=> m_jsonReader.GetDateTime();
		public System.DateTimeOffset GetDateTimeOffset()
			=> m_jsonReader.GetDateTimeOffset();
		public decimal GetDecimal()
			=> m_jsonReader.GetDecimal();
		public double GetDouble()
			=> m_jsonReader.GetDouble();
		public System.Guid GetGuid()
			=> m_jsonReader.GetGuid();
		public short GetInt16()
			=> m_jsonReader.GetInt16();
		public int GetInt32()
			=> m_jsonReader.GetInt32();
		public long GetInt64()
			=> m_jsonReader.GetInt64();
		[System.CLSCompliant(false)]
		public sbyte GetSByte()
			=> m_jsonReader.GetSByte();
		public float GetSingle()
			=> m_jsonReader.GetSingle();
		public string? GetString()
			=> m_jsonReader.GetString();
		[System.CLSCompliant(false)]
		public ushort GetUInt16()
			=> m_jsonReader.GetUInt16();
		[System.CLSCompliant(false)]
		public uint GetUInt32()
			=> m_jsonReader.GetUInt32();
		[System.CLSCompliant(false)]
		public ulong GetUInt64()
			=> m_jsonReader.GetUInt64();

		public bool TryGetDecimal(out byte value)
			=> m_jsonReader.TryGetByte(out value);
		public bool TryGetBytesFromBase64(out byte[]? value)
			=> m_jsonReader.TryGetBytesFromBase64(out value);
		public bool TryGetDateTime(out System.DateTime value)
			=> m_jsonReader.TryGetDateTime(out value);
		public bool TryGetDateTimeOffset(out System.DateTimeOffset value)
			=> m_jsonReader.TryGetDateTimeOffset(out value);
		public bool TryGetDecimal(out decimal value)
			=> m_jsonReader.TryGetDecimal(out value);
		public bool TryGetDouble(out double value)
			=> m_jsonReader.TryGetDouble(out value);
		public bool TryGetGuid(out System.Guid value)
			=> m_jsonReader.TryGetGuid(out value);
		public bool TryGetInt16(out short value)
			=> m_jsonReader.TryGetInt16(out value);
		public bool TryGetInt32(out int value)
			=> m_jsonReader.TryGetInt32(out value);
		public bool TryGetInt64(out long value)
			=> m_jsonReader.TryGetInt64(out value);
		[System.CLSCompliant(false)]
		public bool TryGetSByte(out sbyte value)
			=> m_jsonReader.TryGetSByte(out value);
		public bool TryGetSingle(out float value)
			=> m_jsonReader.TryGetSingle(out value);
		[System.CLSCompliant(false)]
		public bool TryGetUInt16(out ushort value)
			=> m_jsonReader.TryGetUInt16(out value);
		[System.CLSCompliant(false)]
		public bool TryGetUInt32(out uint value)
			=> m_jsonReader.TryGetUInt32(out value);
		[System.CLSCompliant(false)]
		public bool TryGetUInt64(out ulong value)
			=> m_jsonReader.TryGetUInt64(out value);

		private sealed class SequenceSegment
			: System.Buffers.ReadOnlySequenceSegment<byte>, System.IDisposable
		{
			internal System.Buffers.IMemoryOwner<byte> Buffer { get; }
			internal SequenceSegment? Previous { get; set; }

			public SequenceSegment(int size, SequenceSegment? previous)
			{
				Buffer = System.Buffers.MemoryPool<byte>.Shared.Rent(size);
				Previous = previous;

				Memory = Buffer.Memory;
				RunningIndex = previous?.RunningIndex + previous?.Memory.Length ?? 0;
			}

			public void SetNext(SequenceSegment next)
				=> Next = next;

			public void Dispose()
			{
				Buffer?.Dispose();
				Previous?.Dispose();
			}
		}
	}
}
