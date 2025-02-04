namespace Flux
{
  /// <summary>Extended implementation of (System.IDisposable) ManagedDisposable. Only needed if directly using unmanaged resources (add the call "Dispose(false)" inside the finalizer) , and possibly if lots of managed resources needs cleaning up right away.</summary>
  public class Disposable
    : System.IDisposable
  {
    protected bool IsDisposed { get; set; } // Disposed has already occured?

    /// <summary>Overload implementation of dispose; if isDisposing = false the method has been called by the runtime from inside the finalizer and only unmanaged resources should be disposed, else if isDisposing = true the method has been called directly or indirectly by a user's code and both managed and unmanaged resources should be disposed.</summary>
    protected virtual void Dispose(bool isDisposing)
    {
      if (!IsDisposed)
      {
        if (isDisposing)
          DisposeManaged();

        DisposeUnmanaged();

        IsDisposed = true;
      }
    }

    /// <summary>Override for disposal of managed resources.</summary>
    protected virtual void DisposeManaged()
    { }

    /// <summary>Override for disposal of unmanaged resources.</summary>
    protected virtual void DisposeUnmanaged()
    { }

    // System.IDisposable
    public void Dispose()
    {
      Dispose(true); // Dispose of both managed and native resources.

      System.GC.SuppressFinalize(this); // Inform GC that our resources has already been cleaned up, no need to call finalize later.
    }
  }
}
