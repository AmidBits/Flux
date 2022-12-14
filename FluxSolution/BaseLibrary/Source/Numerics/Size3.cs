namespace Flux.Numerics
{
  public readonly record struct Size3<TSelf>
    : ISize3<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    public static readonly Size3<TSelf> Zero;

    private readonly TSelf m_width;
    private readonly TSelf m_height;
    private readonly TSelf m_depth;

    public Size3(TSelf width, TSelf height, TSelf depth)
    {
      m_width = width;
      m_height = height;
      m_depth = depth;
    }

    public TSelf Width { get => m_width; init => m_width = value; }
    public TSelf Height { get => m_height; init => m_height = value; }
    public TSelf Depth { get => m_depth; init => m_depth = value; }

    public Numerics.CartesianCoordinate3<TSelf> Center()
      => new(
        m_width.Divide(2),
        m_height.Divide(2),
        m_depth.Divide(2)
      );

    #region Overloaded operators
    public static Size3<TSelf> operator +(Size3<TSelf> a, Size3<TSelf> b) => new(a.m_width + b.m_width, a.m_height + b.m_height, a.m_depth + b.m_depth);
    public static Size3<TSelf> operator +(Size3<TSelf> a, TSelf b) => new(a.m_width + b, a.m_height + b, a.m_depth + b);
    public static Size3<TSelf> operator +(TSelf a, Size3<TSelf> b) => b + a;
    public static Size3<TSelf> operator -(Size3<TSelf> a, Size3<TSelf> b) => new(a.m_width - b.m_width, a.m_height - b.m_height, a.m_depth - b.m_depth);
    public static Size3<TSelf> operator -(Size3<TSelf> a, TSelf b) => new(a.m_width - b, a.m_height - b, a.m_depth - b);
    public static Size3<TSelf> operator -(TSelf a, Size3<TSelf> b) => b - a;
    public static Size3<TSelf> operator *(Size3<TSelf> a, Size3<TSelf> b) => new(a.m_width * b.m_width, a.m_height * b.m_height, a.m_depth * b.m_depth);
    public static Size3<TSelf> operator *(Size3<TSelf> a, TSelf b) => new(a.m_width * b, a.m_height * b, a.m_depth * b);
    public static Size3<TSelf> operator *(TSelf a, Size3<TSelf> b) => b * a;
    public static Size3<TSelf> operator /(Size3<TSelf> a, Size3<TSelf> b) => new(a.m_width / b.m_width, a.m_height / b.m_height, a.m_depth / b.m_depth);
    public static Size3<TSelf> operator /(Size3<TSelf> a, TSelf b) => new(a.m_width / b, a.m_height / b, a.m_depth / b);
    public static Size3<TSelf> operator /(TSelf a, Size3<TSelf> b) => b / a;
    #endregion Overloaded operators

    #region Object overrides
    public override string? ToString()
      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height}, Depth = {m_depth} }}";
    #endregion Object overrides
  }
}
