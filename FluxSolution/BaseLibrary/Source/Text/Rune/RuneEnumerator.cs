namespace Flux.Text
{
	public class RuneEnumerator
	: Disposable, System.Collections.Generic.IEnumerable<System.Text.Rune>
	{
		internal readonly System.IO.TextReader m_textReader;
		internal readonly int m_bufferSize;

		public RuneEnumerator(System.IO.Stream stream, System.Text.Encoding encoding, int bufferSize = 8192)
		{
			m_textReader = new System.IO.StreamReader(stream ?? throw new System.ArgumentNullException(nameof(stream)), encoding);
			m_bufferSize = bufferSize;
		}
		public RuneEnumerator(System.IO.TextReader textReader, int bufferSize = 8192)
		{
			m_textReader = textReader ?? throw new System.ArgumentNullException(nameof(textReader));
			m_bufferSize = bufferSize;
		}

		public System.Collections.Generic.IEnumerator<System.Text.Rune> GetEnumerator()
			=> new RuneIterator(this);
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();

		protected override void DisposeManaged()
			=> m_textReader.Dispose();

		private class RuneIterator
			: System.Collections.Generic.IEnumerator<System.Text.Rune>
		{
			private readonly RuneEnumerator m_enumerator;

			private readonly char[] m_array;
			private int m_index;
			private int m_count;

			private System.Text.Rune m_current;

			public RuneIterator(RuneEnumerator enumerator)
			{
				m_enumerator = enumerator;

				m_array = new char[enumerator.m_bufferSize];
				m_index = 0;
				m_count = 0;

				m_current = default!;
			}

			public System.Text.Rune Current
				=> m_current;
			object System.Collections.IEnumerator.Current
				=> m_current!;

			public bool MoveNext()
			{
				var difference = m_count - m_index;

				if (difference <= 4)
				{
					if (difference > 0)
						System.Array.Copy(m_array, m_index, m_array, 0, difference);

					m_index = 0;
					m_count = difference;
				}

				if (m_index == 0 && m_count < m_array.Length)
				{
					m_count += m_enumerator.m_textReader.Read(m_array, m_count, m_array.Length - m_count);
				}

				if (System.Text.Rune.DecodeFromUtf16(m_array.AsSpan().Slice(m_index, m_count - m_index), out var rune, out var count) is var or && or == System.Buffers.OperationStatus.Done)
				{
					m_index += count;

					m_current = rune;

					return true;
				}

				return false;
			}

			public void Reset()
				=> throw new System.NotImplementedException();
			//{
			//	m_enumerator.m_streamReader.BaseStream.Position = 0;

			//	charIndex = 0;
			//	charCount = 0;

			//	m_current = default!;
			//}

			public void Dispose() { }
		}
	}
}
