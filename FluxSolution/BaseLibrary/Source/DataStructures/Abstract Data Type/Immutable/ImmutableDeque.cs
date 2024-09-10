namespace Flux.DataStructures.Immutable
{
  /// <summary>
  /// <para>An immutable double ended queue implementation.</para>
  /// <para><see href="https://ericlippert.com/2008/01/22/immutability-in-c-part-ten-a-double-ended-queue/"/></para>
  /// <para><see href="https://ericlippert.com/2008/02/12/immutability-in-c-part-eleven-a-working-double-ended-queue/"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/Double-ended_queue"/></para>
  /// <para><seealso href="https://en.wikipedia.org/wiki/List_of_data_structures#Abstract_data_types"/></para>
  /// </summary>
  /// <typeparam name="TValue">The type of value for the immutable double-ended queue node.</typeparam>
  /// <remarks>This implementation is courtesy Eric Lippert.</remarks>
  public sealed class ImmutableDeque<TValue>
    : IDeque<TValue>
  {
    public static readonly IDeque<TValue> Empty = new EmptyDeque();

    private readonly Dequelette m_left;
    private readonly IDeque<Dequelette> m_middle;
    private readonly Dequelette m_right;

    public bool IsEmpty => false;
    public IDeque<TValue> EnqueueLeft(TValue value)
    {
      if (!m_left.Full) return new ImmutableDeque<TValue>(m_left.EnqueueLeft(value), m_middle, m_right);
      else return new ImmutableDeque<TValue>(new Dequelette2(value, m_left.PeekLeft()), m_middle.EnqueueLeft(m_left.DequeueLeft()), m_right);
    }
    public IDeque<TValue> EnqueueRight(TValue value)
    {
      if (!m_right.Full) return new ImmutableDeque<TValue>(m_left, m_middle, m_right.EnqueueRight(value));
      else return new ImmutableDeque<TValue>(m_left, m_middle.EnqueueRight(m_right.DequeueRight()), new Dequelette2(m_right.PeekRight(), value));
    }
    public IDeque<TValue> DequeueLeft()
    {
      if (m_left.Size > 1)
        return new ImmutableDeque<TValue>(m_left.DequeueLeft(), m_middle, m_right);
      if (!m_middle.IsEmpty)
        return new ImmutableDeque<TValue>(m_middle.PeekLeft(), m_middle.DequeueLeft(), m_right);
      if (m_right.Size > 1)
        return new ImmutableDeque<TValue>(new Dequelette1(m_right.PeekLeft()), m_middle, m_right.DequeueLeft());

      return new SingleDeque(m_right.PeekLeft());
    }
    public IDeque<TValue> DequeueRight()
    {
      if (m_right.Size > 1)
        return new ImmutableDeque<TValue>(m_left, m_middle, m_right.DequeueRight());
      if (!m_middle.IsEmpty)
        return new ImmutableDeque<TValue>(m_left, m_middle.DequeueRight(), m_middle.PeekRight());
      if (m_left.Size > 1)
        return new ImmutableDeque<TValue>(m_left.DequeueRight(), m_middle, new Dequelette1(m_left.PeekRight()));

      return new SingleDeque(m_left.PeekRight());
    }
    public TValue PeekLeft() => m_left.PeekLeft();
    public TValue PeekRight() => m_right.PeekRight();

    private ImmutableDeque(Dequelette left, IDeque<Dequelette> middle, Dequelette right)
    {
      m_left = left;
      m_middle = middle;
      m_right = right;
    }

    public override string ToString() => $"{GetType().Name}";

    private sealed class EmptyDeque
      : IDeque<TValue>
    {
      public bool IsEmpty => true;
      public IDeque<TValue> EnqueueLeft(TValue value) => new SingleDeque(value);
      public IDeque<TValue> EnqueueRight(TValue value) => new SingleDeque(value);
      public IDeque<TValue> DequeueLeft() => throw new System.Exception(nameof(EmptyDeque));
      public IDeque<TValue> DequeueRight() => throw new System.Exception(nameof(EmptyDeque));
      public TValue PeekLeft() => throw new System.Exception(nameof(EmptyDeque));
      public TValue PeekRight() => throw new System.Exception(nameof(EmptyDeque));

      public override string ToString() => $"{GetType().Name}";
    }

    private sealed class SingleDeque
      : IDeque<TValue>
    {
      private readonly TValue item;

      public SingleDeque(TValue t)
      {
        item = t;
      }

      public bool IsEmpty => false;
      public IDeque<TValue> EnqueueLeft(TValue value) => new ImmutableDeque<TValue>(new Dequelette1(value), ImmutableDeque<Dequelette>.Empty, new Dequelette1(item));
      public IDeque<TValue> EnqueueRight(TValue value) => new ImmutableDeque<TValue>(new Dequelette1(item), ImmutableDeque<Dequelette>.Empty, new Dequelette1(value));
      public IDeque<TValue> DequeueLeft() => Empty;
      public IDeque<TValue> DequeueRight() => Empty;
      public TValue PeekLeft() => item;
      public TValue PeekRight() => item;

      public override string ToString() => $"{GetType().Name} {{ IsEmpty = {IsEmpty} }}";
    }

    private abstract class Dequelette
    {
      public abstract int Size { get; }
      public virtual bool Full => false;
      public abstract TValue PeekLeft();
      public abstract TValue PeekRight();
      public abstract Dequelette EnqueueLeft(TValue t);
      public abstract Dequelette EnqueueRight(TValue t);
      public abstract Dequelette DequeueLeft();
      public abstract Dequelette DequeueRight();

      public override string ToString() => $"{GetType().Name} {{ Size = {Size}, Full = {Full} }}";
    }

    private sealed class Dequelette1
      : Dequelette
    {
      private readonly TValue v1;

      public Dequelette1(TValue t1)
      {
        v1 = t1;
      }

      public override int Size => 1;
      public override TValue PeekLeft() => v1;
      public override TValue PeekRight() => v1;
      public override Dequelette EnqueueLeft(TValue t) => new Dequelette2(t, v1);
      public override Dequelette EnqueueRight(TValue t) => new Dequelette2(v1, t);
      public override Dequelette DequeueLeft() => throw new System.Exception("Impossible");
      public override Dequelette DequeueRight() => throw new System.Exception("Impossible");
    }

    private sealed class Dequelette2
      : Dequelette
    {
      private readonly TValue v1;
      private readonly TValue v2;

      public Dequelette2(TValue t1, TValue t2)
      {
        v1 = t1;
        v2 = t2;
      }

      public override int Size => 2;
      public override TValue PeekLeft() => v1;
      public override TValue PeekRight() => v2;
      public override Dequelette EnqueueLeft(TValue t) => new Dequelette3(t, v1, v2);
      public override Dequelette EnqueueRight(TValue t) => new Dequelette3(v1, v2, t);
      public override Dequelette DequeueLeft() => new Dequelette1(v2);
      public override Dequelette DequeueRight() => new Dequelette1(v1);
    }

    private sealed class Dequelette3
      : Dequelette
    {
      private readonly TValue v1;
      private readonly TValue v2;
      private readonly TValue v3;

      public Dequelette3(TValue t1, TValue t2, TValue t3)
      {
        v1 = t1;
        v2 = t2;
        v3 = t3;
      }

      public override int Size => 3;
      public override TValue PeekLeft() => v1;
      public override TValue PeekRight() => v3;
      public override Dequelette EnqueueLeft(TValue t) => new Dequelette4(t, v1, v2, v3);
      public override Dequelette EnqueueRight(TValue t) => new Dequelette4(v1, v2, v3, t);
      public override Dequelette DequeueLeft() => new Dequelette2(v2, v3);
      public override Dequelette DequeueRight() => new Dequelette2(v1, v2);
    }

    private sealed class Dequelette4
      : Dequelette
    {
      private readonly TValue v1;
      private readonly TValue v2;
      private readonly TValue v3;
      private readonly TValue v4;

      public Dequelette4(TValue t1, TValue t2, TValue t3, TValue t4)
      {
        v1 = t1;
        v2 = t2;
        v3 = t3;
        v4 = t4;
      }

      public override int Size => 4;
      public override bool Full => true;
      public override TValue PeekLeft() => v1;
      public override TValue PeekRight() => v4;
      public override Dequelette EnqueueLeft(TValue t) => throw new System.Exception("Impossible");
      public override Dequelette EnqueueRight(TValue t) => throw new System.Exception("Impossible");
      public override Dequelette DequeueLeft() => new Dequelette3(v2, v3, v4);
      public override Dequelette DequeueRight() => new Dequelette3(v1, v2, v3);
    }
  }
}
