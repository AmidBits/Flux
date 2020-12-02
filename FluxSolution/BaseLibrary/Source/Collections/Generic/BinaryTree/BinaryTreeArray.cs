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

      public IBinaryTreeArrayNode<TKey, TValue> this[int index]
      {
        get { return index >= 0 && index < m_data.Length ? m_data[index] : Empty; }
        //set { if (index >= 0 && index < m_data.Length) m_data[index] = value; }
      }
      public int Count { get; private set; }

      /// <summary>Yields the count of descendants plus this node.</summary>
      public int GetTreeCount(int index)
        => index > m_data.Length - 1 || m_data[index] is var indexItem && indexItem.IsEmpty
        ? 0
        : 1 + GetTreeCount(ChildIndexLeft(index)) + GetTreeCount(ChildIndexRight(index));

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

      public bool IsBST(int index, TKey minimumKey, TKey maximumKey)
      {
        if (index >= m_data.Length - 1 || (m_data[index] is var indexItem && indexItem.IsEmpty)) return true;
        if (indexItem.Key.CompareTo(minimumKey) < 0 || indexItem.Key.CompareTo(maximumKey) > 0) return false;

        return IsBST(ChildIndexLeft(index), minimumKey, indexItem.Key) && IsBST(ChildIndexRight(index), indexItem.Key, maximumKey);
      }
      public bool IsBT(int index)
      {
        var indexItem = m_data[index];

        var indexLeft = ChildIndexLeft(index);
        var indexRight = ChildIndexRight(index);

        var boolLeft = indexLeft > m_data.Length - 1 || (m_data[indexLeft] is var indexItemLeft && (indexItemLeft.IsEmpty || (indexItemLeft.Key.CompareTo(indexItem.Key) < 0 && IsBT(indexLeft))));
        var boolRight = indexRight > m_data.Length - 1 || (m_data[indexRight] is var indexItemRight && (indexItemRight.IsEmpty || (indexItemRight.Key.CompareTo(indexItem.Key) > 0 && IsBT(indexRight))));

        return boolLeft && boolRight;
      }

      private static int ChildIndexLeft(int index)
        => (index << 1) + 1;
      private static int ChildIndexRight(int index)
        => (index << 1) + 2;
      private static int ParentIndex(int index)
        => (index - 1) >> 1;

      private bool Delete(TKey key, int index)
      {
        if (Search(key, index) is var deleteIndex && deleteIndex > -1)
        {
          Count--;

          m_data[deleteIndex] = Empty;

          for (var replaceIndex = m_data.Length - 1; replaceIndex >= 0 && replaceIndex > deleteIndex; replaceIndex--)
            if (!m_data[replaceIndex].IsEmpty)
            {
              m_data[deleteIndex] = m_data[replaceIndex];
              m_data[replaceIndex] = Empty;
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
          //while (newIndices < m_data.Length) m_data[newIndices++] = Empty;
          System.Array.Fill(m_data, Empty, newIndices, m_data.Length - newIndices);
        }

        if (m_data[index] is var indexItem && indexItem.IsEmpty)
        {
          m_data[index] = new BinaryTreeArrayValue(key, value);

          Count++;
        }
        else if (key.CompareTo(indexItem.Key) < 0)
          Insert(key, value, ChildIndexLeft(index));
        else
          Insert(key, value, ChildIndexRight(index));
      }
      public void Insert(TKey key, TValue value)
        => Insert(key, value, 0);

      private int Search(TKey key, int index)
      {
        if (index >= m_data.Length - 1)
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

      //public System.Collections.Generic.IEnumerable<(int, IBinaryTreeArrayNode<TKey, TValue>)> TraverseBreadthFirstSearch(int index)
      //{
      //  var current = new System.Collections.Generic.Queue<int>();
      //  current.Enqueue(index);

      //  var next = new System.Collections.Generic.Queue<int>();
      //  var level = 0;

      //  while (current.Count > 0)
      //  {
      //    index = current.Dequeue();

      //    if (this[index] is var item && !item.IsEmpty)
      //      yield return (level, item);

      //    if (ChildIndexLeft(index) is var indexL && !this[indexL].IsEmpty)
      //      next.Enqueue(indexL);
      //    if (ChildIndexRight(index) is var indexR && !this[indexR].IsEmpty)
      //      next.Enqueue(indexR);

      //    if (current.Count == 0)
      //    {
      //      current = next;
      //      next = new System.Collections.Generic.Queue<int>();
      //      level++;
      //    }
      //  }
      //}

      //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchInOrder(int index)
      //{
      //  Flux.Collections.Generic.Traversal.BinaryTreeSearch()
      //  if (this[index] is var item && item.IsEmpty) yield break;

      //  foreach (var itemL in TraverseDepthFirstSearchInOrder(ChildIndexLeft(index))) yield return itemL;
      //  yield return item;
      //  foreach (var itemR in TraverseDepthFirstSearchInOrder(ChildIndexRight(index))) yield return itemR;
      //}
      //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchPostOrder(int index)
      //{
      //  if (this[index] is var item && item.IsEmpty) yield break;

      //  foreach (var itemL in TraverseDepthFirstSearchPostOrder(ChildIndexLeft(index))) yield return itemL;
      //  foreach (var itemR in TraverseDepthFirstSearchPostOrder(ChildIndexRight(index))) yield return itemR;
      //  yield return item;
      //}
      //public System.Collections.Generic.IEnumerable<IBinaryTreeArrayNode<TKey, TValue>> TraverseDepthFirstSearchPreOrder(int index)
      //{
      //  if (this[index] is var item && item.IsEmpty) yield break;

      //  yield return item;
      //  foreach (var itemL in TraverseDepthFirstSearchPreOrder(ChildIndexLeft(index))) yield return itemL;
      //  foreach (var itemR in TraverseDepthFirstSearchPreOrder(ChildIndexRight(index))) yield return itemR;
      //}

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
