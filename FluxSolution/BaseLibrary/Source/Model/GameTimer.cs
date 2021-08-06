namespace Flux.Model
{
  public class GameTimer
  {
    #region Fields and Properties
    private long _deltaCounter;
    /// <summary>The delta counter represents the total number of calls to the Update() function.</summary>
    public long DeltaCounter => _deltaCounter;

    private System.TimeSpan _deltaTime;
    /// <summary>The delta time is accumulated from all calls to the Update() function.</summary>
    public System.TimeSpan DeltaTime => _deltaTime;

    private System.TimeSpan _effectiveTime;
    /// <summary>Effective time is accumulated from the delta times from each call to the Update() function.</summary>
    public System.TimeSpan EffectiveTime => _effectiveTime;

    /// <summary>Total time is a directly calculated value, since the restarted time to the updated time, or paused time if paused.</summary>
    public System.TimeSpan TotalTime => IsPaused ? (_paused - _restarted) : (_updated - _restarted);

    /// <summary>Indicates whether the timer is paused.</summary>
    public bool IsPaused => _paused.Ticks > 0;

    private System.DateTime _paused;
    /// <summary>Indicates when the timer was last paused, or System.DateTime(0) if not.</summary>
    public System.DateTime Paused => _paused;

    private System.DateTime _restarted;
    /// <summary>Indicates when the timer was last restarted.</summary>
    public System.DateTime Restarted => _restarted;

    private System.DateTime _updated;
    /// <summary>Indicates when the timer was last updated.</summary>
    public System.DateTime Updated => _updated;
    #endregion

    public GameTimer() => Restart();

    /// <summary>Takes the timer out of paused mode, if paused.</summary>
    public void Continue()
    {
      if (IsPaused)
      {
        _paused = new System.DateTime(0);
      }
    }

    /// <summary>Enters the timer into paused mode, unless already paused.</summary>
    public void Pause()
    {
      if (!IsPaused)
      {
        _paused = System.DateTime.Now;
      }
    }

    /// <summary>Restart the timer, setting all values to initital values.</summary>
    public void Restart()
    {
      _deltaCounter = 0;
      _deltaTime = new System.TimeSpan(0);

      _effectiveTime = new System.TimeSpan(0);

      _paused = new System.DateTime(0);
      _restarted = System.DateTime.Now;
      _updated = _restarted;
    }

    /// <summary>Updates the timer values, as long as the timer is not paused. This version of Update() is made for when the delta value is calculated elsewhere.</summary>
    public void Update(System.TimeSpan deltaTime)
    {
      if (!IsPaused)
      {
        _updated = System.DateTime.Now;

        _deltaTime = deltaTime;
        _deltaCounter++;

        _effectiveTime += deltaTime;
      }
    }

    /// <summary>Updates the timer values, as long as the timer is not paused.</summary>
    public void Update()
    {
      if (!IsPaused)
      {
        long updatedTicks = _updated.Ticks;

        _updated = System.DateTime.Now;

        _deltaTime = new System.TimeSpan(_updated.Ticks - updatedTicks);
        _deltaCounter++;

        _effectiveTime = _effectiveTime.Add(_deltaTime);
      }
    }

    //public class Timer
    //{
    //  #region Fields and Properties
    //  private System.TimeSpan _deltaTime;
    //  public System.TimeSpan DeltaTime => _deltaTime;

    //  private System.TimeSpan _effectiveTime;
    //  public System.TimeSpan EffectiveTime => _effectiveTime;

    //  private ulong _frameCounter;
    //  public ulong FrameCounter => _frameCounter;

    //  private System.DateTime _paused;
    //  public System.DateTime Paused => _paused;

    //  private System.DateTime _started;
    //  public System.DateTime Started => _started;

    //  private System.DateTime _updated;
    //  public System.DateTime Updated => _updated;
    //  #endregion

    //  public bool IsPaused => _paused.Ticks > 0;
    //  public bool IsStarted => _started.Ticks > 0;

    //  public System.TimeSpan TotalTime => new System.TimeSpan(System.DateTime.Now.Ticks - _started.Ticks);

    //  public Timer() => Reset();

    //  public void Continue()
    //  {
    //    if (IsPaused)
    //    {
    //      _paused = new System.DateTime(0);

    //      _updated = System.DateTime.Now;
    //    }
    //    else
    //    {
    //      throw new System.InvalidOperationException("Timer can only continue when paused.");
    //    }
    //  }

    //  public void Pause()
    //  {
    //    if (IsStarted && !IsPaused)
    //    {
    //      _paused = System.DateTime.Now;
    //    }
    //    else
    //    {
    //      throw new System.InvalidOperationException("Timer can only pause when running.");
    //    }
    //  }

    //  public void Reset()
    //  {
    //    _deltaTime = new System.TimeSpan(0);

    //    _effectiveTime = new System.TimeSpan(0);

    //    _frameCounter = 0;

    //    _paused = new System.DateTime(0);
    //    _started = System.DateTime.Now;
    //    _updated = _started;
    //  }

    //  public void Start()
    //  {
    //    if (!IsStarted)
    //    {
    //      _paused = new System.DateTime(0);
    //      _started = System.DateTime.Now;
    //      _updated = _started;
    //    }
    //    else
    //    {
    //      throw new System.InvalidOperationException("Timer can only start once.");
    //    }
    //  }

    //  public void Update()
    //  {
    //    if (IsStarted && !IsPaused)
    //    {
    //      long updatedTicks = _updated.Ticks;

    //      _updated = System.DateTime.Now;

    //      _deltaTime = new System.TimeSpan(_updated.Ticks - updatedTicks);

    //      _effectiveTime = _effectiveTime.Add(_deltaTime);

    //      _frameCounter++;
    //    }
    //    else
    //    {
    //      throw new System.InvalidOperationException("Timer can only update when running.");
    //    }
    //  }

    //  public override string ToString() => "Frame #" + _frameCounter.ToString() + " (" + (EffectiveTime.TotalMilliseconds / FrameCounter).ToString("F1") + " ms), effective time " + _effectiveTime.ToString() + " from a total of " + TotalTime.ToString();
    //}

    ///// <summary>This is the game tree .</summary>
    //public static class Tree
    //{
    //	/// <summary>This is the node class for game tree organization.</summary>
    //	public class Node
    //		: System.Collections.Generic.LinkedList<Item>
    //	{
    //		/// <summary>Represents the child nodes, of this node.</summary>
    //		public System.Collections.Generic.LinkedList<Node> ChildNodes { get; set; }

    //		/// <summary>Represents the parent node, of this node.</summary>
    //		public Node ParentNode { get; set; }

    //		/// <summary>Represents the root node, of this tree.</summary>
    //		public Node RootNode
    //		{
    //			get
    //			{
    //				var parent = this;

    //				while (parent.ParentNode != null)
    //					parent = parent.ParentNode;

    //				return parent;
    //			}
    //		}

    //		public Node()
    //		{
    //			ChildNodes = new System.Collections.Generic.LinkedList<Node>();
    //		}

    //		public virtual void Process(float deltaTime, object surface)
    //		{
    //			if (!ProcessDisabled)
    //			{
    //				foreach (var item in this)
    //				{
    //					if (!item.UpdateDisabled)
    //						item.Update(deltaTime);

    //					if (!item.RenderDisabled)
    //						item.Render(surface);
    //				}
    //			}

    //			foreach (var childNode in ChildNodes)
    //				if (!childNode.ProcessDisabled)
    //					childNode.Process(deltaTime, surface);
    //		}
    //		/// <summary>Disables processing, if set to true.</summary>
    //		public bool ProcessDisabled { get; set; }
    //	}
    //}
  }
}
