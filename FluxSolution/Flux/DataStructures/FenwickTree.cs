namespace Flux.DataStructures
{
  /// <summary>
  /// <para>A Fenwick tree is most easily understood using a one-based array A[n] with n values. This is a one-based implementation.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Fenwick_tree"/></para>
  /// </summary>
  public class FenwickTree
  {
    private readonly int[] m_tree;

    public FenwickTree(int treeSize)
    {
      m_tree = new int[treeSize + 1]; // 1-based indexing
    }

    /// <summary>
    /// <para>Update the tree by adding <paramref name="value"/> at <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    public void Update(int index, int value)
    {
      while (index < m_tree.Length)
      {
        m_tree[index] += value;

        index += index & -index; // Move to the next index, by LSB.
      }
    }

    /// <summary>
    /// <para>Get the prefix sum from index 1 to <paramref name="index"/>.</para>
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public int PrefixSum(int index)
    {
      var sum = 0;

      while (index > 0)
      {
        sum += m_tree[index];

        index -= index & -index; // Move to the parent index, by LSB.
      }

      return sum;
    }

    /// <summary>
    /// <para>Get the sum of elements in range [<paramref name="leftIndex"/>, <paramref name="rightIndex"/>].</para>
    /// </summary>
    /// <param name="leftIndex"></param>
    /// <param name="rightIndex"></param>
    /// <returns></returns>
    public int RangeSum(int leftIndex, int rightIndex)
      => PrefixSum(rightIndex) - PrefixSum(leftIndex - 1);
  }
}
