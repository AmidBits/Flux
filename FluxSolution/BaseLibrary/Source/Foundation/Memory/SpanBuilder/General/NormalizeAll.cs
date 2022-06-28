//namespace Flux
//{
//  public ref partial struct SpanBuilder<T>
//  {
//    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element.</summary>
//    public void NormalizeAll(T normalizeWith, System.Func<T, bool> predicate)
//    {
//      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));

//      var normalizedIndex = 0;

//      var isPrevious = true; // Set to true in order for trimming to occur on the left.

//      for (var sourceIndex = 0; sourceIndex < m_bufferPosition; sourceIndex++)
//      {
//        var character = m_buffer[sourceIndex];

//        var isCurrent = predicate(character);

//        if (!(isPrevious && isCurrent))
//        {
//          m_buffer[normalizedIndex++] = isCurrent ? normalizeWith : character;

//          isPrevious = isCurrent;
//        }
//      }

//      if (isPrevious) normalizedIndex--;

//      m_bufferPosition = normalizedIndex;

//      Clear();
//    }
//    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the specified equality comparer.</summary>
//    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IEqualityComparer<T> equalityComparer, System.Collections.Generic.IList<T> normalize)
//      => NormalizeAll(normalizeWith, t => normalize.Contains(t, equalityComparer));
//    /// <summary>Creates a new readonlyspan with the predicated characters normalized throughout the string. Normalizing means removing leading/trailing, and replace all elements satisfying the predicate with the specified element. Uses the default equality comparer.</summary>
//    public void NormalizeAll(T normalizeWith, System.Collections.Generic.IList<T> normalize)
//      => NormalizeAll(normalizeWith, normalize.Contains);
//  }
//}
