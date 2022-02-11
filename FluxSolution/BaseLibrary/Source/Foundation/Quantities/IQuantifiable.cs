namespace Flux
{
  public interface IQuantifiable<TType>
  {
    /// <summary>Create a string representing the quantity.</summary>
    /// <param name="format">Pptionally format the value.</param>
    /// <param name="useFullName">Optionally use the full name, rather than symbols or shorter (e.g. acronym) variants.</param>
    /// <param name="preferUnicode">Optionally prefer Unicode characters where available.</param>
    /// <returns></returns>
    //string ToString(string? format, bool useFullName, bool preferUnicode);

    /// <summary>Create a value representing the value.</summary>
    //TType ToValue();

    /// <summary>The value of the quantity.</summary>
    TType Value { get; }
  }
}
