namespace Flux
{
  namespace Collections.Generic
  {
    public interface IBinaryTreeArrayNode<TValue>
      where TValue : System.IComparable<TValue>
    {
      bool IsEmpty { get; }
      TValue Value { get; }
    }

    public static class BinaryTreeArray
    {
      public static int ChildIndexLeft(int index) => (index << 1) + 1;
      public static int ChildIndexRight(int index) => (index << 1) + 2;
      public static int ParentIndex(int index) => (index - 1) >> 1;
    }

    public class BinaryTreeArray<TValue>
      where TValue : System.IComparable<TValue>
    {
      private static readonly IBinaryTreeArrayNode<TValue> Empty = new BinaryTreeArrayEmpty();

      private IBinaryTreeArrayNode<TValue>[] m_data;

      public BinaryTreeArray(int size)
      {
        m_data = new IBinaryTreeArrayNode<TValue>[size];

        for (var index = 0; index < m_data.Length; index++)
        {
          m_data[index] = Empty;
        }
      }

      public bool IsEmpty => false;

      public void Delete(TValue value)
      {
        var index = m_data.Length - 1;

        while (value.CompareTo(m_data[index].Value) != 0)
        {
          index--;
        }

        if (index > 0)
        {
          m_data[index] = Empty;
        }
      }
      private void Insert(TValue value, int index)
      {
        if (index >= m_data.Length)
        {
          var newIndices = m_data.Length;
          System.Array.Resize(ref m_data, index + 1);
          while (newIndices < m_data.Length) m_data[newIndices++] = Empty;
        }

        var element = m_data[index];

        if (element.IsEmpty) m_data[index] = new BinaryTreeArrayValue(value);
        else if (value.CompareTo(element.Value) < 0) Insert(value, BinaryTreeArray.ChildIndexLeft(index));
        else Insert(value, BinaryTreeArray.ChildIndexRight(index));
      }
      public void Insert(TValue value)
      {
        if (m_data[0].IsEmpty) m_data[0] = new BinaryTreeArrayValue(value);
        if (value.CompareTo(m_data[0].Value) > 0) Insert(value, 1);
      }
      public void Search(TValue value)
      {
        var index = m_data.Length - 1;

        while (value.CompareTo(m_data[index].Value) != 0)
        {
          index--;
        }

        if (index > 0)
        {
          m_data[index] = Empty;
        }
      }

      private class BinaryTreeArrayValue
        : IBinaryTreeArrayNode<TValue>
      {
        public bool IsEmpty => false;
        private readonly TValue m_value;
        public TValue Value => m_value;
        public BinaryTreeArrayValue(TValue value) => m_value = value;
      }

      private class BinaryTreeArrayEmpty
        : IBinaryTreeArrayNode<TValue>
      {
        public bool IsEmpty => true;
        public TValue Value => throw new System.InvalidOperationException();
      }
    }
  }
}
