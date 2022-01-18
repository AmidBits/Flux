using System;
using System.Linq;

namespace Flux.DataStructures
{
  public sealed class RuneBuilder
    : System.Collections.Generic.IList<System.Text.Rune>
  {
    private const int DefaultBufferSize = 128;

    private System.Text.Rune[] m_array;

    private int m_head; // Start of content.
    private int m_tail; // End of content.

    public RuneBuilder(int count)
    {
      m_array = new System.Text.Rune[count];

      m_head = count / 2;
      m_tail = count / 2;
    }
    public RuneBuilder()
      : this(DefaultBufferSize)
    {
    }

    public int Count
      => m_tail - m_head;

    public int GetUtf16SequenceLength()
    {
      var length = 0;
      for (var index = m_head; index < m_tail; index++)
        length += m_array[index].Utf16SequenceLength;
      return length;
    }

    public void DoubleBuffer()
    {
      var length = m_tail - m_head;

      var array = new System.Text.Rune[m_array.Length * 2];

      var head = (array.Length - length) / 2;
      var tail = head + length;

      System.Array.Copy(m_array, m_head, array, head, length);

      m_array = array;

      m_head = head;
      m_tail = tail;
    }
    public void EnsureBuffer(int length)
    {
      while (m_head < length || m_tail + length > m_array.Length)
        DoubleBuffer();
    }

    public RuneBuilder Append(RuneBuilder value)
    {
      m_version++;

      EnsureBuffer(value.Count);

      System.Array.Copy(value.m_array, value.m_head, m_array, m_tail, value.Count);

      m_tail += value.Count;

      return this;
    }
    public RuneBuilder Append(System.Text.Rune value)
    {
      m_version++;

      if (m_tail == m_array.Length)
        DoubleBuffer();

      m_array[m_tail++] = value;

      return this;
    }
    public RuneBuilder Append(char value)
      => Append((System.Text.Rune)value);
    public RuneBuilder Append(System.ReadOnlySpan<System.Text.Rune> runes)
    {
      m_version++;

      EnsureBuffer(runes.Length);

      runes.CopyTo(m_array, m_tail);

      m_tail += runes.Length;

      return this;
    }
    public RuneBuilder Append(System.Text.SpanRuneEnumerator enumerator)
    {
      foreach (var rune in enumerator)
        Append(rune);

      return this;
    }
    public RuneBuilder Append(System.ReadOnlySpan<char> chars)
      => Append(chars.EnumerateRunes());

    public RuneBuilder AppendLine()
      => Append(System.Environment.NewLine);
    public RuneBuilder AppendLine(RuneBuilder value)
      => Append(value).AppendLine();
    public RuneBuilder AppendLine(System.Text.Rune rune)
    => Append(rune).AppendLine();
    public RuneBuilder AppendLine(char character)
      => Append(character).AppendLine();
    public RuneBuilder AppendLine(System.ReadOnlySpan<System.Text.Rune> runes)
      => Append(runes).AppendLine();
    public RuneBuilder AppendLine(System.Text.SpanRuneEnumerator enumerator)
      => AppendLine().Append(enumerator);
    public RuneBuilder AppendLine(System.ReadOnlySpan<char> chars)
      => Append(chars).AppendLine();

    public RuneBuilder Prepend(RuneBuilder value)
    {
      m_version++;

      EnsureBuffer(value.Count);

      m_head -= value.Count;

      System.Array.Copy(value.m_array, value.m_head, m_array, m_head, value.Count);

      return this;
    }
    public RuneBuilder Prepend(System.Text.Rune value)
    {
      m_version++;

      if (m_head == 0)
        EnsureBuffer(1);

      m_array[--m_head] = value;

      return this;
    }
    public RuneBuilder Prepend(char value)
      => Prepend((System.Text.Rune)value);
    public RuneBuilder Prepend(System.ReadOnlySpan<System.Text.Rune> runes)
    {
      m_version++;

      EnsureBuffer(runes.Length);

      m_head -= runes.Length;

      runes.CopyTo(m_array, m_head);

      return this;
    }
    public RuneBuilder Prepend(System.Text.SpanRuneEnumerator enumerator)
    {
      var count = 0;
      foreach (var rune in enumerator)
      {
        Prepend(rune);
        count++;
      }

      System.Array.Reverse(m_array, m_head, count);

      return this;
    }
    public RuneBuilder Prepend(System.ReadOnlySpan<char> chars)
      => Prepend(chars.EnumerateRunes());

    public RuneBuilder PrependLine()
      => Prepend(System.Environment.NewLine);
    public RuneBuilder PrependLine(RuneBuilder value)
      => PrependLine().Prepend(value);
    public RuneBuilder PrependLine(System.Text.Rune value)
      => PrependLine().Prepend(value);
    public RuneBuilder PrependLine(char value)
      => PrependLine().Prepend(value);
    public RuneBuilder PrependLine(System.ReadOnlySpan<System.Text.Rune> runes)
      => PrependLine().Prepend(runes);
    public RuneBuilder PrependLine(System.Text.SpanRuneEnumerator enumerator)
      => PrependLine().Prepend(enumerator);
    public RuneBuilder PrependLine(System.ReadOnlySpan<char> chars)
      => PrependLine().Prepend(chars.EnumerateRunes());

    public string ToString(int startIndex, int count)
    {
      var sb = new System.Text.StringBuilder();

      for (var index = m_head + startIndex; count > 0; count--)
        sb.Append(m_array[index++].ToString());

      return sb.ToString();
    }

    #region Implementation of IList<T>
    private int m_version = 0;

    public System.Text.Rune this[int index]
    {
      get => m_array[index];
      set
      {
        m_version++;
        m_array[index] = value;
      }
    }

    public bool IsReadOnly
      => false;

    public void Add(System.Text.Rune item)
    {
      m_version++;
      Append(item);
    }

    public void Clear()
    {
      m_version++;

      m_array = new System.Text.Rune[DefaultBufferSize];

      m_head = DefaultBufferSize / 2;
      m_tail = DefaultBufferSize / 2;
    }

    public bool Contains(System.Text.Rune item)
      => System.Array.Exists(m_array, t => t.Equals(item));

    public void CopyTo(System.Text.Rune[] array, int arrayIndex)
      => m_array.CopyTo(array, arrayIndex);

    public System.Collections.Generic.IEnumerator<System.Text.Rune> GetEnumerator()
    {
      for (var index = m_head; index < m_tail; index++)
        yield return m_array[index];
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    public int IndexOf(System.Text.Rune item)
      => System.Array.IndexOf(m_array, item);

    public void Insert(int index, System.Text.Rune item)
    {
      m_version++;
      m_array.Insert(index, item);
    }

    public bool Remove(System.Text.Rune item)
    {
      if (IndexOf(item) is var index && index > -1)
      {
        RemoveAt(index);
        return true;
      }
      return false;
    }

    public void RemoveAt(int index)
    {
      m_version++;
      if (m_head <= m_array.Length - m_tail)
      {
        System.Array.Copy(m_array, m_head, m_array, m_head + 1, index - m_head);
        m_array[m_head - 1] = (System.Text.Rune)0;
      }
      else
      {
        System.Array.Copy(m_array, index + 1, m_array, index, m_tail - index - 1);
        m_array[m_tail] = (System.Text.Rune)0;
      }
    }
    #endregion Implementation of IList<T>
  }
}
