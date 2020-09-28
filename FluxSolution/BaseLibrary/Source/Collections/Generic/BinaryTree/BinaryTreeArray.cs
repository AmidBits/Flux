using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace Flux
{
  namespace Collections.Generic
  {
    public interface IBinaryTreeArrayNode<TKey, TValue>
      where TKey : System.IComparable<TKey>
    {
      bool IsEmpty { get; }
      TKey Key { get; }
      TValue Value { get; }
    }

    public class BinaryTreeArray<TKey, TValue>
      where TKey : System.IComparable<TKey>
    {
      private static readonly IBinaryTreeArrayNode<TKey, TValue> Empty = new BinaryTreeArrayEmpty();

      private IBinaryTreeArrayNode<TKey, TValue>[] m_data;

      public int Count { get; private set; }

      /// <summary>Yields the count of descendants plus this node.</summary>
      public int GetCount(int index)
        => index > m_data.Length - 1 || m_data[index] is var indexItem && indexItem.IsEmpty
        ? 0
        : 1 + GetCount(ChildIndexLeft(index)) + GetCount(ChildIndexRight(index));

      public BinaryTreeArray(int size)
      {
        m_data = new IBinaryTreeArrayNode<TKey, TValue>[size];

        for (var index = 0; index < m_data.Length; index++)
        {
          m_data[index] = Empty;
        }
      }
      public BinaryTreeArray()
        : this(1)
      {
      }

      private static int ChildIndexLeft(int index)
        => (index << 1) + 1;
      private static int ChildIndexRight(int index)
        => (index << 1) + 2;
      private static int ParentIndex(int index)
        => (index - 1) >> 1;

      public bool Delete(TKey key, int index)
      {
        if (Search(key, index) is var deleteIndex && deleteIndex > -1)
        {
          Count--;

          m_data[deleteIndex] = Empty;

          for (var replaceIndex = m_data.Length - 1; replaceIndex >= 0 && replaceIndex > deleteIndex; replaceIndex--)
            if (!m_data[replaceIndex].IsEmpty)
            {
              m_data[deleteIndex] = m_data[replaceIndex];
              break;
            }

          return true;
        }

        return false;
      }
      public bool Delete(TKey key)
        => Delete(key, 0);

      private void Insert(TKey key, TValue value, int index)
      {
        if (index >= m_data.Length) // Extend array if needed.
        {
          var newIndices = m_data.Length;
          System.Array.Resize(ref m_data, index + 1);
          while (newIndices < m_data.Length) m_data[newIndices++] = Empty;
        }

        if (m_data[index] is var indexItem && indexItem.IsEmpty)
        {
          m_data[index] = new BinaryTreeArrayValue(key, value);

          Count++;
        }
        else if (key.CompareTo(indexItem.Key) > 0)
          Insert(key, value, ChildIndexRight(index));
        else
          Insert(key, value, ChildIndexLeft(index));
      }
      public void Insert(TKey key, TValue value)
        => Insert(key, value, 0);

      private int Search(TKey key, int index)
      {
        if (index >= m_data.Length)
          return -1;

        var indexItem = m_data[index];

        if (indexItem.IsEmpty)
          return -1;

        if (indexItem.Key.CompareTo(key) == 0)
          return index;

        var indexLeft = Search(key, ChildIndexLeft(index));
        if (indexLeft > -1)
          return indexLeft;

        var indexRight = Search(key, ChildIndexRight(index));
        if (indexRight > -1)
          return indexRight;

        return -1;
      }
      public IBinaryTreeArrayNode<TKey, TValue> Search(TKey key)
        => Search(key, 0) is var index && index > -1 ? m_data[index] : Empty;

      private class BinaryTreeArrayValue
        : IBinaryTreeArrayNode<TKey, TValue>
      {
        private readonly TKey m_key;
        private readonly TValue m_value;

        public bool IsEmpty
          => false;
        public TKey Key
          => m_key;
        public TValue Value
          => m_value;

        public BinaryTreeArrayValue(TKey key, TValue value)
        {
          m_key = key;
          m_value = value;
        }

        public override string ToString()
          => $"<{nameof(BinaryTreeArrayValue)} : \"{m_key}\" = \"{m_value}\">";
      }

      private class BinaryTreeArrayEmpty
        : IBinaryTreeArrayNode<TKey, TValue>
      {
        public bool IsEmpty
          => true;
        public TKey Key
          => throw new System.InvalidOperationException(nameof(BinaryTreeArrayEmpty));
        public TValue Value
          => throw new System.InvalidOperationException(nameof(BinaryTreeArrayEmpty));

        public override string ToString()
          => $"<{nameof(BinaryTreeArrayEmpty)}>";
      }
    }
  }
}
