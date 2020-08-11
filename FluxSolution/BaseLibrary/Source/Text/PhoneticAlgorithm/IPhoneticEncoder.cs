namespace Flux
{
  /// <summary>Represents an algorithm for indexing of words by their pronunciation.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Phonetic_algorithm"/>
  interface IPhoneticEncoder
  {
    /// <summary>Represents the method that computes a code value representing the source (word) reduced to its phonetic properties as interpreted by the algorithm.</summary>
    /// <param name="expression">The string to encode.</param>
    string Encode(string expression);
  }
}
