namespace Flux.Combinatorics.Basics.Permutations
{
  /// <summary>
  /// <para>Heap's algorithm. All possible permutations, without repeats, of n objects.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
  /// </summary>
  public static partial class HeapsAlgorithm
  {
    //public static int Count(int setSize) => (int)System.Numerics.BigInteger.CreateChecked(setSize).PermutationsWithoutRepetition(setSize);

    //public static HeapsAlgorithm<T> Create<T>(T[] data) => new(data);

    //public static System.Collections.Generic.IEnumerable<T[]> GetPermutations<T>(T[] data)
    //{
    //  var ha = Create(data);

    //  while (ha.MoveNext())
    //    yield return ha.Current;
    //}

    /// <summary>
    /// <para>Creates a new sequence of all possible permutations of n objects.</para>
    /// <para>NOTE! This implementation share the same array space between each permutation, i.e. the previous permutation is lost to the next permutation.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <remarks>CAUTION! All permutations are created in the same single underlying storage array, which means that each permutation overwrites the previous permutation.</remarks>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<T[]> GetPermutations<T>(T[] source)
    {
      var stackState = new int[source.Length];

      System.Array.Fill(stackState, default);

      yield return source;

      var stackIndex = 0;

      while (stackIndex < stackState.Length)
      {
        if (stackState[stackIndex] < stackIndex)
        {
          if (int.IsEvenInteger(stackIndex))
            source.Swap(0, stackIndex);
          else
            source.Swap(stackState[stackIndex], stackIndex);

          yield return source;

          stackState[stackIndex]++;
          stackIndex = 1;
        }
        else
        {
          stackState[stackIndex] = 0;
          stackIndex++;
        }
      }
    }
  }

  ///// <summary>
  ///// <para>Heap's algorithm generates all possible permutations of n objects.</para>
  ///// <para>The algorithm minimizes movement: it generates each permutation from the previous one by interchanging a single pair of elements; the other n−2 elements are not disturbed.</para>
  ///// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
  ///// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  ///// </summary>
  //public sealed class HeapsAlgorithm<T>
  //{
  //  private readonly T[] m_original;

  //  private readonly T[] m_permutation; // The data from m_original while running.

  //  private int m_index;

  //  private int m_stackIndex; // The lexiographical index of the current permutation.
  //  private readonly int[] m_stackState; // The current state of the algorithm.

  //  public HeapsAlgorithm(T[] data)
  //  {
  //    m_original = new T[data.Length];
  //    data.CopyTo(m_original.AsSpan());

  //    m_stackState = new int[m_original.Length];

  //    m_permutation = new T[m_original.Length];

  //    Reset();
  //  }

  //  public T[] Current
  //    => m_stackIndex < 0 || m_stackIndex >= m_stackState.Length
  //    ? throw new System.InvalidOperationException()
  //    : m_permutation;

  //  public int Index => m_index;

  //  public bool MoveNext()
  //  {
  //    var change = false;

  //    if (m_stackIndex == -1)
  //    {
  //      m_stackIndex = 0;

  //      change = true; // return true;
  //    }
  //    else // Added so that the "change = true" above equals "return true".
  //      while (m_stackIndex < m_stackState.Length)
  //      {
  //        if (m_stackState[m_stackIndex] < m_stackIndex)
  //        {
  //          if (int.IsEvenInteger(m_stackIndex))
  //            m_permutation.Swap(0, m_stackIndex);
  //          else
  //            m_permutation.Swap(m_stackState[m_stackIndex], m_stackIndex);

  //          m_stackState[m_stackIndex]++;
  //          m_stackIndex = 1;

  //          change = true; // return true;
  //          break;
  //        }
  //        else
  //        {
  //          m_stackState[m_stackIndex] = 0;
  //          m_stackIndex++;
  //        }
  //      }

  //    if (change)
  //      m_index++;

  //    return change;
  //  }

  //  public void Reset()
  //  {
  //    m_original.CopyTo(m_permutation.AsSpan());

  //    m_index = -1;

  //    m_stackIndex = -1;

  //    System.Array.Fill(m_stackState, default);
  //  }
  //}
}
