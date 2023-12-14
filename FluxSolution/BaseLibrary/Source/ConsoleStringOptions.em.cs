namespace Flux
{
  public record class ConsoleStringOptions
  {
    /// <summary>The horizontal separator character for the console string output, if applicable.</summary>
    public char HorizontalSeparator { get; init; } = '|';

    /// <summary>The vertical separator character for the console string output, if applicable.</summary>
    public char VerticalSeparator { get; init; } = '-';

    /// <summary>Make column content to uniform width in the console string output, if applicable.</summary>
    public bool UniformWidth { get; init; } = false;

    /// <summary>Center column content in the console string output, if applicable.</summary>
    public bool CenterContent { get; init; } = false;

    /// <summary>Includes the column names in the console string output, if applicable.</summary>
    public bool IncludeColumnNames { get; init; } = true;
  }
}
