namespace Flux
{
  public record class ConsoleStringOptions
  {
    private readonly char m_horizontalSeparator = '|';
    private readonly char m_verticalSeparator = '-';
    private readonly bool m_uniformWidth = false;
    private readonly bool m_centerContent = false;
    private readonly bool m_includeColumnNames = true;

    public static ConsoleStringOptions Default { get; } = new();

    /// <summary>The horizontal separator character for the console string output, if applicable.</summary>
    public char HorizontalSeparator { get => m_horizontalSeparator; init => m_horizontalSeparator = value; }

    /// <summary>The vertical separator character for the console string output, if applicable.</summary>
    public char VerticalSeparator { get => m_verticalSeparator; init => m_verticalSeparator = value; }

    /// <summary>Make column content to uniform width in the console string output, if applicable.</summary>
    public bool UniformWidth { get => m_uniformWidth; init => m_uniformWidth = value; }

    /// <summary>Center column content in the console string output, if applicable.</summary>
    public bool CenterContent { get => m_centerContent; init => m_centerContent = value; }

    /// <summary>Includes the column names in the console string output, if applicable.</summary>
    public bool IncludeColumnNames { get => m_includeColumnNames; init => m_includeColumnNames = value; }
  }
}
