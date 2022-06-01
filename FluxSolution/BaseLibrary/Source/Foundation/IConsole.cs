namespace Flux
{
  // This is for objects that can output blocks (strings of characters) to the console.
  public interface IConsoleWritable
  {
    //System.Collections.Generic.IEnumerable<string> ToConsoleStrings();
    string ToConsoleBlock();
  }
}
