//namespace Flux.Numerics
//{
//  public readonly record struct Size2<TSelf>
//    : ISize2<TSelf>
//    where TSelf : System.Numerics.INumber<TSelf>
//  {
//    public static readonly Size2<TSelf> Zero;

//    private readonly TSelf m_width;
//    private readonly TSelf m_height;

//    public Size2(TSelf width, TSelf height)
//    {
//      m_width = width;
//      m_height = height;
//    }

//    public TSelf Width { get => m_width; init => m_width = value; }
//    public TSelf Height { get => m_height; init => m_height = value; }

//    public Numerics.CartesianCoordinate2<TSelf> Center()
//      => new(
//        m_width.Divide(2),
//        m_height.Divide(2)
//      );

//    #region Overloaded operators
//    public static Size2<TSelf> operator +(Size2<TSelf> a, Size2<TSelf> b) => new(a.m_width + b.m_width, a.m_height + b.m_height);
//    public static Size2<TSelf> operator +(Size2<TSelf> a, TSelf b) => new(a.m_width + b, a.m_height + b);
//    public static Size2<TSelf> operator +(TSelf a, Size2<TSelf> b) => b + a;
//    public static Size2<TSelf> operator -(Size2<TSelf> a, Size2<TSelf> b) => new(a.m_width - b.m_width, a.m_height - b.m_height);
//    public static Size2<TSelf> operator -(Size2<TSelf> a, TSelf b) => new(a.m_width - b, a.m_height - b);
//    public static Size2<TSelf> operator -(TSelf a, Size2<TSelf> b) => b - a;
//    public static Size2<TSelf> operator *(Size2<TSelf> a, Size2<TSelf> b) => new(a.m_width * b.m_width, a.m_height * b.m_height);
//    public static Size2<TSelf> operator *(Size2<TSelf> a, TSelf b) => new(a.m_width * b, a.m_height * b);
//    public static Size2<TSelf> operator *(TSelf a, Size2<TSelf> b) => b * a;
//    public static Size2<TSelf> operator /(Size2<TSelf> a, Size2<TSelf> b) => new(a.m_width / b.m_width, a.m_height / b.m_height);
//    public static Size2<TSelf> operator /(Size2<TSelf> a, TSelf b) => new(a.m_width / b, a.m_height / b);
//    public static Size2<TSelf> operator /(TSelf a, Size2<TSelf> b) => b / a;
//    #endregion Overloaded operators

//    #region Object overrides
//    public override string? ToString()
//      => $"{GetType().Name} {{ Width = {m_width}, Height = {m_height} }}";
//    #endregion Object overrides
//  }
//}
