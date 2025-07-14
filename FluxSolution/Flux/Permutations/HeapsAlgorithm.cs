namespace Flux.Permutations
{
  public record class HeapsAlgorithm<T>
  {
  	private T[] m_data;
  	
  	private T[] m_stackData;
  	private int m_stackIndex;
  	private int[] m_stackState;
  	
  	public HeapsAlgorithm(T[] data)
  	{
  		m_data = new T[data.Length];
  		
  		data.CopyTo(m_data.AsSpan());
  
  		m_stackState = new int[m_data.Length];
  
  		m_stackData = new T[m_data.Length];
  
  		Reset();
  	}
  
  	public T[] Current => m_stackData;
  
  	public bool MoveNext()
  	{
  		if(m_stackIndex == -1)
  		{
  			m_stackIndex = 0;
  			
  			return true;
  		}
  
  		while (m_stackIndex < m_stackState.Length)
  		{
  			if (m_stackState[m_stackIndex] < m_stackIndex)
  			{
  				if (int.IsEvenInteger(m_stackIndex))
  					m_stackData.Swap(0, m_stackIndex);
  				else
  					m_stackData.Swap(m_stackState[m_stackIndex], m_stackIndex);
  
  				m_stackState[m_stackIndex]++;
  				m_stackIndex = 1;
  
  				return true;
  			}
  			else
  			{
  				m_stackState[m_stackIndex] = 0;
  				m_stackIndex++;
  			}
  		}
  			
  		return false;
  	}
  	
    public void Reset()
    {
      m_data.CopyTo(m_stackData.AsSpan());
  
      m_stackIndex = -1;
  
      System.Array.Fill(m_stackState, default);
    }
  
  	
  	public static System.Collections.Generic.IEnumerable<T[]> GetPermutations(T[] data)
  	{
  		var ha = new HeapsAlgorithm<T>(data);
  
  		while(ha.MoveNext())
  		{
  			yield return ha.Current;
  		}
  	}
  }

  /// <summary>
  /// <para>Heap's algorithm generates all possible permutations of n objects.</para>
  /// <para>The algorithm minimizes movement: it generates each permutation from the previous one by interchanging a single pair of elements; the other n−2 elements are not disturbed.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Heap%27s_algorithm"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Permutation"/></para>
  /// </summary>
  public static partial class HeapsAlgorithm
  {
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
    public static System.Collections.Generic.IEnumerable<T[]> PermutationsHeapsAlgorithm<T>(this T[] source)
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
}
