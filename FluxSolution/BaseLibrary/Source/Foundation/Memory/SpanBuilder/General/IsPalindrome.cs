//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Determines whether the sequence is a palindrome. Uses the specified comparer.</summary>
//    public bool IsPalindrome(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
//    {
//      if (equalityComparer is null) throw new System.ArgumentNullException(nameof(equalityComparer));

//      for (int indexL = 0, indexR = m_bufferPosition - 1; indexL < indexR; indexL++, indexR--)
//        if (!equalityComparer.Equals(m_buffer[indexL], m_buffer[indexR]))
//          return false;

//      return true;
//    }
//    /// <summary>Determines whether the sequence is a palindrome. Uses the default comparer.</summary>
//    public bool IsPalindrome()
//      => IsPalindrome(System.Collections.Generic.EqualityComparer<T>.Default);
//  }
//}
