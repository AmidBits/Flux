using System.Linq;

namespace Flux.Text.Csv
{
	public enum CsvTokenType
	{
		None,
		StartLine,
		StartField,
		EndField,
		EndLine
	}

	public class CsvReader
		: Flux.Disposable, System.Collections.Generic.IEnumerable<string[]>
	{
		private readonly CsvOptions m_options;

		private readonly System.IO.StreamReader m_streamReader;

		public CsvReader(System.IO.Stream stream, CsvOptions options)
		{
			m_options = options ?? new CsvOptions();

			m_streamReader = new System.IO.StreamReader(stream, m_options.Encoding);
		}
		public CsvReader(string csv, CsvOptions options)
		{
			m_options = options ?? new CsvOptions();

			m_streamReader = new System.IO.StreamReader(csv, m_options.Encoding);
		}

		private CsvTokenType m_tokenType = CsvTokenType.None;
		/// <summary>Returns the current type of token of the reader.</summary>
		public CsvTokenType TokenType
			=> m_tokenType;

		private readonly System.Collections.Generic.IList<string> m_fieldValues = new System.Collections.Generic.List<string>();
		/// <summary>Returns all accumulated field values for the current line.</summary>
		public System.Collections.Generic.IReadOnlyList<string> FieldValues
			=> (System.Collections.Generic.IReadOnlyList<string>)m_fieldValues;

		/// <summary>Returns the current field value, or null of there are no field values available yet.</summary>
		public string? FieldValue
			=> FieldValues.Count - 1 is var maxIndex && maxIndex > -1 ? FieldValues[maxIndex] : null;

		private readonly System.Text.StringBuilder m_fieldValue = new System.Text.StringBuilder(8192);

		/// <summary>Reads a field value ending with either a comma or a carriage return.</summary>
		public string ReadFieldValue()
		{
			m_fieldValue.Clear();

			var isQuotedField = false;

			for (var peek = m_streamReader.Peek(); peek != -1; peek = m_streamReader.Peek())
			{
				if (!isQuotedField) // We are not inside a double quoted field, and so various characters are important.
				{
					if (peek == '"') // Read is a double quote.
					{
						m_streamReader.Read();

						isQuotedField = true; // Enter double quoted field.
					}
					else if (peek == m_options.FieldSeparator) // Read is a field separator.
					{
						m_streamReader.Read();

						break;
					}
					else if (peek == '\r')
					{
						m_streamReader.Read();

						break;
					}
					else if (peek == '\n')
					{
						m_fieldValue.Append(char.ConvertFromUtf32(m_streamReader.Read()));

						break;
					}
					else
						m_fieldValue.Append(char.ConvertFromUtf32(m_streamReader.Read()));
				}
				else // We are inside a double quoted field, so really the only 'important' character will be an 'exit field' double quote.
				{
					if (peek == '"') // Read is a double quote.
					{
						m_streamReader.Read();

						if (m_streamReader.Peek() != '"') // Single double quote?
						{
							isQuotedField = false; // Exit double quoted field.
						}
						else // Peek is a double quote, so this is, or should be, an escape sequence.
						{
							m_fieldValue.Append(char.ConvertFromUtf32(m_streamReader.Read())); // Keep the other double quote (since it was escaped as one).
						}
					}
					else
						m_fieldValue.Append(char.ConvertFromUtf32(m_streamReader.Read())); // Anything else goes, for a double quoted field.
				}
			}

			return m_fieldValue.ToString();
		}

		public void ReadToNextToken()
		{
			switch (TokenType)
			{
				case CsvTokenType.None:
					if (m_streamReader.Peek() != -1)
					{
						m_tokenType = CsvTokenType.StartLine;
					}
					break;
				case CsvTokenType.StartLine:
					m_fieldValues.Clear();

					if (ReadFieldValue() is var startLineValue && startLineValue == "\n")
					{
						m_tokenType = CsvTokenType.EndLine;
					}
					else
					{
						m_fieldValues.Add(startLineValue);

						m_tokenType = CsvTokenType.StartField;
					}
					break;
				case CsvTokenType.StartField:
					m_tokenType = CsvTokenType.EndField;
					break;
				case CsvTokenType.EndField:
					if (ReadFieldValue() is var endFieldValue && endFieldValue == "\n")
					{
						m_tokenType = CsvTokenType.EndLine;
					}
					else
					{
						m_fieldValues.Add(endFieldValue);

						m_tokenType = CsvTokenType.StartField;
					}
					break;
				case CsvTokenType.EndLine:
					if (m_streamReader.Peek() == -1)
					{
						m_tokenType = CsvTokenType.None;
					}
					else
					{
						m_tokenType = CsvTokenType.StartLine;
					}
					break;
				default:
					throw new System.ArgumentOutOfRangeException(nameof(TokenType));
			}
		}

		public System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlyList<string>> ReadLists()
		{
			for (ReadToNextToken(); TokenType != CsvTokenType.None; ReadToNextToken())
			{
				if (TokenType == CsvTokenType.EndLine)
				{
					yield return FieldValues;
				}
			}
		}

		//public string[] ReadArray()
		//{
		//	var inDoubleQuotes = false;

		//	var sb = new System.Text.StringBuilder(8192);

		//	return m_streamReader.EndOfStream ? System.Array.Empty<string>() : GetFields().ToArray();

		//	System.Collections.Generic.IEnumerable<string> GetFields()
		//	{
		//		for (int read = m_streamReader.Read(), peek = m_streamReader.Peek(); read != -1; read = m_streamReader.Read(), peek = m_streamReader.Peek())
		//		{
		//			if (!inDoubleQuotes) // We are not inside a double quoted field, and so various characters are important.
		//			{
		//				if (read == '"') // Read is a double quote.
		//				{
		//					inDoubleQuotes = true; // Enter double quoted field.
		//				}
		//				else if (read == m_options.FieldSeparator) // Read is a field separator.
		//				{
		//					yield return sb.ToString();

		//					sb.Clear();
		//				}
		//				else if (read == '\r' || read == '\n') // If read is either a CR or an LF, we have reached the end of line (EOL).
		//				{
		//					if (read == '\r' && peek == '\n') // If read was a CR and peek is an LF (standard for CSV files)...
		//						m_streamReader.Read();  // We remove the peek LF from the stream.

		//					yield return sb.ToString(); // Return the accumulated characters as a field value.

		//					yield break; // Since this was the EOL we can break to denote a set of fields.
		//				}
		//				else
		//					sb.Append(char.ConvertFromUtf32(read));
		//			}
		//			else // We are inside a double quoted field, so really the only 'important' character will be an 'exit field' double quote.
		//			{
		//				if (read == '"') // Read is a double quote.
		//				{
		//					if (peek != '"')
		//					{
		//						inDoubleQuotes = false; // Exit double quoted field.
		//					}
		//					else // Peek is a double quote, so this is, or should be, an escape sequence.
		//					{
		//						m_streamReader.Read(); // Remove the peek double quote from the stream.

		//						sb.Append(char.ConvertFromUtf32(read)); // Keep the other double quote (since it was escaped as one).
		//					}
		//				}
		//				else
		//					sb.Append(char.ConvertFromUtf32(read)); // Anything else goes, for a double quoted field.
		//			}
		//		}
		//	}
		//}

		public System.Collections.Generic.IEnumerable<string[]> ReadArrays()
		{
			for (ReadToNextToken(); TokenType != CsvTokenType.None; ReadToNextToken())
				if (TokenType == CsvTokenType.EndLine)
					yield return FieldValues.ToArray();

			//while (!m_streamReader.EndOfStream)
			//{
			//	yield return ReadArray();
			//}
		}

		protected override void DisposeManaged()
			=> m_streamReader?.Dispose();

		// IEnumerable<string[]>
		public System.Collections.Generic.IEnumerator<string[]> GetEnumerator()
			=> ReadArrays().Cast<string[]>().GetEnumerator();
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}
